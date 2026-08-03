using Azure;
using Azure.AI.OpenAI;
using DevExpress.AIIntegration;
using DevExpress.AIIntegration.WinForms.Chat;
using DevExpress.Blazor.Popup.Internal;
using DevExpress.XtraCharts;
using EKYNOX_HEI.CORE.Enums;
using GenerativeAI.Microsoft;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmAIChat : DevExpress.XtraEditors.XtraForm
    {
        public string apiKey;
        public string aiModel;
        public string endpoint;
        public AIEnum aiType;
        IChatClient chatClient;

        public frmAIChat()
        {
            InitializeComponent();
        }

        private async void frmAIChat_Load(object sender, EventArgs e)
        {

            SetAI();

            aiChatControl.FileUploadEnabled = DevExpress.Utils.DefaultBoolean.True;

            aiChatControl.OptionsFileUpload.FileTypeFilter.AddRange(new List<string>
            {
                "text/plain",
                "application/pdf",
                "image/png",
                "image/jpeg"
            });

            aiChatControl.OptionsFileUpload.AllowedFileExtensions.AddRange(new List<string>
{
    ".txt", ".pdf", ".png", ".jpeg"
});

            aiChatControl.OptionsFileUpload.MaxFileCount = 5;
            aiChatControl.OptionsFileUpload.MaxFileSize = 5 * 1024 * 1024;

            this.Text = $"AI Chat - AI: {CORE.Helpers.EnumHelper.GetDisplayName(aiType)} - Model: {aiModel}";
        }

        private void frmAIChat_FormClosed(object sender, FormClosedEventArgs e)
        {
            AIExtensionsContainerDesktop.Default.UnregisterChatClient();
            chatClient = null;
        }

        private void SetAI() 
        {
            switch (aiType)
            {
                case AIEnum.Gemini:
                    chatClient = new GenerativeAIChatClient
                         (
                             apiKey,
                             aiModel
                         );

                    DevExpress.AIIntegration.AIExtensionsContainerDesktop.Default.RegisterChatClient(chatClient);
                    break;
                case AIEnum.AzureAI:
                    chatClient =
                    new AzureOpenAIClient
                    (
                        new Uri(endpoint),
                        new System.ClientModel.ApiKeyCredential(apiKey)
                    ).GetChatClient(aiModel).AsIChatClient();

                    AIExtensionsContainerDesktop.Default.RegisterChatClient(chatClient);
                    break;
                case AIEnum.Groq:
                    chatClient =
                    new OpenAIClient
                    (
                        new ApiKeyCredential(apiKey),
                        new OpenAIClientOptions
                        {
                            Endpoint = new Uri(endpoint)
                        }
                    ).GetChatClient(aiModel).AsIChatClient();

                    AIExtensionsContainerDesktop.Default.RegisterChatClient(chatClient);
                    break;
                default:
                    break;
            }
        }
    }
}