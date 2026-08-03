using DevExpress.XtraWaitForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EKYNOX_HEI.DAPP.View.Common
{
    public partial class frmWaitingForm : WaitForm
    {
        public frmWaitingForm()
        {
            InitializeComponent();
            this.mainProgPanel.AutoHeight = true;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.mainProgPanel.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.mainProgPanel.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        private void frmWaitingForm_Load(object sender, EventArgs e)
        {

        }

        public enum WaitFormCommand
        {
        }
    }
}