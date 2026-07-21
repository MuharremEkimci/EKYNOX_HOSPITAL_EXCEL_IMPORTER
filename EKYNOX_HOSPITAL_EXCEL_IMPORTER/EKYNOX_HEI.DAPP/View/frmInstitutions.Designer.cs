namespace EKYNOX_HEI.DAPP.View
{
    partial class frmInstitutions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInstitutions));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            grdList = new DevExpress.XtraGrid.GridControl();
            grvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            teCode = new DevExpress.XtraEditors.TextEdit();
            teName = new DevExpress.XtraEditors.TextEdit();
            teCity = new DevExpress.XtraEditors.TextEdit();
            teTown = new DevExpress.XtraEditors.TextEdit();
            teDistrict = new DevExpress.XtraEditors.TextEdit();
            meAddress = new DevExpress.XtraEditors.MemoEdit();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            lcgInstitutionDetail = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            pmGrid = new DevExpress.XtraBars.PopupMenu(components);
            bbtnAdd = new DevExpress.XtraBars.BarButtonItem();
            bbtnUpdate = new DevExpress.XtraBars.BarButtonItem();
            bbtnDelete = new DevExpress.XtraBars.BarButtonItem();
            bmGrid = new DevExpress.XtraBars.BarManager(components);
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grvList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)teCode.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)teName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)teCity.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)teTown.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)teDistrict.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)meAddress.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lcgInstitutionDetail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pmGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bmGrid).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(grdList);
            layoutControl1.Controls.Add(teCode);
            layoutControl1.Controls.Add(teName);
            layoutControl1.Controls.Add(teCity);
            layoutControl1.Controls.Add(teTown);
            layoutControl1.Controls.Add(teDistrict);
            layoutControl1.Controls.Add(meAddress);
            layoutControl1.Controls.Add(btnCancel);
            layoutControl1.Controls.Add(btnSave);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Margin = new Padding(3, 2, 3, 2);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(3028, 366, 812, 500);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(457, 717);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // grdList
            // 
            grdList.EmbeddedNavigator.Margin = new Padding(3, 2, 3, 2);
            grdList.Location = new Point(5, 26);
            grdList.MainView = grvList;
            grdList.Margin = new Padding(3, 2, 3, 2);
            grdList.Name = "grdList";
            grdList.Size = new Size(447, 394);
            grdList.TabIndex = 0;
            grdList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { grvList });
            // 
            // grvList
            // 
            grvList.DetailHeight = 284;
            grvList.GridControl = grdList;
            grvList.Name = "grvList";
            grvList.OptionsEditForm.PopupEditFormWidth = 686;
            grvList.OptionsView.ShowAutoFilterRow = true;
            grvList.OptionsView.ShowGroupPanel = false;
            grvList.FocusedRowChanged += grvList_FocusedRowChanged;
            grvList.MouseDown += grvList_MouseDown;
            // 
            // teCode
            // 
            teCode.Location = new Point(91, 451);
            teCode.Margin = new Padding(3, 2, 3, 2);
            teCode.Name = "teCode";
            teCode.Properties.ReadOnly = true;
            teCode.Properties.UseReadOnlyAppearance = false;
            teCode.Size = new Size(361, 20);
            teCode.StyleController = layoutControl1;
            teCode.TabIndex = 2;
            // 
            // teName
            // 
            teName.Location = new Point(91, 475);
            teName.Margin = new Padding(3, 2, 3, 2);
            teName.Name = "teName";
            teName.Size = new Size(361, 20);
            teName.StyleController = layoutControl1;
            teName.TabIndex = 3;
            // 
            // teCity
            // 
            teCity.Location = new Point(91, 499);
            teCity.Margin = new Padding(3, 2, 3, 2);
            teCity.Name = "teCity";
            teCity.Size = new Size(361, 20);
            teCity.StyleController = layoutControl1;
            teCity.TabIndex = 4;
            // 
            // teTown
            // 
            teTown.Location = new Point(91, 523);
            teTown.Margin = new Padding(3, 2, 3, 2);
            teTown.Name = "teTown";
            teTown.Size = new Size(361, 20);
            teTown.StyleController = layoutControl1;
            teTown.TabIndex = 5;
            // 
            // teDistrict
            // 
            teDistrict.Location = new Point(91, 547);
            teDistrict.Margin = new Padding(3, 2, 3, 2);
            teDistrict.Name = "teDistrict";
            teDistrict.Size = new Size(361, 20);
            teDistrict.StyleController = layoutControl1;
            teDistrict.TabIndex = 6;
            // 
            // meAddress
            // 
            meAddress.Location = new Point(91, 571);
            meAddress.Margin = new Padding(3, 2, 3, 2);
            meAddress.Name = "meAddress";
            meAddress.Size = new Size(361, 101);
            meAddress.StyleController = layoutControl1;
            meAddress.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnCancel.ImageOptions.SvgImage");
            btnCancel.Location = new Point(353, 676);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(99, 36);
            btnCancel.StyleController = layoutControl1;
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Vazgeç [F3]";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnSave.ImageOptions.SvgImage");
            btnSave.Location = new Point(250, 676);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(99, 36);
            btnSave.StyleController = layoutControl1;
            btnSave.TabIndex = 8;
            btnSave.Text = "Kaydet [F2]";
            btnSave.Click += btnSave_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, lcgInstitutionDetail });
            Root.Name = "Root";
            Root.Size = new Size(457, 717);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup1.Location = new Point(0, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup1.Size = new Size(457, 425);
            layoutControlGroup1.Text = "Kurumlar";
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = grdList;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(451, 398);
            layoutControlItem1.TextVisible = false;
            // 
            // lcgInstitutionDetail
            // 
            lcgInstitutionDetail.Enabled = false;
            lcgInstitutionDetail.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3, layoutControlItem4, layoutControlItem5, layoutControlItem6, layoutControlItem7, layoutControlItem8, emptySpaceItem1, layoutControlItem9 });
            lcgInstitutionDetail.Location = new Point(0, 425);
            lcgInstitutionDetail.Name = "lcgInstitutionDetail";
            lcgInstitutionDetail.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            lcgInstitutionDetail.Size = new Size(457, 292);
            lcgInstitutionDetail.Text = "Kurum Detay";
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = teCode;
            layoutControlItem2.Location = new Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(451, 24);
            layoutControlItem2.Text = "Kurum Kodu";
            layoutControlItem2.TextSize = new Size(74, 13);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = teName;
            layoutControlItem3.Location = new Point(0, 24);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(451, 24);
            layoutControlItem3.Text = "Kurum Açıklama";
            layoutControlItem3.TextSize = new Size(74, 13);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = teCity;
            layoutControlItem4.Location = new Point(0, 48);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(451, 24);
            layoutControlItem4.Text = "Şehir";
            layoutControlItem4.TextSize = new Size(74, 13);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = teTown;
            layoutControlItem5.Location = new Point(0, 72);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(451, 24);
            layoutControlItem5.Text = "İlçe";
            layoutControlItem5.TextSize = new Size(74, 13);
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = teDistrict;
            layoutControlItem6.Location = new Point(0, 96);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new Size(451, 24);
            layoutControlItem6.Text = "Mahalle";
            layoutControlItem6.TextSize = new Size(74, 13);
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = meAddress;
            layoutControlItem7.Location = new Point(0, 120);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new Size(451, 105);
            layoutControlItem7.Text = "Açık Adres";
            layoutControlItem7.TextSize = new Size(74, 13);
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = btnCancel;
            layoutControlItem8.Location = new Point(348, 225);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.Size = new Size(103, 40);
            layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new Point(0, 225);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(245, 40);
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = btnSave;
            layoutControlItem9.Location = new Point(245, 225);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new Size(103, 40);
            layoutControlItem9.TextVisible = false;
            // 
            // pmGrid
            // 
            pmGrid.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(bbtnAdd), new DevExpress.XtraBars.LinkPersistInfo(bbtnUpdate), new DevExpress.XtraBars.LinkPersistInfo(bbtnDelete) });
            pmGrid.Manager = bmGrid;
            pmGrid.Name = "pmGrid";
            // 
            // bbtnAdd
            // 
            bbtnAdd.Caption = "Ekle";
            bbtnAdd.Id = 0;
            bbtnAdd.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bbtnAdd.ImageOptions.SvgImage");
            bbtnAdd.Name = "bbtnAdd";
            // 
            // bbtnUpdate
            // 
            bbtnUpdate.Caption = "Değiştir";
            bbtnUpdate.Id = 1;
            bbtnUpdate.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bbtnUpdate.ImageOptions.SvgImage");
            bbtnUpdate.Name = "bbtnUpdate";
            // 
            // bbtnDelete
            // 
            bbtnDelete.Caption = "Sil";
            bbtnDelete.Id = 2;
            bbtnDelete.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("bbtnDelete.ImageOptions.SvgImage");
            bbtnDelete.Name = "bbtnDelete";
            // 
            // bmGrid
            // 
            bmGrid.DockControls.Add(barDockControlTop);
            bmGrid.DockControls.Add(barDockControlBottom);
            bmGrid.DockControls.Add(barDockControlLeft);
            bmGrid.DockControls.Add(barDockControlRight);
            bmGrid.Form = this;
            bmGrid.Items.AddRange(new DevExpress.XtraBars.BarItem[] { bbtnAdd, bbtnUpdate, bbtnDelete });
            bmGrid.MaxItemId = 3;
            bmGrid.ItemClick += bmGrid_ItemClick;
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = bmGrid;
            barDockControlTop.Size = new Size(457, 0);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 717);
            barDockControlBottom.Manager = bmGrid;
            barDockControlBottom.Size = new Size(457, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 0);
            barDockControlLeft.Manager = bmGrid;
            barDockControlLeft.Size = new Size(0, 717);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(457, 0);
            barDockControlRight.Manager = bmGrid;
            barDockControlRight.Size = new Size(0, 717);
            // 
            // frmInstitutions
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 717);
            Controls.Add(layoutControl1);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            IconOptions.Icon = (Icon)resources.GetObject("frmInstitutions.IconOptions.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmInstitutions";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kurumlar";
            Load += frmInstitutions_Load;
            KeyDown += frmInstitutions_KeyDown;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grdList).EndInit();
            ((System.ComponentModel.ISupportInitialize)grvList).EndInit();
            ((System.ComponentModel.ISupportInitialize)teCode.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)teName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)teCity.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)teTown.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)teDistrict.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)meAddress.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)lcgInstitutionDetail).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pmGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)bmGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl grdList;
        private DevExpress.XtraGrid.Views.Grid.GridView grvList;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit teCode;
        private DevExpress.XtraEditors.TextEdit teName;
        private DevExpress.XtraEditors.TextEdit teCity;
        private DevExpress.XtraEditors.TextEdit teTown;
        private DevExpress.XtraEditors.TextEdit teDistrict;
        private DevExpress.XtraEditors.MemoEdit meAddress;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup lcgInstitutionDetail;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraBars.PopupMenu pmGrid;
        private DevExpress.XtraBars.BarButtonItem bbtnAdd;
        private DevExpress.XtraBars.BarButtonItem bbtnUpdate;
        private DevExpress.XtraBars.BarButtonItem bbtnDelete;
        private DevExpress.XtraBars.BarManager bmGrid;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}