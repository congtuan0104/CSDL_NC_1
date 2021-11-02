using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MAKH,HO+' '+TEN AS HOTEN,NGSINH,DIENTHOAI,SONHA,DUONG,PHUONG,QUAN,THANHPHO FROM KHACHHANG";
            tblKH = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvKhachHang.DataSource = tblKH; //Nguồn dữ liệu            
            dgvKhachHang.Columns[0].HeaderText = "Mã KH";
            dgvKhachHang.Columns[1].HeaderText = "Họ tên khách hàng";         
            dgvKhachHang.Columns[2].HeaderText = "Ngày sinh";
            dgvKhachHang.Columns[3].HeaderText = "Điện thoại";
            dgvKhachHang.Columns[4].HeaderText = "Số nhà";
            dgvKhachHang.Columns[5].HeaderText = "Đường";
            dgvKhachHang.Columns[6].HeaderText = "Phường";
            dgvKhachHang.Columns[7].HeaderText = "Quận";
            dgvKhachHang.Columns[8].HeaderText = "Thành phố";
            dgvKhachHang.Columns[0].Width = 70;
            dgvKhachHang.Columns[1].Width = 120;
            dgvKhachHang.Columns[2].Width = 80;
            dgvKhachHang.Columns[3].Width = 80;
            dgvKhachHang.Columns[4].Width = 50;
            dgvKhachHang.Columns[7].Width = 80;
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
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["HOTEN"].Value.ToString();           
            mtbDienThoai.Text = dgvKhachHang.CurrentRow.Cells["DIENTHOAI"].Value.ToString();
            dateTimePickerNgSinh.Value = Convert.ToDateTime(dgvKhachHang.CurrentRow.Cells["NGSINH"].Value.ToString());
            txtSoNha.Text = dgvKhachHang.CurrentRow.Cells["SONHA"].Value.ToString();
            txtDuong.Text = dgvKhachHang.CurrentRow.Cells["DUONG"].Value.ToString();
            txtPhuong.Text = dgvKhachHang.CurrentRow.Cells["PHUONG"].Value.ToString();
            txtQuan.Text = dgvKhachHang.CurrentRow.Cells["QUAN"].Value.ToString();
            txtThanhPho.Text = dgvKhachHang.CurrentRow.Cells["THANHPHO"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }
    }
}
