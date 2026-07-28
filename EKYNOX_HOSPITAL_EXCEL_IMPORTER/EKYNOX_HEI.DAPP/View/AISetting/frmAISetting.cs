using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.AISetting;
using Google.GenAI;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using static DevExpress.DataProcessing.InMemoryDataProcessor.AddSurrogateOperationAlgorithm;

namespace EKYNOX_HEI.DAPP.View.AISetting
{
    public partial class frmAISetting : DevExpress.XtraEditors.XtraForm
    {
        private AISettingModel aiSetting;

        public frmAISetting()
        {
            InitializeComponent();
            aiSetting = new AISettingModel();
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
                              Method = c,
                              Name = c.GetCustomAttribute<DisplayAttribute>()!.Name,
                          }).ToList();

            lueMethod.Properties.DataSource = methods;
            lueAIUsingStatus.Properties.ValueMember = "Method";
            lueAIUsingStatus.Properties.DisplayMember = "Name";

            grdList.DataSource = new BindingList<AISettingListModel>(aiSetting.Detail);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void lueAI_SelectionChanged(object sender, DevExpress.XtraEditors.Controls.PopupSelectionChangedEventArgs e)
        {
            SetAIModels();

            if (!string.IsNullOrEmpty(lueAI.EditValue?.ToString()) && (AIEnum)lueAI.EditValue == AIEnum.AzureAI)
            {
                lueEndpoint.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                lueEndpoint.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

        }

        private void teApiKey_TextChanged(object sender, EventArgs e)
        {
            SetAIModels();
        }

        private async void SetAIModels()
        {
            if (lueAI.EditValue != null && string.IsNullOrEmpty(teApiKey.Text))
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

                        break;
                    default:
                        break;
                }

                repSlueAIModels.DataSource = dt;
                repSlueAIModels.ValueMember = "Name";
                repSlueAIModels.DisplayMember = "Description";
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

        }
    }
}