namespace EKYNOX_HEI.DAPP.View
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            txtUserName = new DevExpress.XtraEditors.TextEdit();
            txtPassword = new DevExpress.XtraEditors.TextEdit();
            btnClose = new DevExpress.XtraEditors.SimpleButton();
            btnLogin = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtUserName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(pictureEdit1);
            layoutControl1.Controls.Add(txtUserName);
            layoutControl1.Controls.Add(txtPassword);
            layoutControl1.Controls.Add(btnClose);
            layoutControl1.Controls.Add(btnLogin);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(2450, 103, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(468, 212);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // pictureEdit1
            // 
            pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            pictureEdit1.Location = new Point(2, 2);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.Appearance.ForeColor = Color.White;
            pictureEdit1.Properties.Appearance.Options.UseForeColor = true;
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit1.Size = new Size(230, 208);
            pictureEdit1.StyleController = layoutControl1;
            pictureEdit1.TabIndex = 1;
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(236, 64);
            txtUserName.Name = "txtUserName";
            txtUserName.Properties.Appearance.Font = new Font("Tahoma", 12F);
            txtUserName.Properties.Appearance.Options.UseFont = true;
            txtUserName.Size = new Size(230, 26);
            txtUserName.StyleController = layoutControl1;
            txtUserName.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(236, 116);
            txtPassword.Name = "txtPassword";
            txtPassword.Properties.Appearance.Font = new Font("Tahoma", 12F);
            txtPassword.Properties.Appearance.Options.UseFont = true;
            txtPassword.Properties.UseSystemPasswordChar = true;
            txtPassword.Size = new Size(230, 26);
            txtPassword.StyleController = layoutControl1;
            txtPassword.TabIndex = 2;
            // 
            // btnClose
            // 
            btnClose.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnClose.ImageOptions.SvgImage");
            btnClose.Location = new Point(353, 174);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(113, 36);
            btnClose.StyleController = layoutControl1;
            btnClose.TabIndex = 4;
            btnClose.Text = "Vazgeç";
            btnClose.Click += btnClose_Click;
            // 
            // btnLogin
            // 
            btnLogin.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnLogin.ImageOptions.SvgImage");
            btnLogin.Location = new Point(236, 174);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(113, 36);
            btnLogin.StyleController = layoutControl1;
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Giriş";
            btnLogin.Click += btnLogin_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, layoutControlItem3, emptySpaceItem1, layoutControlItem5, layoutControlItem4, emptySpaceItem2 });
            Root.Name = "Root";
            Root.Size = new Size(468, 212);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = pictureEdit1;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(234, 212);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.AppearanceItemCaption.Font = new Font("Tahoma", 12F);
            layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem2.AppearanceItemCaptionDisabled.BackColor = Color.White;
            layoutControlItem2.AppearanceItemCaptionDisabled.Options.UseBackColor = true;
            layoutControlItem2.Control = txtUserName;
            layoutControlItem2.Location = new Point(234, 38);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(234, 54);
            layoutControlItem2.Text = "Kullanıcı Adı";
            layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem2.TextSize = new Size(87, 19);
            layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.AppearanceItemCaption.BackColor = Color.White;
            layoutControlItem3.AppearanceItemCaption.Font = new Font("Tahoma", 12F);
            layoutControlItem3.AppearanceItemCaption.Options.UseBackColor = true;
            layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem3.Control = txtPassword;
            layoutControlItem3.Location = new Point(234, 92);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(234, 52);
            layoutControlItem3.Text = "Parola";
            layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem3.TextSize = new Size(44, 19);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AppearanceItemCaption.BackColor = Color.White;
            emptySpaceItem1.AppearanceItemCaption.Options.UseBackColor = true;
            emptySpaceItem1.Location = new Point(234, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(234, 38);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = btnLogin;
            layoutControlItem5.Location = new Point(234, 172);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(117, 40);
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = btnClose;
            layoutControlItem4.Location = new Point(351, 172);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(117, 40);
            layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AppearanceItemCaption.BackColor = Color.White;
            emptySpaceItem2.AppearanceItemCaption.Options.UseBackColor = true;
            emptySpaceItem2.AppearanceItemCaptionDisabled.BackColor = Color.White;
            emptySpaceItem2.AppearanceItemCaptionDisabled.Options.UseBackColor = true;
            emptySpaceItem2.Location = new Point(234, 144);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new Size(234, 28);
            // 
            // frmLogin
            // 
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(468, 212);
            Controls.Add(layoutControl1);
            FormBorderStyle = FormBorderStyle.None;
            IconOptions.Image = (Image)resources.GetObject("frmLogin.IconOptions.Image");
            KeyPreview = true;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterParent;
            Text = "frmLogin";
            Shown += frmLogin_Shown;
            KeyDown += frmLogin_KeyDown;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtUserName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}