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

        public string mahd;

        private void FormHoaDon_Chitiet_Load(object sender, EventArgs e)
        {
            loadDataGidView_CTDH();
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            textBox_CT_MAHD.Text = mahd;
            textBox_CT_MAHD.Enabled = false;
            
        }


        private void loadDataGidView_CTDH()
        {
            string sql1 = "SELECT MAHD, MASP, SOLUONG, GIABAN, GIAGIAM, THANHTIEN FROM CT_HD WHERE MAHD='"+mahd+"'";
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
            dataGridview_CTHD.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dataGridview_CTHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_MASP.Focus();
                return;
            }
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
            

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void ResetValues()
        {
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
            textBox_CT_MASP.Focus();
        }

        public void ThemHDtuFormHD()
        {
            textBox_CT_MAHD.Text = mahd;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            textBox_CT_MASP.Focus();
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
                MessageBox.Show("Chưa nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Sản phẩm này đã tồn tại trong hoá đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_MASP.Focus();
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
            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (table_CTHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (textBox_CT_MASP.Text.Trim() == "")
            {
                MessageBox.Show("Chọn sản phẩm muốn xoá trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xác nhận xoá sản phẩm này khỏi hoá đơn!!!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

            if (textBox_CT_MASP.Text == "")
            {
                MessageBox.Show("Chọn chi tiết hóa đơn cần sửa thông tin trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            string temp_MASP = textBox_CT_MASP.Text.Trim();

            if (textBox_CT_MASP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_MASP.Focus();
                return;
            };

            if (textBox_CT_SOLUONG.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập số lượng sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_CT_SOLUONG.Focus();
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
            MessageBox.Show("Sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnBoQua.Enabled = false;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
        }
    }
}
