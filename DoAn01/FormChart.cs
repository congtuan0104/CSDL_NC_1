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
    public partial class FormChart : Form
    {
        DataTable tblDS;
        public FormChart()
        {
            InitializeComponent();
        }

        private void FormChart_Load(object sender, EventArgs e)
        {
            fillChart();
        }

        private void fillChart()
        {
            string sql;
            sql = "  SELECT (CAST(MONTH(NGAYLAP) AS VARCHAR)+'/'+CAST(YEAR(NGAYLAP) AS VARCHAR)) AS TIME,SUM(TONGTIEN) AS DOANHTHU " +
                    "FROM HOADON " +
                    "GROUP BY MONTH(NGAYLAP),YEAR(NGAYLAP) " +
                    "ORDER BY YEAR(NGAYLAP),MONTH(NGAYLAP)";
            tblDS = Class.Functions.GetDataToTable(sql);
            chartDoanhThu.DataSource = tblDS;
            chartDoanhThu.Series["Doanh Thu"].XValueMember = "TIME";
            chartDoanhThu.Series["Doanh Thu"].YValueMembers = "DOANHTHU";
            chartDoanhThu.Titles.Add("Biểu đồ doanh số hàng tháng");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
