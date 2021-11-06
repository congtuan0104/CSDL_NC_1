using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn01.Class;

namespace DoAn01
{
    public partial class FormSP : Form
    {

        DataTable tblSP;

        public FormSP()
        {
            InitializeComponent();
        }

        private void FormSP_Load(object sender, EventArgs e)
        {
            txtMaSP.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM SANPHAM";
            tblSP = DoAn01.Class.Functions.GetDataToTable(sql);
            dgvSanPham.DataSource = tblSP;          
            dgvSanPham.Columns[0].HeaderText = "Mã sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[2].HeaderText = "Số lượng tồn";
            dgvSanPham.Columns[3].HeaderText = "Mô tả";
            dgvSanPham.Columns[4].HeaderText = "Giá bán";

            dgvSanPham.Columns[0].Width = 100;
            dgvSanPham.Columns[1].Width = 130;
            dgvSanPham.Columns[2].Width = 80;
            dgvSanPham.Columns[3].Width = 200;
            dgvSanPham.Columns[4].Width = 100;
            dgvSanPham.AllowUserToAddRows = false; 
            dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically; 
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (tblSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaSP.Text = dgvSanPham.CurrentRow.Cells["MASP"].Value.ToString();
            txtTenSP.Text = dgvSanPham.CurrentRow.Cells["TENSP"].Value.ToString();
            txtSoLuongTon.Text = dgvSanPham.CurrentRow.Cells["SOLUONGTON"].Value.ToString();
            txtMoTa.Text = dgvSanPham.CurrentRow.Cells["MOTA"].Value.ToString();
            txtGia.Text = dgvSanPham.CurrentRow.Cells["GIA"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaSP.Enabled = true;
            txtMaSP.Focus();
        }

        private void ResetValues()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuongTon.Text = "";
            txtMoTa.Text = "";
            txtGia.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSP.Focus();
                return;
            }
            if (txtGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập giá sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGia.Focus();
                return;
            }
            if (txtSoLuongTon.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongTon.Focus();
                return;
            }
            

            //Kiểm tra đã tồn tại mã khách chưa
            sql = "SELECT MASP FROM SANPHAM WHERE MASP='" + txtMaSP.Text.Trim() + "'";
            if (Functions.CheckValue(sql))
            {
                MessageBox.Show("Sản phẩm này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            //Chèn thêm

            sql = "INSERT INTO SANPHAM VALUES ("
                + "'" + txtMaSP.Text.Trim()
                + "',N'" + txtTenSP.Text.Trim()
                + "','" + txtSoLuongTon.Text.Trim()
                + "',N'" + txtMoTa.Text.Trim()
                + "','" + txtGia.Text.Trim()
                + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaSP.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaSP.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblSP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSP.Text.Trim() == "")
            {
                MessageBox.Show("Chọn dòng muốn xoá trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xác nhận xoá sản phẩm này!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE SANPHAM WHERE MASP=N'" + txtMaSP.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblSP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSP.Text == "")
            {
                MessageBox.Show("Chọn sản phẩm cần sửa thông tin trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSP.Focus();
                return;
            }
            if (txtSoLuongTon.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập số lượng tồn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongTon.Focus();
                return;
            }
            if (txtGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập giá sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGia.Focus();
                return;
            }
            if (txtMoTa.Text.Trim().Length == 0)
            {
                txtMoTa.Text = "";
            }
            string Gia = txtGia.Text.Replace(",", ".");

            sql = "UPDATE SANPHAM SET "
                + "TENSP=N'" + txtTenSP.Text.Trim() + "',"
                + "SOLUONGTON=" + txtSoLuongTon.Text + ","
                + "MOTA=N'" + txtMoTa.Text.Trim() + "',"
                + "GIA=" + Gia
                + " WHERE MASP=N'" + txtMaSP.Text + "';";

            System.Diagnostics.Debug.WriteLine(sql);
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormTimKiemHoaDon dglTimKiem = new FormTimKiemHoaDon();
            dglTimKiem.ShowDialog();
        }
    }
}
