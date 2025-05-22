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

namespace QuanLySSCenter
{
    public partial class NhanVien : Form
    {
        string sCon = "Data Source=.\\MINHNGOCHOANG;Initial Catalog=BTLNhom7;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public NhanVien()
        {
            InitializeComponent();
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
            //Bước 1: 
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình kết nối SB");
            }

            //Bước 2 Lấy dữ liệu
            string sQuery = "select * from nhanvien";
            SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "NhanVien");

            dataGridView1.DataSource = ds.Tables["NhanVien"]; //Đổ dữ liệu từ dataset datagridview

            con.Close(); //Bước 3
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
