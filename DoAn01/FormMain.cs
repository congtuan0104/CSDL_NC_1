using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn01
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            FormKH dlgKH = new FormKH();
            dlgKH.ShowDialog();
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            FormSP dlgSP = new FormSP();
            dlgSP.ShowDialog();
        }

        private void btnHD_Click(object sender, EventArgs e)
        {
            FormHD dlgHD = new FormHD();
            dlgHD.ShowDialog();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            FormThongKe dlgTK = new FormThongKe();
            dlgTK.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormKH dlgKH = new FormKH();
            dlgKH.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            FormSP dlgSP = new FormSP();
            dlgSP.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FormHD dlgHD = new FormHD();
            dlgHD.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FormThongKe dlgTK = new FormThongKe();
            dlgTK.ShowDialog();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
