using Azure;
using Azure.AI.OpenAI;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.CognitiveServices;
using DevExpress.Mvvm.Native;
using DevExpress.Office.DigitalSignatures;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.AISetting;
using EKYNOX_HEI.DAPP.Controller;
using Google.GenAI;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using static DevExpress.DataProcessing.InMemoryDataProcessor.AddSurrogateOperationAlgorithm;

namespace EKYNOX_HEI.DAPP.View.AISetting
{
    public partial class frmAISetting : DevExpress.XtraEditors.XtraForm
    {
        private AISettingModel aiSetting;
        private BindingList<AISettingListModel> aiSettingList;
        public AISettingListViewModel listInfo;
        private readonly clsAISetting AISettingService;
        private readonly IServiceProvider serviceProvider;

        public frmAISetting(clsAISetting _AISettingService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            aiSetting = new AISettingModel();
            listInfo = new AISettingListViewModel();
            AISettingService = _AISettingService;
            this.serviceProvider = serviceProvider;
            aiSettingList = new BindingList<AISettingListModel>(new List<AISettingListModel>());
        }

        private void frmAISetting_Load(object sender, EventArgs e)
        {

            lueAI.Properties.DataSource = EnumHelper.GetDisplayValues(typeof(AIEnum));
            lueAI.Properties.ValueMember = "Id";
            lueAI.Properties.DisplayMember = "Name";

            lueAIUsingStatus.Properties.DataSource = EnumHelper.GetDisplayValues(typeof(AIEnumUsingStatus));
            lueAIUsingStatus.Properties.ValueMember = "Id";
            lueAIUsingStatus.Properties.DisplayMember = "Name";

            var methods = typeof(AIHelper)
                          .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                          .Where(c => c.GetCustomAttribute<DisplayAttribute>() != null)
                          .Select(c => new
                          {
                              Method = c.Name,
                              Name = c.GetCustomAttribute<DisplayAttribute>()!.Description,
                          }).ToList();

            lueMethod.Properties.DataSource = methods;
            lueMethod.Properties.ValueMember = "Method";
            lueMethod.Properties.DisplayMember = "Name";

            lueAIUsingStatus.EditValue = AIEnumUsingStatus.Using.GetHashCode();

            if (listInfo.LogicalRef > 0)
            {
                var getData = AISettingService.GetAISetting(listInfo.LogicalRef);
                if (getData.Status == StatusEnum.Warning)
                {
                    MessageBox.Show(getData.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.DialogResult = DialogResult.Cancel;
                }

                if (getData.Status == StatusEnum.Error)
                {
                    MessageBox.Show("AI ayar listesi veritabanından getirilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AppLogger.Error(DateTime.Now, nameof(frmAISetting), nameof(frmAISetting_Load), nameof(AISettingService.GetAISetting), getData.Message);
                }

                var resData = getData.Data;

                teEndPoint.Text = resData.Endpoint;
                teNo.Text = resData.AiNo;
                lueAI.EditValue = resData.Ai.GetHashCode();
                lueAIUsingStatus.EditValue = resData.UsingStatus.GetHashCode();
                lueMethod.EditValue = resData.MethodName;
                teApiKey.Text = resData.ApiKey;
                SetAIModels();
                aiSetting.Detail = resData.Detail;

                aiSettingList = new BindingList<AISettingListModel>(aiSetting.Detail);
                grdList.DataSource = aiSettingList;
            }
            else
            {
                teNo.Text = AISettingService.CreateAINo().Data;
            }

            grdList.DataSource = aiSettingList;
            grvList.Columns["AiModelName"].ColumnEdit = repSlueAIModels;
            grvList.Columns["AiModelDesc"].ColumnEdit = repSlueAIModelsDesc;
            grvList.Columns["AiModelTest"].ColumnEdit = repBeTestModel;
            grvList.Columns["LineNr"].Width = 15;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void lueAI_SelectionChanged(object sender, DevExpress.XtraEditors.Controls.PopupSelectionChangedEventArgs e)
        {


        }

        private void teApiKey_TextChanged(object sender, EventArgs e)
        {
            SetAIModels();
        }

        private async void SetAIModels()
        {
            if (lueAI.EditValue != null && !string.IsNullOrEmpty(teApiKey.Text))
            {
                var dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("DisplayName", typeof(string));


                switch ((AIEnum)lueAI.EditValue)
                {
                    case AIEnum.Gemini:
                        var googleAI = new Client(apiKey: teApiKey.Text);
                        var aiModels = await googleAI.Models.ListAsync();
                        var aiList = await aiModels.Select(c => new { Name = c.Name, Description = c.Description, DisplayName = c.DisplayName }).ToListAsync();

                        foreach (var c in aiList)
                            dt.Rows.Add(c.Name, c.Description, c.DisplayName);

                        break;
                    case AIEnum.AzureAI:

                        string endpoint = teEndPoint.Text;
                        string apiKey = teApiKey.Text;
                        string apiVersion = "2024-10-21";

                        var res = await AISettingService.GetAIModelRequest(AIEnum.AzureAI,$"{endpoint}/openai/models?api-version={apiVersion}", apiKey);
                        if (res.Status == StatusEnum.Error)
                        {
                            MessageBox.Show("Model bilgileri getirilirken hata oluştu. Api Key bilginizi doğru girdiğinizden emin olunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        foreach (var model in res.Data.RootElement.GetProperty("data").EnumerateArray())
                        {
                            string id = model.GetProperty("id").GetString();
                            dt.Rows.Add(id, id, id);
                        }

                        break;

                    case AIEnum.Groq:
                        apiKey = teApiKey.Text;
                        endpoint = teEndPoint.Text;

                        res = await AISettingService.GetAIModelRequest(AIEnum.Groq, $"{endpoint}/models", apiKey);
                        if (res.Status == StatusEnum.Error)
                        {
                            MessageBox.Show("Model bilgileri getirilirken hata oluştu. Api Key bilginizi doğru girdiğinizden emin olunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        foreach (var model in res.Data.RootElement.GetProperty("data").EnumerateArray())
                        {
                            string id = model.GetProperty("id").GetString();
                            string ownedBy = model.GetProperty("owned_by").GetString();
                            dt.Rows.Add(id, ownedBy, ownedBy);
                        }

                        break;
                    default:
                        break;
                }

                repSlueAIModels.DataSource = dt;
                repSlueAIModels.ValueMember = "Name";
                repSlueAIModels.DisplayMember = "Name";

                repSlueAIModelsDesc.DataSource = dt;
                repSlueAIModelsDesc.ValueMember = "Name";
                repSlueAIModelsDesc.DisplayMember = "Description";
            }
        }

        private void repBeTestModel_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as AISettingListModel;

                if (row != null)
                {
                    if (string.IsNullOrEmpty(row.AiModelName))
                    {
                        MessageBox.Show("İlgili satırda model seçimi yapılmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    var frm = new frmAIChat();
                    frm.aiModel = row.AiModelName;
                    frm.apiKey = teApiKey.Text;
                    frm.endpoint = teEndPoint.Text;
                    frm.aiType = (AIEnum)lueAI.EditValue;
                    frm.ShowDialog();
                }
            }
        }

        private void lueAI_EditValueChanged(object sender, EventArgs e)
        {
            if (aiSettingList.Any() && MessageBox.Show("Yapay zeka değişikliği tespit edildi. Model listesi sıfırlanacaktır.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                aiSettingList.Clear();

            SetAIModels();

            if (!string.IsNullOrEmpty(lueAI.EditValue?.ToString()) && ((AIEnum)lueAI.EditValue is AIEnum.AzureAI or AIEnum.Groq))
            {
                lueEndpoint.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lueEndpoint.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void frmAISetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSave.PerformClick();
            }

            if (e.KeyCode == Keys.F3)
            {
                btnClose.PerformClick();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                aiSetting.LogicalRef = listInfo.LogicalRef;
                aiSetting.UsingStatus = (AIEnumUsingStatus)lueAIUsingStatus.EditValue;
                aiSetting.AiNo = AISettingService.CreateAINo().Data;
                aiSetting.Ai = (AIEnum)lueAI.EditValue;
                aiSetting.MethodName = lueMethod.EditValue.ToString();
                aiSetting.Endpoint = teEndPoint.Text;
                aiSetting.ApiKey = teApiKey.Text.Trim();
                aiSetting.Detail = aiSettingList.ToList();

                if (listInfo.LogicalRef > 0)
                {
                    var res = AISettingService.Update(aiSetting);
                    if (res.Status == StatusEnum.Error)
                    {
                        MessageBox.Show("AI ayarları güncellenirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        AppLogger.Error(DateTime.Now, nameof(frmAISetting), nameof(btnSave_Click), nameof(AISettingService.Update), res.Message);
                        return;
                    }
                }
                else
                {
                    var res = AISettingService.Save(aiSetting);
                    if (res.Status == StatusEnum.Error)
                    {
                        MessageBox.Show("AI ayarları kaydedilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        AppLogger.Error(DateTime.Now, nameof(frmAISetting), nameof(btnSave_Click), nameof(AISettingService.Save), res.Message);
                        return;
                    }
                }

                MessageBox.Show("İşlem Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        bool Validate()
        {
            var blResult = true;

            try
            {
                if (Convert.ToInt32(lueAI.EditValue) <= 0)
                    throw new Exception("Yapay zeka seçilmelidir.");

                if (string.IsNullOrEmpty(teApiKey.Text))
                    throw new Exception("Api key girilmelidir.");

                if (string.IsNullOrEmpty(lueMethod.EditValue?.ToString()))
                    throw new Exception("Uygulama içi işlem yapacak method seçilmelidir.");

                if (((AIEnum)lueAI.EditValue is AIEnum.AzureAI or AIEnum.Groq) && string.IsNullOrEmpty(teEndPoint.Text))
                    throw new Exception("Yapay zeka AzureAI veya Groq seçildiğinde endpoint girilmesi zorunludur.");

                if (!aiSettingList.Any())
                    throw new Exception("Model listesinde giriş yapılmalıdır.");

                if (!aiSettingList.GroupBy(c => c.AiModelName).Count().Equals(aiSettingList.Count()))
                    throw new Exception("Satırlarda aynı model tekrar seçilemez.");

                if (true)
                {

                }
            }
            catch (Exception ex)
            {
                blResult = false;
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return blResult;
        }

        private void grvList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "AiModelName")
            {
                var row = grvList.GetRow(e.RowHandle) as AISettingListModel;

                if (row != null)
                {
                    row.AiModelDesc = row.AiModelName;
                    grdList.RefreshDataSource();
                }
            }
        }

        private void grvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Delete)
            {
                int rowHandle = grvList.FocusedRowHandle;

                if (rowHandle >= 0 && MessageBox.Show("Satır Silinecektir.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    grvList.DeleteRow(rowHandle);

                    grdList.RefreshDataSource();
                    grvList.RefreshData();
                    e.Handled = true;


                    aiSettingList.OrderBy(c => c.LineNr).ToList().ForEach(c => c.LineNr = aiSettingList.ToList().IndexOf(c) + 1);

                }
            }
        }

        private void grvList_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvList.SetRowCellValue(e.RowHandle, "LineNr", grvList.DataRowCount + 1);
            grdList.RefreshDataSource();
            grvList.RefreshData();
        }
    }
}