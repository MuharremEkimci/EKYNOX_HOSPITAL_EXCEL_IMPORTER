namespace EKYNOX_HEI.DAPP.View.AISetting
{
    partial class frmAISettingList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAISettingList));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            grdList = new DevExpress.XtraGrid.GridControl();
            grvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            btnClose = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pmGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bmGrid).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(grdList);
            layoutControl1.Controls.Add(btnClose);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(2749, 353, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(779, 586);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // grdList
            // 
            grdList.Location = new Point(2, 2);
            grdList.MainView = grvList;
            grdList.Name = "grdList";
            grdList.Size = new Size(775, 542);
            grdList.TabIndex = 0;
            grdList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { grvList });
            // 
            // grvList
            // 
            grvList.GridControl = grdList;
            grvList.Name = "grvList";
            grvList.OptionsView.ShowGroupPanel = false;
            grvList.MouseDown += grvList_MouseDown;
            // 
            // btnClose
            // 
            btnClose.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnClose.ImageOptions.SvgImage");
            btnClose.Location = new Point(684, 548);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(93, 36);
            btnClose.StyleController = layoutControl1;
            btnClose.TabIndex = 2;
            btnClose.Text = "Kapat [F3]";
            btnClose.Click += btnClose_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, emptySpaceItem1, layoutControlItem2 });
            Root.Name = "Root";
            Root.Size = new Size(779, 586);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = grdList;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(779, 546);
            layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new Point(0, 546);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(682, 40);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = btnClose;
            layoutControlItem2.Location = new Point(682, 546);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(97, 40);
            layoutControlItem2.Text = "Kapat [F3]";
            layoutControlItem2.TextVisible = false;
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
            bbtnUpdate.Caption = "Güncelle";
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
            barDockControlTop.Size = new Size(779, 0);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 586);
            barDockControlBottom.Manager = bmGrid;
            barDockControlBottom.Size = new Size(779, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 0);
            barDockControlLeft.Manager = bmGrid;
            barDockControlLeft.Size = new Size(0, 586);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(779, 0);
            barDockControlRight.Manager = bmGrid;
            barDockControlRight.Size = new Size(0, 586);
            // 
            // frmAISettingList
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(779, 586);
            Controls.Add(layoutControl1);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Name = "frmAISettingList";
            Text = "AI Listesi";
            Load += frmAISettingList_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grdList).EndInit();
            ((System.ComponentModel.ISupportInitialize)grvList).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
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
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
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