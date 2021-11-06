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
    public partial class FormThongKe : Form
    {
        DataTable tblTK;
        public FormThongKe()
        {
            InitializeComponent();
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "  SELECT (CAST(MONTH(NGAYLAP) AS VARCHAR)+'/'+CAST(YEAR(NGAYLAP) AS VARCHAR)) AS TIME,SUM(TONGTIEN) AS DOANHTHU " +
                "FROM HOADON " +
                "GROUP BY MONTH(NGAYLAP),YEAR(NGAYLAP) " +
                "ORDER BY YEAR(NGAYLAP),MONTH(NGAYLAP)";
            tblTK = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvThongKe.DataSource = tblTK; //Nguồn dữ liệu            
            dgvThongKe.Columns[0].HeaderText = "Tháng";
            dgvThongKe.Columns[1].HeaderText = "Doanh thu";
            dgvThongKe.Columns[0].Width = 70;
            dgvThongKe.Columns[1].Width = 150;
            dgvThongKe.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvThongKe.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvThongKe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tblTK.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtThang.Text = dgvThongKe.CurrentRow.Cells["TIME"].Value.ToString();
            txtDoanhThu.Text = dgvThongKe.CurrentRow.Cells["DOANHTHU"].Value.ToString();
        }

        private void btnBieuDo_Click(object sender, EventArgs e)
        {
            FormChart dlgChart = new FormChart();
            dlgChart.ShowDialog();
        }
    }
}
