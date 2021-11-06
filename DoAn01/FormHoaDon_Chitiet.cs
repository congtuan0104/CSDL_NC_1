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
using System.Text.RegularExpressions;

namespace DoAn01
{
    public partial class FormHoaDon_Chitiet : Form
    {
        DataTable table_CTHD;

       
        public FormHoaDon_Chitiet()
        {
            InitializeComponent();
        }

        private void FormHoaDon_Chitiet_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            loadDataGidView_CTDH();
            //Class.Functions.Disconnect();
        }


        private void loadDataGidView_CTDH()
        {
            string sql1 = "SELECT MAHD, MASP, SOLUONG, GIABAN, GIAGIAM, THANHTIEN FROM CT_HD";
            table_CTHD = Class.Functions.GetDataToTable(sql1);
            dataGridview_CTHD.DataSource = table_CTHD;
            dataGridview_CTHD.Columns[0].HeaderText = "MÃ HÓA ĐƠN";
            dataGridview_CTHD.Columns[1].HeaderText = "MÃ SẢN PHẨM";
            dataGridview_CTHD.Columns[2].HeaderText = "SỐ LƯỢNG";
            dataGridview_CTHD.Columns[3].HeaderText = "GIÁ BÁN";
            dataGridview_CTHD.Columns[4].HeaderText = "GIÁ GIẢM";
            dataGridview_CTHD.Columns[5].HeaderText = "THÀNH TIỀN";
            dataGridview_CTHD.Columns[0].Width = 125;
            dataGridview_CTHD.Columns[1].Width = 125;
            dataGridview_CTHD.Columns[2].Width = 125;
            dataGridview_CTHD.Columns[3].Width = 125;
            dataGridview_CTHD.Columns[4].Width = 125;
            dataGridview_CTHD.Columns[5].Width = 125;
        }









        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (btnThem.Enabled == false)
            //{
            //    MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtMaKH.Focus();
            //    return;
            //}
            if (table_CTHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            textBox_CT_MAHD.Text = dataGridview_CTHD.CurrentRow.Cells["MAHD"].Value.ToString();
            textBox_CT_MASP.Text = dataGridview_CTHD.CurrentRow.Cells["MASP"].Value.ToString();
            textBox_CT_SOLUONG.Text = dataGridview_CTHD.CurrentRow.Cells["SOLUONG"].Value.ToString();
            textBox_CT_GIABAN.Text = dataGridview_CTHD.CurrentRow.Cells["GIABAN"].Value.ToString();
            textBox_CT_GIAGIAM.Text = dataGridview_CTHD.CurrentRow.Cells["GIAGIAM"].Value.ToString();
            textBox_CT_THANHTIEN.Text = dataGridview_CTHD.CurrentRow.Cells["THANHTIEN"].Value.ToString();
            //txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["HO"].Value.ToString() + " " + dgvKhachHang.CurrentRow.Cells["TEN"].Value.ToString();
            //mtbDienThoai.Text = dgvKhachHang.CurrentRow.Cells["DIENTHOAI"].Value.ToString();
            //if (dgvKhachHang.CurrentRow.Cells["NGSINH"].Value.ToString() != "")
            //{
            //    dateTimePickerNgSinh.Value = Convert.ToDateTime(dgvKhachHang.CurrentRow.Cells["NGSINH"].Value.ToString());
            //}

            //txtSoNha.Text = dgvKhachHang.CurrentRow.Cells["SONHA"].Value.ToString();
            //txtDuong.Text = dgvKhachHang.CurrentRow.Cells["DUONG"].Value.ToString();
            //txtPhuong.Text = dgvKhachHang.CurrentRow.Cells["PHUONG"].Value.ToString();
            //txtQuan.Text = dgvKhachHang.CurrentRow.Cells["QUAN"].Value.ToString();
            //txtThanhPho.Text = dgvKhachHang.CurrentRow.Cells["THANHPHO"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void ResetValues()
        {
            textBox_CT_MAHD.Text = "";
            textBox_CT_MASP.Text = "";
            textBox_CT_SOLUONG.Text = "";
            textBox_CT_GIABAN.Text = "";
            textBox_CT_GIAGIAM.Text = "";
            textBox_CT_THANHTIEN.Text = "";
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            textBox_CT_MAHD.Enabled = true;
            textBox_CT_MAHD.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (textBox_CT_MAHD.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_MAHD.Focus();
                return;
            }
            if (textBox_CT_MASP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập max sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_MASP.Focus();
                return;
            }
            if (textBox_CT_SOLUONG.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chưa nhập số lượng sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_SOLUONG.Focus();
                return;
            }
            

            //Kiểm tra đã tồn tại chi tiết hóa đơn chưa
            sql = "SELECT MAHD FROM CT_HD WHERE MAHD='" + textBox_CT_MAHD.Text.Trim() + "'AND MASP='"+textBox_CT_MASP.Text.Trim()+"'";
            if (Functions.CheckValue(sql))
            {
                MessageBox.Show("Chi tiết hóa đơn đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_MAHD.Focus();
                return;
            }
            //Chèn thêm
            

            sql = "INSERT INTO CT_HD(MAHD, MASP, SOLUONG) VALUES ("
                + "N'" + textBox_CT_MAHD.Text.Trim()
                + "',N'" + textBox_CT_MASP.Text.Trim()
                +"'," +textBox_CT_SOLUONG.Text.Trim()
                + ")";
            Functions.RunSQL(sql);
            loadDataGidView_CTDH();
            //LoadDataGridView();
            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            textBox_CT_MAHD.Enabled = false;
            textBox_CT_MASP.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (table_CTHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (textBox_CT_MAHD.Text.Trim() == "")
            {
                MessageBox.Show("Chọn chi tiết hóa đơn muốn xoá trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xác nhận xoá chi tiết đơn hàng này!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE CT_HD WHERE MAHD=N'" + textBox_CT_MAHD.Text + "' AND MASP=N'"+ textBox_CT_MASP.Text + "'";
                Functions.RunSqlDel(sql);
                loadDataGidView_CTDH();
                ResetValues();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (table_CTHD.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            if (textBox_CT_MAHD.Text == "")
            {
                MessageBox.Show("Chọn chi tiết hóa đơn cần sửa thông tin trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            string temp_MASP = textBox_CT_MASP.Text.Trim();

            if (textBox_CT_MASP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_MASP.Focus();
                return;
            };

            if (textBox_CT_SOLUONG.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_SOLUONG.Focus();
                return;
            };

            if (textBox_CT_GIABAN.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá bán của sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_GIABAN.Focus();
                return;
            };

            if (textBox_CT_GIAGIAM.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá giảm của sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_GIAGIAM.Focus();
                return;
            };

            sql = "UPDATE CT_HD SET "
                + "MASP = N'" + textBox_CT_MASP.Text.Trim() + "', "
                + "SOLUONG = " + textBox_CT_SOLUONG.Text + ", "
                + "GIABAN = " + textBox_CT_GIABAN.Text + ", "
                + "GIAGIAM = " + textBox_CT_GIAGIAM.Text + " "
                + "WHERE MAHD = N'" + textBox_CT_MAHD.Text.Trim() + "' AND MASP = N'" + temp_MASP + "';";

            System.Diagnostics.Debug.WriteLine(sql);
            Functions.RunSQL(sql);
            loadDataGidView_CTDH();
            ResetValues();
            MessageBox.Show("Sửa thông tin chi tiết đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnBoQua.Enabled = false;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
