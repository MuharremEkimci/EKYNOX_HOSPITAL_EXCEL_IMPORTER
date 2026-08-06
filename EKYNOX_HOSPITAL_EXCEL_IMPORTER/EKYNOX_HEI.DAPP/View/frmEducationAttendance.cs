using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.EducationAttendance;
using EKYNOX_HEI.DAPP.Controller;
using EKYNOX_HEI.DAPP.View.AISetting;
using EKYNOX_HEI.DAPP.View.Common;
using EKYNOX_HEI.DATA.DataModel.Common;
using HeyRed.Mime;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                viewModel.DocNo = teDocNo.Text;
                viewModel.InstitutionRef = Convert.ToInt32(slueInstitutions.EditValue);
                viewModel.EducatorRef = Convert.ToInt32(slueEducator.EditValue);

                if (viewModel.LogicalRef > 0)
                {
                    var res = educationAttendanceService.Update(viewModel);
                    if (res.Status == StatusEnum.Error)
                    {
                        MessageBox.Show("Eğitim katılım güncellenirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        AppLogger.Error(DateTime.Now, nameof(frmEducationAttendance), nameof(btnSave_Click), nameof(educationAttendanceService.Update), res.Message);
                        return;
                    }
                }
                else
                {
                    var res = educationAttendanceService.Save(viewModel);
                    if (res.Status == StatusEnum.Error)
                    {
                        MessageBox.Show("AI ayarları kaydedilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        AppLogger.Error(DateTime.Now, nameof(frmEducationAttendance), nameof(btnSave_Click), nameof(educationAttendanceService.Save), res.Message);
                        return;
                    }
                }

                this.DialogResult = DialogResult.OK;
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
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                    grdList.RefreshDataSource();
                    grdList.Refresh();
                    grvList.RefreshData();
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
                viewModel.ExcelData = data.ExcelData;
            }
            else
            {
                teDocNo.Text = educationAttendanceService.CreateEducationAttendanceNo().Data;
            }

            grdList.DataSource = viewModel.ImagesDetailList;
            grvList.Columns["EducationDate"].ColumnEdit = repDeDate;
        }

        private void btnReadImages_Click(object sender, EventArgs e)
        {
            if (ReadImagesValidate() && MessageBox.Show("Görsellere istinaden girilen bilgilerin doğruluğundan emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var filterList = viewModel.ImagesDetailList.Where(c => c.ReadAndExcelProcess == CORE.Enums.ReadAndExcelProcessEnum.NonProcess).OrderBy(c => c.EducationNumber).ThenBy(c => c.EducationType).ToList();

                foreach (var item in filterList)
                {
                    var frm = serviceProvider.GetRequiredService<frmImageReadConfirm>();
                    frm.imageInfo = item;
                    frm.excelData = viewModel.ExcelData;
                    frm.educator = slueEducator.Text;
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
                if (Convert.ToInt32(slueEducator.EditValue) <= 0)
                    throw new Exception("Eğitmen Seçimi Yapılmalıdır.");

                if (!viewModel.ImagesDetailList.Any())
                    throw new Exception("Görsel Seçimi Yapılmalıdır.");

                if (viewModel.ImagesDetailList.Any(c => !Enum.IsDefined(c.EducationType)))
                    throw new Exception("Eğitim Türü Seçimi Yapılmalıdır.");

                if (viewModel.ImagesDetailList.Any(c => !Enum.IsDefined(c.ModuleType)))
                    throw new Exception("Modül Seçimi Yapılmalıdır.");

                if (viewModel.ImagesDetailList.Any(c => c.EducationNumber <= 0))
                    throw new Exception("Eğitim Numarası Yazılmalıdır.");
            }
            catch (Exception ex)
            {
                blResult = false;
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return blResult;
        }

        private void grvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Delete)
            {
                int rowHandle = grvList.FocusedRowHandle;
                if (rowHandle >= 0 && MessageBox.Show("Satır Silinecektir.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    var row = grvList.GetRow(rowHandle) as CORE.Models.EducationAttendance.EducationAttendanceListModel;

                    if (row != null && row.ReadAndExcelProcess == CORE.Enums.ReadAndExcelProcessEnum.ProcessCompleted)
                    {
                        var res = educationAttendanceService.AgainReadClearProcessExcel(viewModel.ExcelData, row);
                        if (res.Status == StatusEnum.Error)
                        {
                            MessageBox.Show("Tekrar işlem excel temizleme sırasında hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            AppLogger.Error(DateTime.Now, nameof(frmEducationAttendance), nameof(grvList_KeyDown), nameof(educationAttendanceService.AgainReadClearProcessExcel), res.Message);
                            return;
                        }

                        viewModel.ExcelData = res.Data;
                    }

                    grvList.DeleteRow(rowHandle);
                    e.Handled = true;
                }
            }
        }

        private void grvList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            GridView view = sender as GridView;

            ReadAndExcelProcessEnum durum = (ReadAndExcelProcessEnum)view.GetRowCellValue(e.RowHandle, "ReadAndExcelProcess");

            if (durum == ReadAndExcelProcessEnum.ProcessCompleted)
            {
                e.Appearance.BackColor = Color.LightGreen;
                //e.Appearance.ForeColor = Color.White;
                e.HighPriority = true;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (viewModel.ExcelData is null)
            {
                MessageBox.Show("Excel verisi bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var file = Path.Combine(Path.GetTempPath(), $@"Rapor_{Guid.NewGuid()}.xlsx");

                File.WriteAllBytes(file, viewModel.ExcelData);

                Process.Start(new ProcessStartInfo
                {
                    FileName = file,
                    UseShellExecute = true // İşletim sisteminin varsayılan uygulamasını (Excel) kullanmasını sağlar
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel verisi açılırken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Error(DateTime.Now, nameof(frmEducationAttendance), nameof(btnExcel_Click), nameof(Process.Start), ex.Message);
            }
        }

        private void frmEducationAttendance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSave.PerformClick();
            }

            if (e.KeyCode == Keys.F3)
            {
                btnClose.PerformClick();
            }

            if (e.KeyCode == Keys.F12)
            {
                btnExcel.PerformClick();
            }
        }

        private void grvList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pmGrid.ShowPopup(MousePosition);
            }
        }

        private void bmGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Name == "btnAgainValidate")
            {
                var row = grvList.GetFocusedRow() as EducationAttendanceListModel;
                if (row != null)
                {
                    if (row.ReadAndExcelProcess == ReadAndExcelProcessEnum.ProcessCompleted)
                    {
                        if (ReadImagesAgainValidate(row) && MessageBox.Show("İlgili Görüntüye Ait Bilgiler Excel Dosyasından Silinecektir. İşleme Devam Etmek İstediğinize Emin Misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var frm = serviceProvider.GetRequiredService<frmImageReadConfirm>();
                            frm.imageInfo = row;
                            frm.excelData = viewModel.ExcelData;
                            frm.educator = slueEducator.Text;
                            frm.blAgainProcess = true;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                viewModel.ExcelData = frm.excelData;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("İlgili kayıtta daha önce hiç işlem gerçekleşmemiş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            if (e.Item.Name == "bbtnPreview")
            {
                var row = grvList.GetFocusedRow() as EducationAttendanceListModel;
                if (row != null)
                {
                    var frm = serviceProvider.GetRequiredService<frmImageReadConfirm>();
                    frm.imageInfo = row;
                    frm.excelData = viewModel.ExcelData;
                    frm.educator = slueEducator.Text;
                    frm.blPreview = true;
                    frm.ShowDialog();
                }
            }
        }

        private bool ReadImagesAgainValidate(EducationAttendanceListModel row)
        {
            bool blResult = true;

            try
            {
                if (Convert.ToInt32(slueEducator.EditValue) <= 0)
                    throw new Exception("Eğitmen Seçimi Yapılmalıdır.");

                if (!Enum.IsDefined(row.EducationType))
                    throw new Exception("Eğitim Türü Seçimi Yapılmalıdır.");

                if (!Enum.IsDefined(row.ModuleType))
                    throw new Exception("Modül Seçimi Yapılmalıdır.");

                if (row.EducationNumber <= 0)
                    throw new Exception("Eğitim Numarası Yazılmalıdır.");
            }
            catch (Exception ex)
            {
                blResult = false;
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return blResult;
        }
    }
}