using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using EKYNOX_HEI.DAPP.Controller;
using EKYNOX_HEI.DAPP.View.AISetting;
using EKYNOX_HEI.DATA.Database;
using EKYNOX_HEI.DATA.DataModel.Common;
using Microsoft.Extensions.DependencyInjection;
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
    }
}