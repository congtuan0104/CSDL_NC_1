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
            sql = "SELECT MAKH,HO+' '+TEN,NGSINH,DIENTHOAI FROM KHACHHANG";
            tblKH = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvKhachHang.DataSource = tblKH; //Nguồn dữ liệu            
            dgvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            dgvKhachHang.Columns[1].HeaderText = "Họ tên khách hàng";         
            dgvKhachHang.Columns[2].HeaderText = "Ngày sinh";
            dgvKhachHang.Columns[3].HeaderText = "Điện thoại";
            dgvKhachHang.Columns[0].Width = 60;
            dgvKhachHang.Columns[1].Width = 100;
            dgvKhachHang.Columns[2].Width = 70;
            dgvKhachHang.Columns[3].Width = 70;
            dgvKhachHang.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }
    }
}
