using DevExpress.XtraSplashScreen;
using EKYNOX_HEI.DAPP.Controller;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmLoading : SplashScreen
    {
        private readonly clsUsers userService;
        private readonly IServiceProvider serviceProvider;

        public frmLoading(IServiceProvider serviceProvider, clsUsers _userService)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.userService = _userService;
            this.labelCopyright.Text = "Copyright © 2025-" + DateTime.Now.Year.ToString();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        private async void SplashScreen1_Load(object sender, EventArgs e)
        {
            await Task.Delay(5000);
            userService.AdminUserControl();
            var frm = serviceProvider.GetRequiredService<frmLogin>();
            frm.Show();
            this.Hide();
        }

        private void frmLoading_Shown(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        public enum SplashScreenCommand
        {
        }
    }
}