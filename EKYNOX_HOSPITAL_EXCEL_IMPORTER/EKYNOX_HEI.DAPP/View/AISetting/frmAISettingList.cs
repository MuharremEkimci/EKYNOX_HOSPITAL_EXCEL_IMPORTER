using DevExpress.XtraEditors;
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

        public frmAISettingList(IServiceProvider _serviceProvider)
        {
            InitializeComponent();
            serviceProvider = _serviceProvider;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}