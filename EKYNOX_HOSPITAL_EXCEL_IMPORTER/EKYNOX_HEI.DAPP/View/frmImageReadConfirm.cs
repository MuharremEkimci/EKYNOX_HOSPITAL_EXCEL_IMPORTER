using DevExpress.XtraEditors;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.EducationAttendance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmImageReadConfirm : DevExpress.XtraEditors.XtraForm
    {
        public EducationAttendanceListModel imageInfo;

        public frmImageReadConfirm()
        {
            InitializeComponent();
            imageInfo = new EducationAttendanceListModel();
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

            grdImageReadList.DataSource = imageInfo.Detail;

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

            string prompt = @"Sen profesyonel bir optik karakter tanıma (OCR) asistanısın. 
            Görseldeki el yazısı katılım listesini incele ve katılan kişilerin ad ve soyadlarını ayıkla.
            
            GÖREVLER VE KURALLAR:
            1. Görseldeki el yazılarını azami dikkatle oku ve doğru tahmin et.
            2. SADECE kişilerin İSİM ve SOYİSİMLERİNİ al. Tablodaki Birim (Hostes, Vezne vb.), Tarih, Döküman No ve İmza alanlarını KESİNLİKLE dahil etme.
            3. İsim ve soyisimleri Türkçe karakter kurallarına uygun olarak TÜMÜ BÜYÜK HARFLERLE yaz (Örn: İSMEK -> İSMEK, ı -> I).
            4. İsim ve soyisimi ayrıştırarak şablona yerleştir.
            
            HEDEF JSON ŞEMASI:
            {
              ""participants"": [
                {
                  ""class_no"": 1,
                  ""name"": ""MUSA"",
                  ""surname"": ""TUNÇ""
                }
              ]
            }";

            var aiApp = new AIHelper("AIzaSyAaEnYOkEPAvqysMM6HkhaE6l8HJBPJ7UU");
            var result = await aiApp.GeminiAIQuestion(prompt, new { aiModelNames = new List<string>(), imageBytes = imageInfo.FileData, imageMimeType = imageInfo.FileMimeType }, true , true);
            var sdasd = result.Data;
            
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
            if (MessageBox.Show("İşlemden Vazgeçilecektir.","Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}