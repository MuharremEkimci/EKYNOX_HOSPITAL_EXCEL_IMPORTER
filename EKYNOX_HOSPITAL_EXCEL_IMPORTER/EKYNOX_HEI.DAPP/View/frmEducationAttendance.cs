using DevExpress.XtraEditors;
using EKYNOX_HEI.CORE.Models.EducationAttendance;
using EKYNOX_HEI.DAPP.View.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using EKYNOX_HEI.DAPP.Controller;
using Microsoft.Extensions.DependencyInjection;
using HeyRed.Mime;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmEducationAttendance : DevExpress.XtraEditors.XtraForm
    {
        private List<EducationAttendanceListModel> list;
        private readonly clsEducationAttendance educationAttendanceService;
        private readonly IServiceProvider serviceProvider;
        public frmEducationAttendance(clsEducationAttendance _educationAttendanceService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            list = new List<EducationAttendanceListModel>();
            educationAttendanceService = _educationAttendanceService;
            this.serviceProvider = serviceProvider;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveValidate())
            {

            }
        }

        private bool SaveValidate() 
        {
            var blResult = true;

            try
            {
                if (list.Any(c => c.ReadAndExcelProcess != CORE.Enums.ReadAndExcelProcessEnum.ProcessCompleted))
                    throw new Exception("Yüklenen tüm resimlerin veri doğrulama işlemleri tamamlanmalıdır.");
            }
            catch (Exception ex)
            {
                blResult = false;
                MessageBox.Show(ex.Message,"Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return blResult;
        }

        private void btnFileSelect_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                var ofd = new OpenFileDialog();

                ofd.Title = "Resimleri Seçiniz";
                ofd.Multiselect = true;
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tüm Dosyalar|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string[] selectFiles = ofd.FileNames;

                    foreach (string file in selectFiles)
                    {
                        var filePath = file;
                        var fileName = Path.GetFileName(file);
                        var fileData = File.ReadAllBytes(file);
                        var fileMimeType = MimeTypesMap.GetMimeType(filePath);

                        if (!list.Any(c => c.FileName == fileName || c.FileData == fileData))
                        {
                            list.Add(new EducationAttendanceListModel
                            {
                                FilePath = filePath,
                                FileName = fileName,
                                FileData = fileData,
                                FileMimeType = fileMimeType,
                                EducationDate = DateTime.Now,
                                ReadAndExcelProcess = CORE.Enums.ReadAndExcelProcessEnum.NonProcess
                            });
                        }
                    }
                }

                grdList.DataSource = list;
                grvList.Columns["EducationDate"].ColumnEdit = repDeDate;
            }
        }

        private void grvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = grvList.GetFocusedRow() as CORE.Models.EducationAttendance.EducationAttendanceListModel;

            if (row != null)
            {
                peMain.Image = row.FileData != null ? Image.FromStream(new System.IO.MemoryStream(row.FileData)) : null;
            }
        }

        private void frmEducationAttendance_Load(object sender, EventArgs e)
        {
            beFileSelect.Text = "Birden Fazla Dosya Seçmek İçin Tıklayınız";

            slueInstitutions.Properties.DataSource = educationAttendanceService.GetInstutionsList().Data;
            slueInstitutions.Properties.ValueMember = "LogicalRef";
            slueInstitutions.Properties.DisplayMember = "Name";

            slueEducator.Properties.DataSource = educationAttendanceService.GetUsersList().Data;
            slueEducator.Properties.ValueMember = "LogicalRef";
            slueEducator.Properties.DisplayMember = "FullName";
        }

        private void btnReadImages_Click(object sender, EventArgs e)
       {
            if (ReadImagesValidate() && MessageBox.Show("Görsellere istinaden girilen bilgilerin doğruluğundan emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var filterList = list.Where(c => c.ReadAndExcelProcess == CORE.Enums.ReadAndExcelProcessEnum.NonProcess).OrderBy(c => c.EducationNumber).ThenBy(c => c.EducationType).ToList();

                //SplashScreenManager.ShowForm(typeof(frmWaitingForm));
                //SplashScreenManager.CloseForm();

                foreach (var item in filterList)
                {
                    var frm = serviceProvider.GetRequiredService<frmImageReadConfirm>();
                    frm.imageInfo = item;
                    if (frm.ShowDialog() != DialogResult.OK)
                        break;
                }               
            }
        }

        private bool ReadImagesValidate() 
        {
            bool blResult = true;

            try
            {
                if (!list.Any())
                    throw new Exception("Görsel Seçimi Yapılmalıdır.");

                if (list.Any(c => !Enum.IsDefined(c.EducationType)))
                    throw new Exception("Eğitim Türü Seçimi Yapılmalıdır.");

                if (list.Any(c => !Enum.IsDefined(c.ModuleType)))
                    throw new Exception("Modül Seçimi Yapılmalıdır.");
            }
            catch (Exception ex)
            {
                blResult = false;
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

            return blResult;
        }
    }
}