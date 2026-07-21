using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
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
        private readonly IServiceProvider serviceProvider;

        public frmMain(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
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
            if (XtraMessageBox.Show("Programdan Çıkılacaktır.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnUsers_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnInstitutions_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = serviceProvider.GetRequiredService<frmInstitutions>();
            OpenForm(frm, true, false);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("Programdan Çıkılacaktır.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}