using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
    public partial class DataForm : Form
    {
        public DataForm(CIS cis, string mode)
        {
            InitializeComponent();
            this.fPri = cis;
            switch(mode)
            {
                case "Event":
                    {
                        SuKienNo = fPri.events[fPri.dataEvent.CurrentRow.Index];
                        break;
                    }
                case "QR":
                    {
                        SuKienNo = fPri.events[fPri.cbbQREvent.SelectedIndex];
                        break;
                    }
            }    
                
        }

        //KHAI BÁO BIẾN
        CIS fPri; //Lấy dữ liệu từ form trước
        internal SuKien SuKienNo; //Số thứ tự của phần tử trong list đang truy xuất
        internal int CountProgress;

        //EVENTS
        private void DataForm_Load(object sender, EventArgs e)
        {
            DataFill(SuKienNo.DsThamGia);
            txtTenEvent.Text = SuKienNo.TenEvent;
            txtThoiGian.Text = SuKienNo.ThoiGian.ToString("dddd dd/MM/yyyy");
            txtDiaDiem.Text = SuKienNo.DiaDiem;
            txtSoLuong.Text = SuKienNo.DsThamGia.Count.ToString();
        }
        private void DataForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            fPri.Show();
            for (int i = 0; i < fPri.dataEvent.RowCount; i++)
                fPri.dataEvent.Rows[i].Cells[3].Value = fPri.events[i].DsThamGia.Count.ToString();  
        }

        #region TẠO THỨ THỰ CHO BẢNG
        private void dataDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.dataDetail.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void dataDetail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (e.RowIndex < dataDetail.RowCount)
            {
                for (int i=0; i < dataDetail.RowCount; i++)
                    this.dataDetail.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        #endregion

        #region THÊM XÓA SỬA
        private void btnThem_Click(object sender, EventArgs e)
        {
            CreateSinhVien form = new CreateSinhVien(this);
            form.Show();
            this.Enabled = false;
            txtSoLuong.Text = SuKienNo.DsThamGia.Count.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa dòng đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int index = dataDetail.CurrentRow.Index;
                    SuKienNo.DsThamGia.RemoveAt(index);
                    dataDetail.Rows.RemoveAt(index);
                    txtSoLuong.Text = SuKienNo.DsThamGia.Count.ToString();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            CreateSinhVien form = new CreateSinhVien(this, true);
            form.Show();
            this.Enabled = false;
        }
        #endregion


        //HÀM CHỨC NĂNG
        private void DataFill(List<SinhVien> e)
        {
            foreach (SinhVien item in e)
                dataDetail.Rows.Add(item.HoVaTen, item.MSSV, item.NienKhoa, item.KhoaVien, item.Email, item.Code);
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            string Path = "";
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path = dialog.FileName;
                Excel excel = new Excel(Path, 1);
                CountProgress = 1;
                Progress form = new Progress(this,"DataForm");
                form.Show();
                new Thread(
                    () =>
                    {
                        while (excel.ReadShell(CountProgress, 1).ToString() != "-")
                        {
                            try
                            {
                                SuKienNo.DsThamGia.Add(new SinhVien(excel.ReadShell(CountProgress, 1).ToString(), excel.ReadShell(CountProgress, 2).ToString(), excel.ReadShell(CountProgress, 3).ToString(), excel.ReadShell(CountProgress, 4).ToString(), excel.ReadShell(CountProgress, 5).ToString()));
                                dataDetail.Rows.Add(excel.ReadShell(CountProgress, 1), excel.ReadShell(CountProgress, 2), excel.ReadShell(CountProgress, 3), excel.ReadShell(CountProgress, 4), excel.ReadShell(CountProgress, 5), SuKienNo.DsThamGia[SuKienNo.DsThamGia.Count-1].Code);
                            }
                            catch (Exception)
                            { }
                            CountProgress += 1;
                        }
                        excel.CloseFile();
                        form.Close();
                        txtSoLuong.Text = SuKienNo.DsThamGia.Count.ToString();
                    }
                    )
                { IsBackground = true }.Start();
            }
            
            
        }


    }       
}
