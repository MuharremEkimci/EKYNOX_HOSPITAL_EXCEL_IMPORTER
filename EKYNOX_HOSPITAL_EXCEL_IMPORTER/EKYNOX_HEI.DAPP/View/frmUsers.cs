using DevExpress.XtraEditors;
using EKYNOX_HEI.CORE.Enums;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.Institutions;
using EKYNOX_HEI.CORE.Models.Users;
using EKYNOX_HEI.DAPP.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmUsers : DevExpress.XtraEditors.XtraForm
    {
        public List<UsersViewModel> usersList;
        public UsersModel usersModel;
        private readonly Controller.clsUsers userService;

        public frmUsers(Controller.clsUsers _userService)
        {
            InitializeComponent();
            userService = _userService;
            usersList = new List<UsersViewModel>();
        }

        void DataRefresh()
        {
            var res = userService.GetAllUsers();
            if (res.Status == CORE.Enums.StatusEnum.Error)
            {
                MessageBox.Show("Kullanıcılar veritabanından getirilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Error($@"ProcessDate: {DateTime.Now}, ProcessLocation: {nameof(frmUsers)}, Module: {nameof(DataRefresh)} - Hata Detayı: {res.Message}");
                return;
            }

            usersList = res.Data;
            grdList.DataSource = usersList;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            lueRole.Properties.DataSource = EnumHelper.GetDisplayValues(typeof(CORE.Enums.RoleEnum));
            lueRole.Properties.DisplayMember = "Name";
            lueRole.Properties.ValueMember = "Id";

            DataRefresh();
        }

        void ClearForm()
        {
            lcgUserDetail.Enabled = false;
            teUserNo.EditValue = null;
            teUserName.EditValue = null;
            teName.EditValue = null;
            teSurname.EditValue = null;
            teEmail.EditValue = null;
            tePhone.EditValue = null;
            tePassword.EditValue = null;
            tePasswordAgain.EditValue = null;
            lueRole.EditValue = null;
        }

        private void bmGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Name == "bbtnAdd")
            {
                ClearForm();
                lcgUserDetail.Enabled = true;
                usersModel = new UsersModel();
                var code = userService.CreateUserNr();
                teUserNo.Text = code.Data.ToString();
            }

            if (e.Item.Name == "bbtnUpdate")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as UsersViewModel;

                if (row != null)
                {
                    lcgUserDetail.Enabled = true;
                    usersModel = new UsersModel();
                    usersModel.LogicalRef = row.LogicalRef;
                    teUserNo.EditValue = row.Nr.ToString();
                    teUserName.EditValue = row.UserName;
                    teName.EditValue = row.Name;
                    teSurname.EditValue = row.Surname;
                    teEmail.EditValue = row.EMail;
                    tePhone.EditValue = row.Phone;
                    lueRole.EditValue = row.Role.GetHashCode();
                }
            }

            if (e.Item.Name == "bbtnDelete")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as UsersViewModel;

                if (row != null && MessageBox.Show("Kullanıcı silinecektir.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var res = userService.DeleteUser(row.LogicalRef);
                    if (res.Status == CORE.Enums.StatusEnum.Warning)
                    {
                        MessageBox.Show(res.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (res.Status == CORE.Enums.StatusEnum.Error)
                    {
                        MessageBox.Show("Silme işlemi yapılırken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AppLogger.Error($@"ProcessDate: {DateTime.Now}, ProcessLocation: {nameof(frmUsers)}, Module: {nameof(bmGrid_ItemClick)} - Hata Detayı: {res.Message}");
                        return;
                    }

                    DataRefresh();
                }
            }
        }

        private void grvList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pmGrid.ShowPopup(MousePosition);
            }
        }

        private void grvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = grvList.GetRow(grvList.FocusedRowHandle) as UsersViewModel;
            if (row != null)
            {
                teUserNo.EditValue = row.Nr.ToString();
                teUserName.EditValue = row.UserName;
                teName.EditValue = row.Name;
                teSurname.EditValue = row.Surname;
                teEmail.EditValue = row.EMail;
                tePhone.EditValue = row.Phone;
                lueRole.EditValue = row.Role.GetHashCode();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                usersModel.Nr = userService.CreateUserNr().Data;
                usersModel.UserName = teUserName.EditValue?.ToString();
                usersModel.Name = teName.EditValue?.ToString();
                usersModel.Surname = teSurname.EditValue?.ToString();
                usersModel.EMail = teEmail.EditValue?.ToString();
                usersModel.Phone = tePhone.EditValue?.ToString();
                usersModel.Role = (RoleEnum)lueRole.EditValue;
                usersModel.Password = string.IsNullOrEmpty(tePassword.Text.Trim()) ? "" : Cryptography.Encrypt(tePassword.Text);

                var res = new ReturnData<bool>();
                if (usersModel.LogicalRef <= 0)
                    res = userService.AddUser(usersModel);
                else
                    res = userService.UpdateUser(usersModel);

                if (res.Status == StatusEnum.Warning)
                {
                    MessageBox.Show(res.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (res.Status == CORE.Enums.StatusEnum.Error)
                {
                    MessageBox.Show("Kayıt işlemi yapılırken hata meydana geldi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AppLogger.Error($@"ProcessDate: {DateTime.Now}, ProcessLocation: {nameof(frmUsers)}, Module: {nameof(btnSave_Click)} - Hata Detayı: {res.Message}");
                    return;
                }

                ClearForm();
                DataRefresh();
            }
        }

        private bool Validate()
        {
            bool blResult = true;

            try
            {
                if (string.IsNullOrEmpty(teUserName.Text.Trim()))
                    throw new Exception("Kullanıcı Adı boş olamaz.");

                if (string.IsNullOrEmpty(teName.Text.Trim()))
                    throw new Exception("Ad boş olamaz.");

                if (string.IsNullOrEmpty(teSurname.Text.Trim()))
                    throw new Exception("Soyad boş olamaz.");

                if (string.IsNullOrEmpty(teEmail.Text.Trim()))
                    throw new Exception("E-posta boş olamaz.");

                var mailControl = new System.Net.Mail.MailAddress(teEmail.Text);
                if (mailControl is null)
                    throw new Exception("Geçerli bir e-posta adresi girilmelidir.");

                if (usersModel.LogicalRef <= 0)
                {
                    if (string.IsNullOrEmpty(tePassword.Text.Trim()))
                        throw new Exception("Şifre boş olamaz.");

                    if (string.IsNullOrEmpty(tePasswordAgain.Text.Trim()))
                        throw new Exception("Şifre tekrarı boş olamaz.");

                    if (tePassword.Text != tePasswordAgain.Text)
                        throw new Exception("Şifre ve şifre tekrarı eşleşmiyor.");
                }

                if (lueRole.EditValue == null)
                    throw new Exception("Rol boş olamaz.");

            }
            catch (Exception ex)
            {
                blResult = false;
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return blResult;
        }
    }
}