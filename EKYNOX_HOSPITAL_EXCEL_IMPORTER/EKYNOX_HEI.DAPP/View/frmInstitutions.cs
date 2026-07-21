using DevExpress.XtraEditors;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.Institutions;
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
    public partial class frmInstitutions : DevExpress.XtraEditors.XtraForm
    {
        private List<InstitutionsViewModel> institutionsList;
        private readonly Controller.clsInstitutions institutionService;
        private InstitutionsModel institutionsModel;

        public frmInstitutions(Controller.clsInstitutions _institutionService)
        {
            InitializeComponent();
            institutionService = _institutionService;
            institutionsList = new List<InstitutionsViewModel>();
            institutionsModel = new InstitutionsModel();
        }

        void DataRefresh()
        {
            var res = institutionService.GetAllInstitutions();
            if (res.Status == CORE.Enums.StatusEnum.Error)
            {
                MessageBox.Show("Kurumlar veritabanından getirilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Error($@"ProcessLocation: {nameof(frmInstitutions)}, Module: {nameof(DataRefresh)} - Hata Detayı: {res.Message}");
                return;
            }

            institutionsList = res.Data;
            grdList.DataSource = institutionsList;
        }

        private void frmInstitutions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSave.PerformClick();
            }

            if (e.KeyCode == Keys.F3)
            {
                btnCancel.PerformClick();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("İşlemden çıkılacaktır.","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ClearForm();
            }
        }

        void ClearForm()
        {
            lcgInstitutionDetail.Enabled = false;
            teCode.EditValue = null;
            teName.EditValue = null;
            teCity.EditValue = null;
            teTown.EditValue = null;
            teDistrict.EditValue = null;
            meAddress.EditValue = null;
        }

        private void bmGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Name == "bbtnAdd")
            {
                ClearForm();
                lcgInstitutionDetail.Enabled = true;
                institutionsModel = new InstitutionsModel();
                var code = institutionService.CreateInstutionNo();
                teCode.Text = code.Data;

            }

            if (e.Item.Name == "bbtnUpdate")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as InstitutionsViewModel;

                if (row != null)
                {
                    lcgInstitutionDetail.Enabled = true;
                    institutionsModel = new InstitutionsModel();
                    institutionsModel.LogicalRef = row.LogicalRef;
                    teCode.EditValue = row.Code;
                    teName.EditValue = row.Name;
                    teCity.EditValue = row.City;
                    teTown.EditValue = row.Town;
                    teDistrict.EditValue = row.District;
                    meAddress.EditValue = row.Address;
                }
            }

            if (e.Item.Name == "bbtnDelete")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as InstitutionsViewModel;

                if (row != null && MessageBox.Show("Kurum silinecektir.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var res = institutionService.DeleteInstitution(row.LogicalRef);
                    if (res.Status == CORE.Enums.StatusEnum.Warning)
                    {
                        MessageBox.Show(res.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (res.Status == CORE.Enums.StatusEnum.Error)
                    {
                        MessageBox.Show("Silme işlemi yapılırken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AppLogger.Error($@"ProcessDate: {DateTime.Now}, ProcessLocation: {nameof(frmInstitutions)}, Module: {nameof(bmGrid_ItemClick)} - Hata Detayı: {res.Message}");
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

        private void frmInstitutions_Load(object sender, EventArgs e)
        {
            DataRefresh();
        }

        private void grvList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = grvList.GetRow(grvList.FocusedRowHandle) as InstitutionsViewModel;

            if (row != null)
            {
                teCode.EditValue = row.Code;
                teName.EditValue = row.Name;
                teCity.EditValue = row.City;
                teTown.EditValue = row.Town;
                teDistrict.EditValue = row.District;
                meAddress.EditValue = row.Address;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                institutionsModel.Code = teCode.EditValue?.ToString();
                institutionsModel.Name = teName.EditValue?.ToString();
                institutionsModel.City = teCity.EditValue?.ToString();
                institutionsModel.Town = teTown.EditValue?.ToString();
                institutionsModel.District = teDistrict.EditValue?.ToString();
                institutionsModel.Address = meAddress.EditValue?.ToString();

                var res = new ReturnData<bool>();
                if (institutionsModel.LogicalRef <= 0)
                    res = institutionService.AddInstitution(institutionsModel);
                else
                    res = institutionService.UpdateInstitution(institutionsModel);

                if (res.Status == CORE.Enums.StatusEnum.Error)
                {
                    MessageBox.Show("Kayıt işlemi yapılırken hata meydana geldi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AppLogger.Error($@"ProcessDate: {DateTime.Now}, ProcessLocation: {nameof(frmInstitutions)}, Module: {nameof(btnSave_Click)} - Hata Detayı: {res.Message}");
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
                if (string.IsNullOrEmpty(teCode.Text.Trim()))
                    throw new Exception("Kurum Kodu boş olamaz.");

                if (string.IsNullOrEmpty(teName.Text.Trim()))
                    throw new Exception("Kurum Adı boş olamaz.");

                if (string.IsNullOrEmpty(teCity.Text.Trim()))
                    throw new Exception("Şehir bilgisi boş olamaz.");

                if (string.IsNullOrEmpty(teTown.Text.Trim()))
                    throw new Exception("İlçe bilgisi boş olamaz.");    

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