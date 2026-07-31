using DevExpress.XtraEditors;
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
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmImageReadConfirm : DevExpress.XtraEditors.XtraForm
    {
        public EducationAttendanceListModel imageInfo;
        private readonly clsEducationAttendance educationAttendanceService;
        public byte[]? excelData;
 
        public frmImageReadConfirm(clsEducationAttendance _educationAttendanceService)
        {
            InitializeComponent();
            imageInfo = new EducationAttendanceListModel();
            educationAttendanceService = _educationAttendanceService;
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

            //var prompt = $@"
            //                 1. Bu görseldeki el yazılarını oku.
            //                 2. Sadece katılımcıların İSİM ve SOYİSİMLERİNİ ayıkla ve TÜRKÇE BÜYÜK HARFLERLE yaz.
            //                 3. El yazılarını dikkatli oku.
            //                 4. El yazılarını dikkatli okuyarak yüksek tahminde bulun. saçmalama.
            //                 5. Birim, Tarih, İmzalar gibi detayları ekleme.
            //                 6. Çıktı sadece json formatında ver. Herhangi bir yorum ekleme sadece json çıktı ver.
            //                 7. ```json kullanma.
            //                 8. Markdown kullanma.
            //                 9. Açıklama yazma.
            //                 10. Ekstra metin yazma. 
            //                 11. Tekrar ediyorum. SADECE geçerli JSON döndür. Markdown kullanma. ```json kullanma. Açıklama yazma. Ekstra metin yazma.
            //                 12. Tekrar söylüyorum. SADECE geçerli JSON döndür. Markdown kullanma. ```json kullanma. Açıklama yazma. Ekstra metin yazma.
            //                 13. Çıktıyı SADECE JSON formatında ver:
            //                 {{
            //                   """"participants"""": [
            //                     {{
            //                       """"class_no"""": 1,
            //                       """"name"""": """"İSİM"""",
            //                       """"surname"""": """"SOYİSİM""""
            //                     }}
            //                   ]
            //                 }}";

            if (imageInfo.LogicalRef > 0 || imageInfo.Detail.Any())
            {
                grdImageReadList.DataSource = imageInfo.Detail;
                grvImageReadList.Columns["ClassNo"].Width = 45;
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(frmWaitingForm));
                var res = await educationAttendanceService.GetImageReadAI(imageInfo.FileData, imageInfo.FileMimeType);
                SplashScreenManager.CloseForm();
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
                grvImageReadList.Columns["ClassNo"].Width = 45;
            }

            var readRes = educationAttendanceService.ReadExcel(imageInfo.ModuleType, excelData);
            if (readRes.Status == StatusEnum.Error)
            {
                MessageBox.Show("Excel okuma işleminde hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Error(DateTime.Now, nameof(frmImageReadConfirm), nameof(frmImageReadConfirm_Load), nameof(educationAttendanceService.ReadExcel), readRes.Message);
                this.Close();
                return;
            }

            grdExcelList.DataSource = readRes.Data;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            imageInfo.EducationDate = deEducationDate.DateTime;
            imageInfo.EducationType = (EducationTypeEnum)lueEducationType.EditValue;
            imageInfo.ModuleType = (ModuleTypeEnum)lueModule.EditValue;
            imageInfo.EducationNumber = Convert.ToInt32(seEducationNumber.EditValue);
            imageInfo.ReadAndExcelProcess = ReadAndExcelProcessEnum.ProcessCompleted;
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
                grvImageReadList.SetRowCellValue(e.RowHandle, e.Column, ((string)e.Value).ToUpper(new CultureInfo("tr-TR")));
        }
    }
}