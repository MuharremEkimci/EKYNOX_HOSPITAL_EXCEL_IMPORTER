using DevExpress.Mvvm.POCO;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.DAPP.Controller;
using EKYNOX_HEI.DAPP.View.AISetting;
using EKYNOX_HEI.DATA.Database;
using EKYNOX_HEI.DATA.DataModel;
using EKYNOX_HEI.DATA.DataModel.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly DatabaseContext context;
        private readonly IServiceProvider serviceProvider;
        private readonly UserInfoSet userInfo;
        public frmMain(IServiceProvider serviceProvider, DatabaseContext _context, UserInfoSet _userInfo)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.context = _context;
            this.userInfo = _userInfo;
        }

        private static void OpenForm(Form form, bool showDialog = false, bool isChildForm = true)
        {
            if (form != null)
            {
                if (showDialog)
                    form.ShowDialog();
                else
                {
                    if (isChildForm)
                        form.MdiParent = Application.OpenForms["frmMain"] as frmMain;

                    form.Show();
                }
            }
            else
                form.BringToFront();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SkinHelper.InitSkinGallery(rgbbTemalar, true);

            var aiSettings = context.AISetting.Where(c => c.USINGSTATUS == CORE.Enums.AIEnumUsingStatus.Using).ToList();
            var aiSettingsDetail = context.AISettingDetail.ToList();

            var joinData = aiSettings.GroupJoin
                           (
                                aiSettingsDetail,
                                ai => ai.LOGICALREF,
                                aiDetail => aiDetail.AISETTINGREF,
                                (ai, aiDetail) => new { ai, aiDetail }
                           ).ToList();

            btnArtificialIntelligence.BeginUpdate();

            foreach (var item in joinData)
            {
                var barSubItem = new BarSubItem();
                barSubItem.Name = $@"btnSi{item.ai.AI.GetType()}";
                barSubItem.Caption = EnumHelper.GetDisplayName(item.ai.AI);
                ribbon.Items.Add(barSubItem);

                foreach (var aiDet in item.aiDetail)
                {
                    dynamic aiInfo = new ExpandoObject();
                    aiInfo.ApiKey = item.ai.APIKEY;
                    aiInfo.AiModelName = aiDet.AIMODELNAME;
                    aiInfo.Endpoint = item.ai.ENDPOINT;
                    aiInfo.AiType = item.ai.AI;

                    var barButtonItem = new BarButtonItem();
                    barButtonItem.Name = $"btnBi{aiDet.LOGICALREF}";
                    barButtonItem.Caption = $@"{aiDet.AIMODELNAME.Replace("models/","")}";
                    barButtonItem.Tag = aiInfo;
                    barButtonItem.ItemClick += BarButtonItem_ItemClick;
                    barSubItem.AddItem(barButtonItem);
                    ribbon.Items.Add(barButtonItem);
                }

                btnArtificialIntelligence.AddItem(barSubItem);
            }
          
            btnArtificialIntelligence.EndUpdate();
        }

        private void btnAppExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void btnUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (userInfo.Role == CORE.Enums.RoleEnum.User)
            {
                MessageBox.Show("Giriş yetkiniz bulunmamaktadır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var frm = serviceProvider.GetRequiredService<frmUsers>();
            OpenForm(frm, true, false);
        }

        private void btnInstitutions_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = serviceProvider.GetRequiredService<frmInstitutions>();
            OpenForm(frm, true, false);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (XtraMessageBox.Show("Programdan Çıkılacaktır.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnEducationAttendandce_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = serviceProvider.GetRequiredService<frmEducationAttendanceList>();
            OpenForm(frm);
        }

        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var frm = serviceProvider.GetRequiredService<frmAISettingList>();
            OpenForm(frm);
        }

        private void btnArtificialIntelligence_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void BarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Tag is IDictionary<string, object> aiDet)
            {
                var frm = new frmAIChat();
                frm.aiModel = aiDet["AiModelName"]?.ToString()??"";
                frm.apiKey = aiDet["ApiKey"]?.ToString()??"";
                frm.endpoint = aiDet["Endpoint"]?.ToString() ?? "";
                frm.aiType = (AIEnum)aiDet["AiType"];
                frm.ShowDialog();
            }
        }
    }
}