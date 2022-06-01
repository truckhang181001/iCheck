using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Tool
{
    public partial class CreateSinhVien : Form
    {
        public CreateSinhVien(DataForm form, bool edit = false)
        {
            InitializeComponent();
            this.fPri = form;
            this.FuncEdit = edit;
        }

        //KHAI BÁO BIẾN
        DataForm fPri;
        SinhVien sinhVien;
        int rowIndex; //Đánh dấu dòng đã chọn
        bool FuncEdit;

        private void CreateSinhVien_Load(object sender, EventArgs e)
        {
            //Kiểm tra là edit hay tạo mới
            if (FuncEdit)
            {
                rowIndex = fPri.dataDetail.CurrentRow.Index;
                sinhVien = fPri.SuKienNo.DsThamGia[fPri.dataDetail.CurrentRow.Index];
                txtTen.Text = sinhVien.HoVaTen;
                txtMSSV.Text = sinhVien.MSSV;
                txtNienKhoa.Text = sinhVien.NienKhoa;
                txtKhoaVien.Text = sinhVien.KhoaVien;
                txtEmail.Text = sinhVien.Email;
            }
        }

        //EVENTS
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra là Edit hay Tạo mới
            if(FuncEdit)
            {
                if (txtTen.Text != "" && txtMSSV.Text != "" && txtNienKhoa.Text != "" && txtKhoaVien.Text != "" && txtEmail.Text != "")
                {
                    fPri.SuKienNo.DsThamGia[rowIndex] = new SinhVien(txtTen.Text, txtMSSV.Text, txtNienKhoa.Text, txtKhoaVien.Text, txtEmail.Text);
                    sinhVien = fPri.SuKienNo.DsThamGia[rowIndex];
                    fPri.dataDetail.CurrentRow.Cells[0].Value = sinhVien.HoVaTen;
                    fPri.dataDetail.CurrentRow.Cells[1].Value = sinhVien.MSSV;
                    fPri.dataDetail.CurrentRow.Cells[2].Value = sinhVien.NienKhoa;
                    fPri.dataDetail.CurrentRow.Cells[3].Value = sinhVien.KhoaVien;
                    fPri.dataDetail.CurrentRow.Cells[4].Value = sinhVien.Email;
                    fPri.dataDetail.CurrentRow.Cells[5].Value = sinhVien.Code;
                    fPri.dataDetail.ClearSelection();
                    fPri.dataDetail.Rows[rowIndex].Selected = true;
                    MessageBox.Show("Thay đổi dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    fPri.Enabled = true;
                    fPri.Show();
                }
                else
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtTen.Text != "" && txtMSSV.Text != "" && txtNienKhoa.Text != "" && txtKhoaVien.Text != "" && txtEmail.Text != "")
                {
                    fPri.SuKienNo.DsThamGia.Add(new SinhVien(txtTen.Text, txtMSSV.Text, txtNienKhoa.Text, txtKhoaVien.Text, txtEmail.Text));
                    fPri.dataDetail.Rows.Add(txtTen.Text, txtMSSV.Text, txtNienKhoa.Text, txtKhoaVien.Text, txtEmail.Text, fPri.SuKienNo.DsThamGia[fPri.SuKienNo.DsThamGia.Count-1].Code);
                    fPri.dataDetail.ClearSelection();
                    fPri.dataDetail.Rows[fPri.dataDetail.RowCount - 1].Selected = true;
                    MessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    fPri.Enabled = true;
                    fPri.Show();
                }
                else
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
            fPri.Enabled = true;
            fPri.Show();
        }

        private void CreateSinhVien_FormClosed(object sender, FormClosedEventArgs e)
        {
            fPri.Enabled = true;
            fPri.Show();
        }
    }
}
