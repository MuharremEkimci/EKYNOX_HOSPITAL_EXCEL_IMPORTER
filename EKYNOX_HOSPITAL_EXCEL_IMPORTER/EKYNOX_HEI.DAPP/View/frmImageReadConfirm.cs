using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.EducationAttendance;
using EKYNOX_HEI.DAPP.Controller;
using EKYNOX_HEI.DAPP.View.Common;
using GenerativeAI.Types;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmImageReadConfirm : DevExpress.XtraEditors.XtraForm
    {
        public EducationAttendanceListModel imageInfo;
        private List<EducationAttendanceExcelReadModel> excelReadList;
        private readonly clsEducationAttendance educationAttendanceService;
        public byte[]? excelData;
        public string educator;
        public bool blAgainProcess;
        public bool blPreview;

        public frmImageReadConfirm(clsEducationAttendance _educationAttendanceService)
        {
            InitializeComponent();
            imageInfo = new EducationAttendanceListModel();
            educationAttendanceService = _educationAttendanceService;
            educator = "";
            excelReadList = new List<EducationAttendanceExcelReadModel>();
        }

        private async void frmImageReadConfirm_Load(object sender, EventArgs e)
        {
            lueModule.Properties.DataSource = EnumHelper.GetDisplayValues(typeof(ModuleTypeEnum));
            lueModule.Properties.ValueMember = "Id";
            lueModule.Properties.DisplayMember = "Name";

            lueEducationType.Properties.DataSource = EnumHelper.GetDisplayValues(typeof(EducationTypeEnum));
            lueEducationType.Properties.ValueMember = "Id";
            lueEducationType.Properties.DisplayMember = "Name";

            deEducationDate.DateTime = imageInfo.EducationDate;
            lueEducationType.EditValue = imageInfo.EducationType.GetHashCode();
            lueModule.EditValue = imageInfo.ModuleType.GetHashCode();
            seEducationNumber.EditValue = imageInfo.EducationNumber;
            peMain.Image = Image.FromStream(new System.IO.MemoryStream(imageInfo.FileData));

            //var ocrApp = new HandWritingOcrApp("https://readpaper.cognitiveservices.azure.com/", "9BAfnhkgsEGAAgmcpur4JZREmllvBUjx36a5lUu78EvcHTagoNolJQQJ99CGACYeBjFXJ3w3AAAFACOGTvuZ");
            //var dsd = ocrApp.OcrProcess(imageInfo.FileData);

            if (blAgainProcess)
            {
                var res = educationAttendanceService.AgainReadClearProcessExcel(excelData, imageInfo);
                if (res.Status == StatusEnum.Error)
                {
                    MessageBox.Show("Tekrar işlem excel temizleme sırasında hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AppLogger.Error(DateTime.Now, nameof(frmImageReadConfirm), nameof(frmImageReadConfirm_Load), nameof(educationAttendanceService.AgainReadClearProcessExcel), res.Message);
                    this.Close();
                    return;
                }

                excelData = res.Data;
            }

            if (imageInfo.LogicalRef > 0 || imageInfo.Detail.Any())
            {
                grdImageReadList.DataSource = imageInfo.Detail;           
            }
            else
            {
                //var handle = SplashScreenManager.ShowOverlayForm(this);
                //SplashScreenManager.Default.SetWaitFormCaption("AI Görüntü Okuma İşlemi Sağlanıyor...");
                //var res = await educationAttendanceService.GetImageReadAI(imageInfo.FileData, imageInfo.FileMimeType);
                //SplashScreenManager.CloseOverlayForm(handle);

                this.Enabled = false;
                SplashScreenManager.ShowForm(this,typeof(frmWaitingForm));
                SplashScreenManager.Default.SetWaitFormCaption("AI Görüntü Okuma");
                SplashScreenManager.Default.SetWaitFormDescription("Lütfen bekleyiniz...");
                var res = await educationAttendanceService.GetImageReadAI(imageInfo.FileData, imageInfo.FileMimeType);
                SplashScreenManager.CloseForm();
                this.Enabled = true;
                if (res.Status == StatusEnum.Warning)
                {
                    MessageBox.Show(res.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                if (res.Status == StatusEnum.Error)
                {
                    MessageBox.Show("Yapay zeka işleminde hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AppLogger.Error(DateTime.Now, nameof(frmImageReadConfirm), nameof(frmImageReadConfirm_Load), nameof(educationAttendanceService.GetImageReadAI), res.Message);
                    this.Close();
                    return;
                }

                imageInfo.Detail = res.Data;
                grdImageReadList.DataSource = imageInfo.Detail;
            }

            grvImageReadList.Columns["ClassNo"].Width = 50;
            grvImageReadList.Columns["ClassNo"].AppearanceCell.Font = new Font(grvImageReadList.Appearance.Row.Font, FontStyle.Bold);
            grvImageReadList.Columns["ClassNo"].AppearanceHeader.Font = new Font(grvImageReadList.Appearance.Row.Font, FontStyle.Bold);
            grvImageReadList.Columns["Name"].ColumnEdit = repNameSurname;
            grvImageReadList.Columns["Surname"].ColumnEdit = repNameSurname;

            grvImageReadList.Columns["Name"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            grvImageReadList.Columns["Surname"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            grvImageReadList.Columns["Surname"].SortIndex = 1;

            var readRes = educationAttendanceService.ReadExcel(imageInfo.ModuleType, excelData);
            if (readRes.Status == StatusEnum.Error)
            {
                MessageBox.Show("Excel okuma işleminde hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Error(DateTime.Now, nameof(frmImageReadConfirm), nameof(frmImageReadConfirm_Load), nameof(educationAttendanceService.ReadExcel), readRes.Message);
                this.Close();
                return;
            }

            excelReadList = readRes.Data.OrderBy(c => c.Name).ThenBy(c => c.Surname).ToList();
            grdExcelList.DataSource = excelReadList;

            if (blPreview)
            {
                grdImageReadList.Enabled = false;
                deEducationDate.Enabled = false;
                lueEducationType.Enabled = false;
                lueModule.Enabled = false;
                seEducationNumber.Enabled = false;
                btnConfirm.Enabled = false;
            }
        }

        private async void frmImageReadConfirm_Shown(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            imageInfo.EducationDate = deEducationDate.DateTime;
            imageInfo.EducationType = (EducationTypeEnum)lueEducationType.EditValue;
            imageInfo.ModuleType = (ModuleTypeEnum)lueModule.EditValue;
            imageInfo.EducationNumber = Convert.ToInt32(seEducationNumber.EditValue);
            imageInfo.ReadAndExcelProcess = ReadAndExcelProcessEnum.ProcessCompleted;

            var file = Path.Combine(AppContext.BaseDirectory, "Files/Extension", "Temp.xlsx");
            byte[] excelPostData = excelData != null ? excelData : File.ReadAllBytes(file);

            SplashScreenManager.ShowForm(typeof(frmWaitingForm));
            SplashScreenManager.Default.SetWaitFormCaption("Excel Yazma İşlemi Sağlanıyor...");
            var excelRes = educationAttendanceService.WriteExcel(excelPostData, imageInfo, educator);
            SplashScreenManager.CloseForm();
            if (excelRes.Status == StatusEnum.Warning)
            {
                MessageBox.Show(excelRes.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            if (excelRes.Status == StatusEnum.Error)
            {
                MessageBox.Show("Excel yazma işleminde hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Error(DateTime.Now, nameof(frmImageReadConfirm), nameof(frmImageReadConfirm_Load), nameof(educationAttendanceService.WriteExcel), excelRes.Message);
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            imageInfo.ReadAndExcelProcess = ReadAndExcelProcessEnum.ProcessCompleted;
            excelData = excelRes.Data;
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("İşlemden Vazgeçilecektir.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void grvImageReadList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Name" || e.Column.FieldName == "Surname")
            {
                grvImageReadList.RefreshRow(e.RowHandle);
                grvExcelList.RefreshData();
            }
        }

        private void grvImageReadList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            GridView view = sender as GridView;

            string name = (string)view.GetRowCellValue(e.RowHandle, "Name");
            string surname = (string)view.GetRowCellValue(e.RowHandle, "Surname");

            if (excelReadList.Any(c => c.Name == name && c.Surname == surname))
            {
                e.Appearance.BackColor = Color.LightGreen;
                //e.Appearance.ForeColor = Color.White;
                e.HighPriority = true;
            }
        }

        private void grvExcelList_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;

            GridView view = sender as GridView;

            string name = (string)view.GetRowCellValue(e.RowHandle, "Name");
            string surname = (string)view.GetRowCellValue(e.RowHandle, "Surname");

            if (imageInfo.Detail.Any(c => c.Name == name && c.Surname == surname))
            {
                e.Appearance.BackColor = Color.LightGreen;
                //e.Appearance.ForeColor = Color.White;
                e.HighPriority = true;
            }
        }

        private void grvImageReadList_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }


    }
}