﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace DoAn01.Class
{
    class Functions
    {
        public static SqlConnection Con;  //Khai báo đối tượng kết nối        

        public static void Connect()
        {
           
            Con = new SqlConnection();   //Khởi tạo đối tượng
            //Con.ConnectionString = @"Data Source = CONGTUAN\CONGTUAN; Initial Catalog = DoAn01_N1; Integrated Security = True";
            Con.ConnectionString = DoAn01.Properties.Settings.Default.QLBHConnection;
            Con.Open();                  //Mở kết nối
            //Kiểm tra kết nối
            if (Con.State == ConnectionState.Open)
                MessageBox.Show("Kết nối dữ liệu thành công");
            else MessageBox.Show("Không thể kết nối với dữ liệu");

        }
        public static void Disconnect()
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();   	//Đóng kết nối
                Con.Dispose(); 	//Giải phóng tài nguyên
                Con = null;
            }
        }

        //Lấy dữ liệu vào bảng
        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter DataAdapter = new SqlDataAdapter(); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            //Tạo đối tượng thuộc lớp SqlCommand
            DataAdapter.SelectCommand = new SqlCommand();
            DataAdapter.SelectCommand.Connection = Functions.Con; //Kết nối cơ sở dữ liệu
            DataAdapter.SelectCommand.CommandText = sql; //Lệnh SQL
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            DataAdapter.Fill(table);
            return table;
        }
    }
}
