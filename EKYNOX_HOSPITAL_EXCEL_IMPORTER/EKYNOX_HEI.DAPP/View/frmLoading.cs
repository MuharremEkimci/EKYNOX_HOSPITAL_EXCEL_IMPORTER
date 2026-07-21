using DevExpress.XtraSplashScreen;
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
        private readonly IServiceProvider serviceProvider;

        public frmLoading(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
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
            var frm = serviceProvider.GetRequiredService<frmMain>();
            frm.Show();
            this.Hide();
        }

        public enum SplashScreenCommand
        {
        }
    }
}