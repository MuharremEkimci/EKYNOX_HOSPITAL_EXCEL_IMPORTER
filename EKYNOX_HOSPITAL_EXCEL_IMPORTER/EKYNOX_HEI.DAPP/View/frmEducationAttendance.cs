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
using EKYNOX_HEI.DATA.DataModel.Common;
using EKYNOX_HEI.CORE.Helpers;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmEducationAttendance : DevExpress.XtraEditors.XtraForm
    {
        private EducationAttendanceModel viewModel;
        private readonly clsEducationAttendance educationAttendanceService;
        private readonly IServiceProvider serviceProvider;
        private readonly UserInfoSet userInfo;
        public EducationAttendanceListViewModel updInfo;

        public frmEducationAttendance(clsEducationAttendance _educationAttendanceService, IServiceProvider serviceProvider, UserInfoSet _userInfo)
        {
            InitializeComponent();
            viewModel = new EducationAttendanceModel();
            educationAttendanceService = _educationAttendanceService;
            this.serviceProvider = serviceProvider;
            this.userInfo = _userInfo;
            updInfo = new EducationAttendanceListViewModel();
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
                if (Convert.ToInt32(slueEducator.EditValue) <= 0)
                    throw new Exception("Eğitmen seçilmesi zorunludur.");

                if (Convert.ToInt32(slueInstitutions.EditValue) <= 0)
                    throw new Exception("Kurum seçilmesi zorunludur.");

                if (viewModel.ImagesDetailList.Any(c => c.ReadAndExcelProcess != CORE.Enums.ReadAndExcelProcessEnum.ProcessCompleted))
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

                        if (!viewModel.ImagesDetailList.Any(c => c.FileName == fileName || c.FileData == fileData))
                        {
                            viewModel.ImagesDetailList.Add(new EducationAttendanceListModel
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

            slueEducator.EditValue = userInfo.LogicalRef;

            if (updInfo.LogicalRef > 0)
            {
                var res = educationAttendanceService.GetData(updInfo.LogicalRef);
                if (res.Status == CORE.Enums.StatusEnum.Error)
                {
                    MessageBox.Show("İlgili kayıt getirilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AppLogger.Error(DateTime.Now, nameof(frmEducationAttendance), nameof(frmEducationAttendance_Load), nameof(educationAttendanceService.GetData), res.Message);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                if (res.Status == CORE.Enums.StatusEnum.Warning)
                {
                    MessageBox.Show(res.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                var data = res.Data;

                viewModel.LogicalRef = data.LogicalRef;
                teDocNo.Text = data.DocNo;
                slueEducator.EditValue = data.EducatorRef;
                slueInstitutions.EditValue = data.InstitutionRef;
                viewModel.ImagesDetailList = data.ImagesDetailList;
            }

            grdList.DataSource = viewModel.ImagesDetailList;
            grvList.Columns["EducationDate"].ColumnEdit = repDeDate;
        }

        private void btnReadImages_Click(object sender, EventArgs e)
       {
            if (ReadImagesValidate() && MessageBox.Show("Görsellere istinaden girilen bilgilerin doğruluğundan emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var filterList = viewModel.ImagesDetailList.Where(c => c.ReadAndExcelProcess == CORE.Enums.ReadAndExcelProcessEnum.NonProcess).OrderBy(c => c.EducationNumber).ThenBy(c => c.EducationType).ToList();

                //SplashScreenManager.ShowForm(typeof(frmWaitingForm));
                //SplashScreenManager.CloseForm();

                foreach (var item in filterList)
                {
                    var frm = serviceProvider.GetRequiredService<frmImageReadConfirm>();
                    frm.imageInfo = item;
                    frm.excelData = viewModel.ExcelData;
                    if (frm.ShowDialog() != DialogResult.OK)
                        break;

                    viewModel.ExcelData = frm.excelData;
                }               
            }
        }

        private bool ReadImagesValidate() 
        {
            bool blResult = true;

            try
            {
                if (!viewModel.ImagesDetailList.Any())
                    throw new Exception("Görsel Seçimi Yapılmalıdır.");

                if (viewModel.ImagesDetailList.Any(c => !Enum.IsDefined(c.EducationType)))
                    throw new Exception("Eğitim Türü Seçimi Yapılmalıdır.");

                if (viewModel.ImagesDetailList.Any(c => !Enum.IsDefined(c.ModuleType)))
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