using DevExpress.XtraEditors;
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
        public frmEducationAttendanceList()
        {
            InitializeComponent();
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

            }

            if (e.Item.Name == "bbtnUpdate")
            {

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