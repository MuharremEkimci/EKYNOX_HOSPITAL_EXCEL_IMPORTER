namespace EKYNOX_HEI.DAPP.View.Common
{
    partial class frmWaitingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainProgPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // mainProgPanel
            // 
            mainProgPanel.Appearance.BackColor = Color.Transparent;
            mainProgPanel.Appearance.Options.UseBackColor = true;
            mainProgPanel.AppearanceCaption.Font = new Font("Microsoft Sans Serif", 12F);
            mainProgPanel.AppearanceCaption.Options.UseFont = true;
            mainProgPanel.AppearanceDescription.Font = new Font("Microsoft Sans Serif", 8.25F);
            mainProgPanel.AppearanceDescription.Options.UseFont = true;
            mainProgPanel.Caption = "Lütfen Bekleyiniz...";
            mainProgPanel.Description = "";
            mainProgPanel.Dock = DockStyle.Fill;
            mainProgPanel.ImageHorzOffset = 20;
            mainProgPanel.Location = new Point(0, 17);
            mainProgPanel.Margin = new Padding(0, 3, 0, 3);
            mainProgPanel.Name = "mainProgPanel";
            mainProgPanel.Size = new Size(246, 39);
            mainProgPanel.TabIndex = 0;
            mainProgPanel.Text = "progressPanel1";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(mainProgPanel, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 14, 0, 14);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(246, 73);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // frmWaitingForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(246, 73);
            Controls.Add(tableLayoutPanel1);
            DoubleBuffered = true;
            Name = "frmWaitingForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Form1";
            Load += frmWaitingForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraWaitForm.ProgressPanel mainProgPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
