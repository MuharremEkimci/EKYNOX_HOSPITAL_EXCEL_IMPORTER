using DevExpress.XtraEditors;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.EducationAttendance;
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

namespace EKYNOX_HEI.DAPP.View
{
    public partial class frmEducationAttendanceList : DevExpress.XtraEditors.XtraForm
    {
        private readonly IServiceProvider serviceProvider;
        private List<EducationAttendanceListViewModel> listData;
        private readonly clsEducationAttendance educationAttendaceService;

        public frmEducationAttendanceList(IServiceProvider serviceProvider, clsEducationAttendance _educationAttendanceService)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            educationAttendaceService = _educationAttendanceService;
            listData = new List<EducationAttendanceListViewModel>();
        }

        void DataRefresh() 
        {
            var res = educationAttendaceService.GetEducationAttendanceList();
            if (res.Status == CORE.Enums.StatusEnum.Error)
            {
                MessageBox.Show("Eğitim katılım listesi getirilirken hata oluştu.","Hata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                AppLogger.Error(DateTime.Now, nameof(frmEducationAttendanceList), nameof(DataRefresh), nameof(educationAttendaceService.GetEducationAttendanceList), res.Message);
                return;
            }

            listData = res.Data;
            grdList.DataSource = listData;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEducationAttendanceList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                btnClose.PerformClick();
            }
        }

        private void bmGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Name == "bbtnAdd")
            {
                var frm = serviceProvider.GetRequiredService<frmEducationAttendance>();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataRefresh();
                }
            }

            if (e.Item.Name == "bbtnUpdate")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as EducationAttendanceListViewModel;

                if (row is not null)
                {
                    var frm = serviceProvider.GetRequiredService<frmEducationAttendance>();
                    frm.updInfo = row;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        DataRefresh();
                    }
                }
            }

            if (e.Item.Name == "bbtnDelete")
            {

            }
        }

        private void grdList_MouseEnter(object sender, EventArgs e)
        {

        }

        private void grvList_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                pmGrid.ShowPopup(MousePosition);
            }   
        }
    }
}