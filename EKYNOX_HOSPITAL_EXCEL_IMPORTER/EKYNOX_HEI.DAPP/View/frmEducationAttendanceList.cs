using DevExpress.Dialogs.Core.View;
using DevExpress.XtraEditors;
using EKYNOX_HEI.CORE.Helpers;
using EKYNOX_HEI.CORE.Models.AISetting;
using EKYNOX_HEI.CORE.Models.EducationAttendance;
using EKYNOX_HEI.DAPP.Controller;
using EKYNOX_HEI.DAPP.View.AISetting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
                MessageBox.Show("Eğitim katılım listesi getirilirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                var row = grvList.GetRow(grvList.FocusedRowHandle) as EducationAttendanceListViewModel;

                if (row != null && MessageBox.Show("İlgili kayıt silinecektir. İşlem geri alınamaz.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var res = educationAttendaceService.Delete(row.LogicalRef);
                    if (res.Status == CORE.Enums.StatusEnum.Warning)
                    {
                        MessageBox.Show(res.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (res.Status == CORE.Enums.StatusEnum.Error)
                    {
                        MessageBox.Show("Silme işlemi yapılırken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AppLogger.Error(DateTime.Now, nameof(frmEducationAttendanceList), nameof(bmGrid_ItemClick), nameof(educationAttendaceService.Delete), res.Message);
                        return;
                    }

                    DataRefresh();
                }
            }

            if (e.Item.Name == "bbtnExcelDownload")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as EducationAttendanceListViewModel;

                if (row is not null)
                {
                    var data = educationAttendaceService.GetData(row.LogicalRef).Data;
                    if (data.ExcelData is null)
                    {
                        MessageBox.Show("Excel verisi bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    using (SaveFileDialog dialog = new SaveFileDialog())
                    {
                        dialog.Filter = "Excel Dosyası (*.xlsx)|*.xlsx";
                        dialog.FileName = "Rapor.xlsx";
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllBytes(dialog.FileName, data.ExcelData);

                            if (MessageBox.Show("Dosya kaydedildi. Açmak ister misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                            {
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = dialog.FileName,
                                    UseShellExecute = true
                                });
                            }
                        }
                    }
                }
            }

            if (e.Item.Name == "bbtnShowExcel")
            {
                var row = grvList.GetRow(grvList.FocusedRowHandle) as EducationAttendanceListViewModel;
                if (row is not null)
                {
                    var data = educationAttendaceService.GetData(row.LogicalRef).Data;
                    if (data.ExcelData is null)
                    {
                        MessageBox.Show("Excel verisi bulunamadı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    try
                    {
                        var file = Path.Combine(Path.GetTempPath(), $@"Rapor_{Guid.NewGuid()}.xlsx");

                        File.WriteAllBytes(file, data.ExcelData);

                        Process.Start(new ProcessStartInfo
                        {
                            FileName = file,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Excel verisi açılırken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        AppLogger.Error(DateTime.Now, nameof(frmEducationAttendanceList), "bbtnShowExcel", nameof(educationAttendaceService.GetData), ex.Message);
                    }

                }
            }
        }

        private void grdList_MouseEnter(object sender, EventArgs e)
        {

        }

        private void grvList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pmGrid.ShowPopup(MousePosition);
            }
        }

        private void frmEducationAttendanceList_Load(object sender, EventArgs e)
        {
            DataRefresh();
        }
    }
}