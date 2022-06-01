using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
    public partial class CreatEvent : Form
    {
        public CreatEvent(CIS form)
        {
            InitializeComponent();
            this.fPri = form;
        }
        private CIS fPri;
        private void CreatEvent_Load(object sender, EventArgs e)
        {

        }


        //EVENTS

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenEvent.Text != "" && txtDiaDiem.Text != "")
            {
                fPri.events.Add(new SuKien(txtTenEvent.Text, timepick.Value.Date, txtDiaDiem.Text));
                SuKien count = fPri.events[fPri.events.Count - 1];
                fPri.AddRowDataEvent(count);
            
                this.Close();
                fPri.Enabled = true;
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
            fPri.Show();
            fPri.Enabled = true;
        }

        private void CreatEvent_FormClosed(object sender, FormClosedEventArgs e)
        {
            fPri.Show();
            fPri.Enabled = true;
        }
    }
}
