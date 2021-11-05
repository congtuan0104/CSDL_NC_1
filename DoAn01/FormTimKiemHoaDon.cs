using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using DoAn01.Class;
namespace DoAn01
{
    public partial class FormTimKiemHoaDon : Form
    {
        DataTable tblHDB;
        public FormTimKiemHoaDon()
        {
            InitializeComponent();
        }

        private void FormTimKiemHoaDon_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT *FROM HOADON";
            tblHDB = Functions.GetDataToTable(sql);
            dgvTKHoaDon.DataSource = tblHDB;
            dgvTKHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
            dgvTKHoaDon.Columns[1].HeaderText = "Mã khách hàng";
            dgvTKHoaDon.Columns[2].HeaderText = "Ngày lập";
            dgvTKHoaDon.Columns[3].HeaderText = "Tổng tiền";
            dgvTKHoaDon.Columns[0].Width = 100;
            dgvTKHoaDon.Columns[1].Width = 120;
            dgvTKHoaDon.Columns[2].Width = 80;
            dgvTKHoaDon.Columns[3].Width = 90;
            dgvTKHoaDon.AllowUserToAddRows = false;
            dgvTKHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void BtnTimKiemHD_Click(object sender, EventArgs e)
        {
            string sql;
            DateTime thisDay = DateTime.Today;
            if ((txtMaHDBan.Text == "") && (dtpNgayLap.Value.ToString("yyyy/MM/dd") == thisDay.ToString("yyyy/MM/dd")))
            {
                MessageBox.Show("Nhập mã hóa đơn hoặc ngày lập hóa đơn!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dtpNgayLap.Value = Convert.ToDateTime(dtpNgayLap.Value.ToString("yyyy/MM/dd"));
            sql = "SELECT * FROM HOADON WHERE 1=1";
            if (txtMaHDBan.Text != "")
                sql = sql + " AND MAHD Like N'%" + txtMaHDBan.Text + "%'";
            if (dtpNgayLap.Value.ToString("yyyy/MM/dd") != thisDay.ToString("yyyy/MM/dd"))
                sql = sql + " AND CONVERT(varchar, NGAYLAP, 111) Like '%" + dtpNgayLap.Value.ToString("yyyy/MM/dd") + "%'";
            if (tblHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tblHDB = Functions.GetDataToTable(sql);
            dgvTKHoaDon.DataSource = tblHDB;
        }

        private void BtnTimKiemKH_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaKhach.Text == "") && (txtTenKH.Text == ""))
            {
                MessageBox.Show("Nhập vào mã khách hàng hoặc tên khách hàng!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT *FROM KHACHHANG WHERE 1=1";
            if (txtMaKhach.Text != "")
                sql = sql + " AND MAKH Like N'%" + txtMaKhach.Text + "%'";
            if (txtTenKH.Text != "")
                sql = sql + " AND TEN Like N'%" + txtTenKH.Text + "%'";
            if (tblHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tblHDB = Functions.GetDataToTable(sql);
            dgvTKHoaDon.DataSource = tblHDB;
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
