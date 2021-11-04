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
    public partial class FormKH : Form
    {
        DataTable tblKH;

        public FormKH()
        {
            InitializeComponent();
        }

        private void FormKH_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MAKH,HO,TEN,NGSINH,DIENTHOAI,SONHA,DUONG,PHUONG,QUAN,THANHPHO FROM KHACHHANG";
            tblKH = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvKhachHang.DataSource = tblKH; //Nguồn dữ liệu            
            dgvKhachHang.Columns[0].HeaderText = "Mã KH";
            dgvKhachHang.Columns[1].HeaderText = "Họ";
            dgvKhachHang.Columns[2].HeaderText = "Tên KH";
            dgvKhachHang.Columns[3].HeaderText = "Ngày sinh";
            dgvKhachHang.Columns[4].HeaderText = "Điện thoại";
            dgvKhachHang.Columns[5].HeaderText = "Số nhà";
            dgvKhachHang.Columns[6].HeaderText = "Đường";
            dgvKhachHang.Columns[7].HeaderText = "Phường";
            dgvKhachHang.Columns[8].HeaderText = "Quận";
            dgvKhachHang.Columns[9].HeaderText = "Thành phố";
            dgvKhachHang.Columns[0].Width = 70;
            dgvKhachHang.Columns[1].Width = 50;
            dgvKhachHang.Columns[2].Width = 80;
            dgvKhachHang.Columns[3].Width = 80;
            dgvKhachHang.Columns[4].Width = 80;
            dgvKhachHang.Columns[5].Width = 50;
            dgvKhachHang.Columns[8].Width = 80;
            dgvKhachHang.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKH.Text = dgvKhachHang.CurrentRow.Cells["MAKH"].Value.ToString();
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["HO"].Value.ToString() + " "+ dgvKhachHang.CurrentRow.Cells["TEN"].Value.ToString();           
            mtbDienThoai.Text = dgvKhachHang.CurrentRow.Cells["DIENTHOAI"].Value.ToString();
            if (dgvKhachHang.CurrentRow.Cells["NGSINH"].Value.ToString() != "")
            {
                dateTimePickerNgSinh.Value = Convert.ToDateTime(dgvKhachHang.CurrentRow.Cells["NGSINH"].Value.ToString());
            }
            
            txtSoNha.Text = dgvKhachHang.CurrentRow.Cells["SONHA"].Value.ToString();
            txtDuong.Text = dgvKhachHang.CurrentRow.Cells["DUONG"].Value.ToString();
            txtPhuong.Text = dgvKhachHang.CurrentRow.Cells["PHUONG"].Value.ToString();
            txtQuan.Text = dgvKhachHang.CurrentRow.Cells["QUAN"].Value.ToString();
            txtThanhPho.Text = dgvKhachHang.CurrentRow.Cells["THANHPHO"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void ResetValues()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            mtbDienThoai.Text = "";
            txtDuong.Text = "";
            txtSoNha.Text = "";
            txtPhuong.Text = "";
            txtQuan.Text = "";
            txtThanhPho.Text = "";
            dateTimePickerNgSinh.Value = DateTime.Today;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKH.Enabled = true;
            txtMaKH.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập họ tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKH.Focus();
                return;
            }
            if (txtSoNha.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập số nhà", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoNha.Focus();
                return;
            }
            if (txtDuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập tên đường", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDuong.Focus();
                return;
            }
            if (txtPhuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập phường", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhuong.Focus();
                return;
            }
            if (txtQuan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập quận", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuan.Focus();
                return;
            }
            if (txtThanhPho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập thành phố", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtThanhPho.Focus();
                return;
            }
            if (mtbDienThoai.Text == "   -    -")
            {
                MessageBox.Show("Chưa nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
                return;
            }

            if (dateTimePickerNgSinh.Value == DateTime.Today)
            {
                MessageBox.Show("Chưa nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerNgSinh.Focus();
                return;
            }

            //Kiểm tra đã tồn tại mã khách chưa
            sql = "SELECT MAKH FROM KHACHHANG WHERE MAKH='" + txtMaKH.Text.Trim() + "'";
            if (Functions.CheckValue(sql))
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKH.Focus();
                return;
            }
            //Chèn thêm
            string fullname = txtTenKH.Text.Trim();
            string firstname = fullname.Substring(0, fullname.IndexOf(" "));
            string lastname = fullname.Substring(fullname.IndexOf(" ")+1);
            string phoneNumber= Regex.Replace(mtbDienThoai.Text, @"[^0-9]", string.Empty);

            sql = "INSERT INTO KHACHHANG VALUES ("
                + "N'" + txtMaKH.Text.Trim()
                + "',N'" + firstname
                + "',N'" + lastname
                + "','" + dateTimePickerNgSinh.Value.ToString("yyyy-MM-dd")
                + "','" + txtSoNha.Text.Trim()
                + "',N'" + txtDuong.Text.Trim()
                + "',N'" + txtPhuong.Text.Trim()
                + "',N'" + txtQuan.Text.Trim()
                + "',N'" + txtThanhPho.Text.Trim()
                + "','" + phoneNumber
                + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaKH.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text.Trim() == "")
            {
                MessageBox.Show("Chọn dòng muốn xoá trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xác nhận xoá khách hàng này!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE KHACHHANG WHERE MAKH=N'" + txtMaKH.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaKH.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Chọn khách hàng cần sửa thông tin trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKH.Focus();
                return;
            }
            if (txtSoNha.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập số nhà", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoNha.Focus();
                return;
            }
            if (txtDuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập tên đường", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDuong.Focus();
                return;
            }
            if (txtPhuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập phường", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhuong.Focus();
                return;
            }
            if (txtQuan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập quận", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuan.Focus();
                return;
            }
            if (txtThanhPho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập thành phố", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtThanhPho.Focus();
                return;
            }
            if (mtbDienThoai.Text == "   -    -")
            {
                MessageBox.Show("Chưa nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
                return;
            }

            if (dateTimePickerNgSinh.Value == DateTime.Now)
            {
                MessageBox.Show("Chưa nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerNgSinh.Focus();
                return;
            }

           
            //Chèn thêm
            string fullname = txtTenKH.Text.Trim();
            string firstname = fullname.Substring(0, fullname.IndexOf(" "));
            string lastname = fullname.Substring(fullname.IndexOf(" ") + 1);
            string phoneNumber = Regex.Replace(mtbDienThoai.Text, @"[^0-9]", string.Empty);

            sql = "UPDATE KHACHHANG SET "
                + "HO=N'" + firstname + "',"
                + "TEN=N'" + lastname + "',"
                + "NGSINH='" + dateTimePickerNgSinh.Value.ToString("yyyy-MM-dd") + "',"
                + "SONHA=" + txtSoNha.Text + ","
                + "DUONG=N'" + txtDuong.Text.Trim() + "',"
                + "PHUONG=N'" + txtPhuong.Text.Trim() + "',"
                + "QUAN=N'" + txtQuan.Text.Trim() + "',"
                + "THANHPHO=N'" + txtThanhPho.Text.Trim() + "',"
                + "DIENTHOAI='" + phoneNumber
                + "' WHERE MAKH=N'" + txtMaKH.Text + "';";

            System.Diagnostics.Debug.WriteLine(sql);
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }
    }
}
