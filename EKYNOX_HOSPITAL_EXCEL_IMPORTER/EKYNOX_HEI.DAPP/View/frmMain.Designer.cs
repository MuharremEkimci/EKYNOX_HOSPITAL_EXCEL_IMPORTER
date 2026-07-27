namespace EKYNOX_HEI.DAPP.View
{
    partial class frmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            rgbbTemalar = new DevExpress.XtraBars.RibbonGalleryBarItem();
            btnAppExit = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            btnUsers = new DevExpress.XtraBars.BarButtonItem();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            rgbTemalar = new DevExpress.XtraBars.RibbonGalleryBarItem();
            ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            rpProcess = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            btnInstitutions = new DevExpress.XtraBars.BarButtonItem();
            btnEducationAttendandce = new DevExpress.XtraBars.BarButtonItem();
            ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            xtmm = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(components);
            ((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xtmm).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.EmptyAreaImageOptions.ImagePadding = new Padding(26, 24, 26, 24);
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbon.ExpandCollapseItem, rgbbTemalar, btnAppExit });
            ribbon.Location = new Point(0, 0);
            ribbon.Margin = new Padding(3, 2, 3, 2);
            ribbon.MaxItemId = 10;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 283;
            ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1, rpProcess });
            ribbon.Size = new Size(1127, 158);
            ribbon.StatusBar = ribbonStatusBar;
            // 
            // rgbbTemalar
            // 
            rgbbTemalar.Caption = "ribbonGalleryBarItem1";
            rgbbTemalar.Id = 8;
            rgbbTemalar.Name = "rgbbTemalar";
            // 
            // btnAppExit
            // 
            btnAppExit.Caption = "Programdan Çıkış";
            btnAppExit.Id = 9;
            btnAppExit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnAppExit.ImageOptions.SvgImage");
            btnAppExit.Name = "btnAppExit";
            btnAppExit.ItemClick += btnAppExit_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup4, ribbonPageGroup2, ribbonPageGroup3 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "Genel";
            // 
            // ribbonPageGroup4
            // 
            ribbonPageGroup4.ItemLinks.Add(btnUsers);
            ribbonPageGroup4.Name = "ribbonPageGroup4";
            ribbonPageGroup4.Text = "Kullanıcılar";
            // 
            // btnUsers
            // 
            btnUsers.Caption = "Kullanıcılar";
            btnUsers.Id = 7;
            btnUsers.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnUsers.ImageOptions.SvgImage");
            btnUsers.Name = "btnUsers";
            btnUsers.ItemClick += btnUsers_ItemClick;
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(rgbTemalar);
            ribbonPageGroup2.ItemLinks.Add(rgbbTemalar);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "Tema Seçimi";
            // 
            // rgbTemalar
            // 
            rgbTemalar.Caption = "ribbonGalleryBarItem1";
            rgbTemalar.Id = 4;
            rgbTemalar.Name = "rgbTemalar";
            // 
            // ribbonPageGroup3
            // 
            ribbonPageGroup3.ItemLinks.Add(btnAppExit);
            ribbonPageGroup3.Name = "ribbonPageGroup3";
            ribbonPageGroup3.Text = "Çıkış";
            // 
            // rpProcess
            // 
            rpProcess.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            rpProcess.Name = "rpProcess";
            rpProcess.Text = "İşlemler";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(btnInstitutions);
            ribbonPageGroup1.ItemLinks.Add(btnEducationAttendandce);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "İşlemler";
            // 
            // btnInstitutions
            // 
            btnInstitutions.Caption = "Kurumlar";
            btnInstitutions.Id = 3;
            btnInstitutions.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnInstitutions.ImageOptions.SvgImage");
            btnInstitutions.Name = "btnInstitutions";
            btnInstitutions.ItemClick += btnInstitutions_ItemClick;
            // 
            // btnEducationAttendandce
            // 
            btnEducationAttendandce.Caption = "Eğitim Katılım Listesi";
            btnEducationAttendandce.Id = 1;
            btnEducationAttendandce.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnEducationAttendandce.ImageOptions.SvgImage");
            btnEducationAttendandce.Name = "btnEducationAttendandce";
            btnEducationAttendandce.ItemClick += btnEducationAttendandce_ItemClick;
            // 
            // ribbonStatusBar
            // 
            ribbonStatusBar.Location = new Point(0, 660);
            ribbonStatusBar.Margin = new Padding(3, 2, 3, 2);
            ribbonStatusBar.Name = "ribbonStatusBar";
            ribbonStatusBar.Ribbon = ribbon;
            ribbonStatusBar.Size = new Size(1127, 24);
            // 
            // xtmm
            // 
            xtmm.MdiParent = this;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1127, 684);
            Controls.Add(ribbon);
            Controls.Add(ribbonStatusBar);
            IconOptions.Icon = (Icon)resources.GetObject("frmMain.IconOptions.Icon");
            IsMdiContainer = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmMain";
            Ribbon = ribbon;
            StatusBar = ribbonStatusBar;
            WindowState = FormWindowState.Maximized;
            FormClosing += frmMain_FormClosing;
            FormClosed += frmMain_FormClosed;
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
            ((System.ComponentModel.ISupportInitialize)xtmm).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpProcess;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnEducationAttendandce;
        private DevExpress.XtraBars.BarButtonItem btnInstitutions;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbTemalar;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.BarButtonItem btnUsers;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbbTemalar;
        private DevExpress.XtraBars.BarButtonItem btnAppExit;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtmm;
    }
}