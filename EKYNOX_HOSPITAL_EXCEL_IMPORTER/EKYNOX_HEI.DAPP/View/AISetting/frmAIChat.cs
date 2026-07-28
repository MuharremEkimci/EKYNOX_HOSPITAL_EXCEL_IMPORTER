using Azure;
using Azure.AI.OpenAI;
using DevExpress.AIIntegration;
using DevExpress.AIIntegration.WinForms.Chat;
using EKYNOX_HEI.CORE.Enums;
using GenerativeAI.Microsoft;
using Microsoft.Extensions.AI;
using System.ClientModel;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmAIChat : DevExpress.XtraEditors.XtraForm
    {
        private AIChatControl chatControl;
        public string apiKey;
        public string aiModel;
        public string endpoint;
        public AIEnum aiType;

        public frmAIChat()
        {

            InitializeComponent();

            IChatClient chatClient;

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
                default:
                    break;
            }

            chatControl = new AIChatControl
            {
                Dock = DockStyle.Fill,
            };

            this.Controls.Add(chatControl);
        }

        private async void frmAIChat_Load(object sender, EventArgs e)
        {

        }
    }
}