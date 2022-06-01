using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Threading;

namespace Tool
{
    public partial class CIS : Form
    {
        public CIS()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //KHAI BÁO BIẾN
        internal List<SuKien> events = new List<SuKien>();
        internal int CountProgress;
        internal string NameProgress;


        private void CIS_Load(object sender, EventArgs e)
        {
            AddFont();
            ReadAllBinaryFile(events);
            FillDataEvent(events);
        }

        private void CIS_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteAllBinaryFile(events);
        }


        ///// HIỆU ỨNG VÀ ADD FONTS
        #region Effects
        PrivateFontCollection CusFont = new PrivateFontCollection();

        private void AddFont()
        {
            CusFont.AddFontFile("..//..//Fonts//SanFranciscoDisplay-Thin.otf");
            CusFont.AddFontFile("..//..//Fonts//SanFranciscoDisplay-Regular.otf");
            CusFont.AddFontFile("..//..//Fonts//SanFranciscoDisplay-Bold.otf");
            btnEvent.Font = new Font(CusFont.Families[1], 16);
            btnQR.Font = new Font(CusFont.Families[2], 16);
            btnMail.Font = new Font(CusFont.Families[2], 16);
            btnCheckin.Font = new Font(CusFont.Families[2], 16);
            // objectFont
            txtQR.Font = new Font(CusFont.Families[0], 36);


        }
        public void ChangebtnEffect(string btn)
        {

            //Fill background
            btnEvent.BackColor = Color.FromArgb(52, 81, 95);
            btnQR.BackColor = Color.FromArgb(52, 81, 95);
            btnMail.BackColor = Color.FromArgb(52, 81, 95);
            btnCheckin.BackColor = Color.FromArgb(52, 81, 95);
            //Fill text
            btnEvent.ForeColor = Color.FromArgb(217, 217, 217);
            btnQR.ForeColor = Color.FromArgb(217, 217, 217);
            btnMail.ForeColor = Color.FromArgb(217, 217, 217);
            btnCheckin.ForeColor = Color.FromArgb(217, 217, 217);
            //Font Style
            btnEvent.Font = new Font(CusFont.Families[2], 16);
            btnQR.Font = new Font(CusFont.Families[2], 16);
            btnMail.Font = new Font(CusFont.Families[2], 16);
            btnCheckin.Font = new Font(CusFont.Families[2], 16);
            //Unvisible
            pnlQR.Visible = false;
            pnlGuiMail.Visible = false;
            pnlEvent.Visible = false;
            pnlCheckin.Visible = false;
            switch (btn)
            {
                case "btnIntro":
                    {
                        btnEvent.BackColor = Color.FromArgb(217, 217, 217);
                        btnEvent.ForeColor = Color.FromArgb(52, 81, 95);
                        btnEvent.Font = new Font(CusFont.Families[1], 16, FontStyle.Regular);
                        pnlEvent.Visible = true;
                    };
                    break;
                case "btnQR":
                    {
                        btnQR.BackColor = Color.FromArgb(217, 217, 217);
                        btnQR.ForeColor = Color.FromArgb(52, 81, 95);
                        btnQR.Font = new Font(CusFont.Families[1], 16, FontStyle.Regular);
                        pnlQR.Visible = true;
                    }
                    break;
                case "btnMail":
                    {
                        btnMail.BackColor = Color.FromArgb(217, 217, 217);
                        btnMail.ForeColor = Color.FromArgb(52, 81, 95);
                        btnMail.Font = new Font(CusFont.Families[1], 16, FontStyle.Regular);
                        pnlGuiMail.Visible = true;
                    }
                    break;
                case "btnCheckin":
                    {
                        btnCheckin.BackColor = Color.FromArgb(217, 217, 217);
                        btnCheckin.ForeColor = Color.FromArgb(52, 81, 95);
                        btnCheckin.Font = new Font(CusFont.Families[1], 16, FontStyle.Regular);
                        pnlCheckin.Visible = true;
                    }
                    break;
            }
        }


        #endregion
        #region ĐỔI MÀU TEXTBOX & BUTTON
        private void txtTaiKhoan_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "Tài khoản")
            {
                txtTaiKhoan.Text = "";
                txtTaiKhoan.ForeColor = Color.FromArgb(36, 31, 32);
            }
        }

        private void txtTaiKhoan_Leave(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "")
            {
                txtTaiKhoan.Text = "Tài khoản";
                txtTaiKhoan.ForeColor = Color.Gray;
            }
        }

        private void txtMatKhau_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "Mật khẩu")
            {
                txtMatKhau.Text = "";
                txtMatKhau.ForeColor = Color.FromArgb(36, 31, 32);
            }
            if (ckbMatKhau.Checked == false) txtMatKhau.UseSystemPasswordChar = true;

        }

        private void txtMatKhau_Leave(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "")
            {
                txtMatKhau.UseSystemPasswordChar = false;
                txtMatKhau.Text = "Mật khẩu";
                txtMatKhau.ForeColor = Color.Gray;
            }
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "Mật khẩu") txtMatKhau.UseSystemPasswordChar = false;
            else
            {
                if (ckbMatKhau.Checked)
                {
                    txtMatKhau.UseSystemPasswordChar = false;
                }
                else txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void ckbMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (txtMatKhau.Text != "Mật khẩu")
            {
                if (ckbMatKhau.Checked == true)
                    txtMatKhau.UseSystemPasswordChar = false;
                else
                    txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void txtMail_NoiDung_Click(object sender, EventArgs e)
        {
            if (txtMailBody.Text == "Nhập nội dung mail tại đây")
            {
                txtMailBody.Text = "";
                txtMailBody.ForeColor = Color.FromArgb(36, 31, 32);
            }
        }

        private void txtMail_NoiDung_Leave(object sender, EventArgs e)
        {
            if (txtMailBody.Text == "")
            {
                txtMailBody.ForeColor = Color.Gray;
                txtMailBody.Text = "Nhập nội dung mail tại đây";
            }
        }

        private void txtSubject_Click(object sender, EventArgs e)
        {
            if (txtMailSubject.Text == "Subject")
            {
                txtMailSubject.Text = "";
                txtMailSubject.ForeColor = Color.FromArgb(36, 31, 32);
            }
        }

        private void txtSubject_Leave(object sender, EventArgs e)
        {
            if (txtMailSubject.Text == "")
            {
                txtMailSubject.ForeColor = Color.Gray;
                txtMailSubject.Text = "Subject";
            }
        }
        #endregion
        #region BUTTON PANEL CLICK
        private void btnQR_Click(object sender, EventArgs e)
        {
            ChangebtnEffect("btnQR");
            cbbQREvent.DataSource = events;
            cbbQREvent.DisplayMember = "TenEvent";

        }

        private void btnIntro_Click(object sender, EventArgs e)
        {
            ChangebtnEffect("btnIntro");

        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            ChangebtnEffect("btnMail");


        }

        private void btnCheckin_Click(object sender, EventArgs e)
        {
            ChangebtnEffect("btnCheckin");

        }
        #endregion



        ////// HÀM CHỨC NĂNG
        #region GỬI GMAIL
        private void Guimail(string from, string to, string subject, string mainmess /*Attachment attach*/)
        {
            //Nội dung email
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            //message.IsBodyHtml = Regex.IsMatch(mainmess, @"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
            message.Body = mainmess;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            //message.Attachments.Add(attach);

            //Gửi mail thông qua smtp
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(txtTaiKhoan.Text, txtMatKhau.Text);
            smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            //smtp.Send(message);
            smtp.SendAsync(message, "CIS sending...");
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("Thao đã được hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (e.Error != null)
            {
                MessageBox.Show("Có lỗi, kiểm tra lại địa chỉ email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Email của bạn đã được gửi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region ĐỌC VÀ GHI DỮ LIỆU
        private static void WriteAllBinaryFile(List<SuKien> objectToWrite, bool append = false)
        {
            foreach (SuKien item in objectToWrite)
            {
                using (Stream stream = File.Open("..//..//Data//" + item.MaEvent + ".CIS", append ? FileMode.Append : FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, item);
                }
            }    

        }

        private static void WriteOneBinaryFile(string path,SuKien objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(path, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        private void ReadAllBinaryFile(List<SuKien> objectToRead)
        {
            string[] filename = Directory.GetFiles("..//..//Data");
            foreach (string item in filename)
            {
                using (Stream stream = File.Open(item, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    objectToRead.Add((SuKien)binaryFormatter.Deserialize(stream));
                }
            }
        }

        private SuKien ReadOneBinaryFile(string FilePath)
        {
            using (Stream stream = File.Open(FilePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (SuKien)binaryFormatter.Deserialize(stream);
            }
        }

        private static void Deletefile(SuKien e)
        {
            File.Delete("..//..//Data//"+e.MaEvent+".CIS");    
        }
        #endregion

        internal void FillDataEvent(List<SuKien> e)
        {
            foreach(SuKien item in e)
            {
                dataEvent.Rows.Add(item.TenEvent,item.ThoiGian.ToString("ddd dd/MM/yyyy"), item.DiaDiem, item.DsThamGia.Count);
            }    
        }
        internal void AddRowDataEvent(SuKien sukien)
        {
            dataEvent.Rows.Add(sukien.TenEvent, sukien.ThoiGian.ToString("ddd dd/MM/yyyy"), sukien.DiaDiem, sukien.DsThamGia.Count);
            dataEvent.ClearSelection();
            dataEvent.Rows[dataEvent.RowCount - 1].Selected = true;
        }


        /// EVENTS
        private void btnMailSend_Click(object sender, EventArgs e)
        {
            Guimail(txtTaiKhoan.Text, "nhpt2701@gmail.com", txtMailSubject.Text, txtMailBody.Text);
        }
        #region QUẢN LÝ EVENTS
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dataEvent.RowCount > 0)
            {
                DataForm dataForm = new DataForm(this,"Event");
                dataForm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Chưa có sự kiện nào.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnTaoEvent_Click(object sender, EventArgs e)
        {
            CreatEvent form = new CreatEvent(this);
            form.Show();
            this.Enabled = false;
        }

        private void btnEventXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xóa dòng đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Deletefile(events[dataEvent.CurrentRow.Index]);
                    events.RemoveAt(dataEvent.CurrentRow.Index);
                    dataEvent.Rows.RemoveAt(dataEvent.CurrentRow.Index);
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            int index = dataEvent.CurrentRow.Index;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CIS File|.CIS";
            sfd.Title = "Lưu sự kiện " + events[index].TenEvent;
            sfd.FileName = events[index].TenEvent;
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                WriteOneBinaryFile(sfd.FileName,events[index]);
            }    
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CIS File|*.CIS";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                try
                {
                    SuKien check = ReadOneBinaryFile(dialog.FileName);
                    SuKien find = events.Find((SuKien ob) => { return (Program.RemoveUnicode(ob.TenEvent) == Program.RemoveUnicode(check.TenEvent) && ob.ThoiGian == check.ThoiGian); });
                    if (find == null)
                    {
                        events.Add(check);
                        MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AddRowDataEvent(events[events.Count - 1]);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("File đã tồn tại, bạn có muốn lưu với một tên khác không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            int x = 1;
                            do
                            {
                                x += 1;
                            }
                            while (events.Find((SuKien ob) => { return (Program.RemoveUnicode(ob.TenEvent) == Program.RemoveUnicode(check.TenEvent) + "(" + x.ToString() + ")" && ob.ThoiGian == check.ThoiGian); }) != null);
                            events.Add(new SuKien(check.TenEvent + "(" + x.ToString() + ")", check.ThoiGian, check.DiaDiem, check.DsThamGia));
                            MessageBox.Show("Thêm thành công với tên gọi khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AddRowDataEvent(events[events.Count - 1]);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void btnQRChiTiet_Click(object sender, EventArgs e)
        {
            DataForm form = new DataForm(this,"QR");
            form.Show();
            this.Hide();
        }

        private void btnQRgenerate_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string pathFolder = dialog.SelectedPath + "\\";
                int index = cbbQREvent.SelectedIndex;

                Progress form = new Progress(this, "CIS");
                form.Show();

                CountProgress = 0;
                NameProgress = "";

                // THỰC THI
                new Thread(
                    () =>
                    {
                        PictureBox picture = new PictureBox();
                        foreach (SinhVien item in events[index].DsThamGia)
                        {

                            //Ghi dữ liệu mã Code
                            string datacreate = item.Code;

                            //TẠO QR CODE
                            QRCoder.QRCodeGenerator QGenerate = new QRCoder.QRCodeGenerator();
                            var MyData = QGenerate.CreateQrCode(datacreate, QRCoder.QRCodeGenerator.ECCLevel.H);
                            var QRimage = new QRCoder.QRCode(MyData);
                            picture.Image = QRimage.GetGraphic(50);

                            // SAVE QR CODE 
                            string pathQR = pathFolder;
                            pathQR += item.MSSV;
                            pathQR += ".jpg";
                            picture.Image.Save(pathQR);

                            //Tăng biến đếm số lượng đã thực hiện
                            NameProgress = item.HoVaTen;
                            CountProgress += 1;
                            Thread.Sleep(2000);
                        }
                        form.Close();
                        MessageBox.Show(string.Format("Tạo thành công {0} QR!", CountProgress), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    )
                { IsBackground = true }.Start();



            }

        }
    }
}
