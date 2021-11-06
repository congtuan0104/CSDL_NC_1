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
using COMExcel = Microsoft.Office.Interop.Excel;
using DoAn01.Class;

namespace DoAn01
{
    public partial class FormHD : Form
    {
        DataTable tblHD;
        public FormHD()
        {
            InitializeComponent();
        }

        private void FormHD_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            txtMAHD.Enabled = false;
            txtMAKH.Enabled = false;
            txtTongTien.Enabled = false;
            txtTongTien.Text = "0";
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM HOADON";
            tblHD = Functions.GetDataToTable(sql);
            dgvHoaDon.DataSource = tblHD;
            dgvHoaDon.Columns[0].HeaderText = "Mã hóa đơn";
            dgvHoaDon.Columns[1].HeaderText = "Mã khách hàng";
            dgvHoaDon.Columns[2].HeaderText = "Ngày lập";
            dgvHoaDon.Columns[3].HeaderText = "Tổng tiền";
            dgvHoaDon.Columns[0].Width = 200;
            dgvHoaDon.Columns[1].Width = 200;
            dgvHoaDon.Columns[2].Width = 200;
            dgvHoaDon.Columns[3].Width = 2000;
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void ResetValues()
        {
            txtMAHD.Text = "";
            txtMAKH.Text = "";
            txtTongTien.Text = "0";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMAKH.Enabled = true;
            txtMAKH.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtMAKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMAKH.Focus();
                return;
            }

            if (txtMAHD.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMAKH.Focus();
                return;
            }

            //Kiểm tra đã tồn tại mã hóa đơn chưa
            sql = "SELECT MAHD FROM HOADON WHERE MAHD='" + txtMAHD.Text.Trim() + "'";
            if (Functions.CheckValue(sql))
            {
                MessageBox.Show("Mã hóa đơn này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMAHD.Focus();
                return;
            }

            sql = "INSERT HOADON(MAHD,MAKH,NGAYLAP) VALUES ("
                    + "N'" + txtMAHD.Text.Trim()
                    + "',N'" + txtMAKH.Text.Trim()
                    + "','" + dtpNgayLap.Value.ToString("yyyy-MM-dd")
                    + "')";

            System.Diagnostics.Debug.WriteLine(sql);
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
           
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMAHD.Enabled = false;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMAHD.Text.Trim() == "")
            {
                MessageBox.Show("Chọn dòng muốn xoá trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xác nhận xoá hóa đơn này!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE HOADON WHERE MAHD=N'" + txtMAHD.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHD.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMAHD.Text == "")
            {
                MessageBox.Show("Chọn mã hóa đơn cần sửa thông tin trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
       
            if (txtTongTien.Text == "")
            {
                MessageBox.Show("Chưa nhập tổng tiền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTongTien.Focus();
                return;
            }

            if (dtpNgayLap.Value == DateTime.Now)
            {
                MessageBox.Show("Chưa nhập ngày lập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayLap.Focus();
                return;
            }

            sql = "UPDATE HOADON SET "
                + "NGAYLAP='" + dtpNgayLap.Value.ToString("yyyy-MM-dd")
                + "' WHERE MAHD=N'" + txtMAHD.Text + "';";

            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMAKH.Enabled = false;
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMAHD.Focus();
                return;
            }
            if (tblHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMAHD.Text = dgvHoaDon.CurrentRow.Cells["MAHD"].Value.ToString();
            txtMAKH.Text = dgvHoaDon.CurrentRow.Cells["MAKH"].Value.ToString();
            txtTongTien.Text = dgvHoaDon.CurrentRow.Cells["TONGTIEN"].Value.ToString();
            if (dgvHoaDon.CurrentRow.Cells["NGAYLAP"].Value.ToString() != "")
            {
                dtpNgayLap.Value = Convert.ToDateTime(dgvHoaDon.CurrentRow.Cells["NGAYLAP"].Value.ToString());
            }

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
