using DevExpress.XtraEditors;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.AISetting;
using EKYNOX_HEI.CORE.Models.Institutions;
using EKYNOX_HEI.DAPP.Controller;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EKYNOX_HEI.DAPP.View.AISetting
{
    public partial class frmAISettingList : DevExpress.XtraEditors.XtraForm
    {
        private readonly IServiceProvider serviceProvider;
        private readonly clsAISetting AISettingService;
        private List<AISettingListViewModel> AISettingList;

        public frmAISettingList(IServiceProvider _serviceProvider, clsAISetting _AISettingService)
        {
            InitializeComponent();
            serviceProvider = _serviceProvider;
            AISettingService = _AISettingService;
            AISettingList = new List<AISettingListViewModel>();
        }

        void DataRefresh()
        {
            var res = AISettingService.GetAISettingList();
            if (res.Status == CORE.Enums.StatusEnum.Error)
            {
                MessageBox.Show("AI ayar listesi veritabanından getirilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.Error($@"ProcessDate: {DateTime.Now}, ProcessLocation: {nameof(frmAISettingList)}, Module: {nameof(DataRefresh)} - Hata Detayı: {res.Message}");
                return;
            }

            AISettingList = res.Data;
            grdList.DataSource = AISettingList;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bmGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Name == "bbtnAdd")
            {
                var frm = serviceProvider.GetRequiredService<frmAISetting>();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataRefresh();
                }
            }

            if (e.Item.Name == "bbtnUpdate")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as AISettingListViewModel;
                if (row != null)
                {
                    var frm = serviceProvider.GetRequiredService<frmAISetting>();
                    frm.listInfo = row;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        DataRefresh();
                    }
                }
            }

            if (e.Item.Name == "bbtnDelete")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as AISettingListViewModel;

                if (row != null && MessageBox.Show("İlgili kayıt silinecektir.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var res = AISettingService.Delete(row.LogicalRef);
                    if (res.Status == CORE.Enums.StatusEnum.Warning)
                    {
                        MessageBox.Show(res.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (res.Status == CORE.Enums.StatusEnum.Error)
                    {
                        MessageBox.Show("Silme işlemi yapılırken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AppLogger.Error($@"ProcessDate: {DateTime.Now}, ProcessLocation: {nameof(frmAISettingList)}, Module: {nameof(bmGrid_ItemClick)} - Hata Detayı: {res.Message}");
                        return;
                    }

                    DataRefresh();
                }
            }
        }

        private void frmAISettingList_Load(object sender, EventArgs e)
        {
            DataRefresh();
        }

        private void grvList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pmGrid.ShowPopup(MousePosition);
            }
        }
    }
}