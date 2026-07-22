using DevExpress.XtraEditors;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.DAPP.Controller;
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
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private readonly clsUsers userService;
        private readonly IServiceProvider serviceProvider;

        public frmLogin(IServiceProvider serviceProvider, clsUsers _userService)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.userService = _userService;
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.Location = new Point(
    (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
    (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            txtUserName.Text = Properties.Settings.Default.LastUserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var loginResult = userService.LoginControl(txtUserName.Text, txtPassword.Text);
            if (loginResult.Status == StatusEnum.Warning)
            {
                MessageBox.Show(loginResult.Message, "Giriş Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (loginResult.Status == StatusEnum.Error)
            {
                MessageBox.Show("Giriş yapılırken hata meydana geldi.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Error($@"ProcessDate: {DateTime.Now}, ProcessLocation: {nameof(frmLogin)}, Module: {nameof(btnLogin_Click)} - Hata Detayı: {loginResult.Message}");
                return;
            }

            clsMain.userInfo = loginResult.Data;
            Properties.Settings.Default.LastUserName = txtUserName.Text;
            Properties.Settings.Default.RememberUser = true;
            Properties.Settings.Default.Save();
            var mainForm = serviceProvider.GetService(typeof(frmMain)) as frmMain;
            mainForm.Show();
            this.Hide();
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}