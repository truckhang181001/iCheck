using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool
{
    public partial class Progress : Form
    {
        public Progress(object fpri, string mode)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.fPri = fpri;
            this.Mode = mode;
            switch (Mode)
            {
                case "DataForm":
                    {
                        ((DataForm)fPri).Enabled = false;
                        break;
                    }
                case "CIS":
                    {
                        ((CIS)fPri).Enabled = false;
                        break;
                    }
            }
        }

        object fPri;
        string Mode;

        private void Progress_Load(object sender, EventArgs e)
        {
            progressBar1.PerformLayout();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(Mode)
            {
                case "DataForm":
                    {
                        lbCount.Text = "Đã thực hiện được: " + ((DataForm)fPri).CountProgress.ToString();
                        break;
                    }
                case "CIS":
                    {
                        lbCount.Text = "Đã thực hiện được: " + ((CIS)fPri).CountProgress.ToString();
                        lbName.Text = "Loading: " + ((CIS)fPri).NameProgress.ToString();
                        break;
                    }
            }    
        }

        private void Progress_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (Mode)
            {
                case "DataForm":
                    {
                        ((DataForm)fPri).Enabled = true;
                        ((DataForm)fPri).Show();
                        break;
                    }
                case "CIS":
                    {
                        ((CIS)fPri).Enabled = true;
                        ((CIS)fPri).Show();
                        break;
                    }
            }
        }
    }
}
