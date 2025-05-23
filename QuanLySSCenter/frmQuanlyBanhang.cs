using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_BVSK
{
    public partial class QuanlyBanhang : Form
    {
        string sCon = "Data Source=.\\MINHNGOCHOANG;Initial Catalog=BTLNhom7;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";


        public QuanlyBanhang()
        {
            InitializeComponent();
        }

        private void QuanlyBanhang_Load(object sender, EventArgs e)
        {
            //Bước 1: 
            SqlConnection con = new SqlConnection();
            try
            {
                con.Open();
            }
            catch (Exception )
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình kết nối SB");
            }

            //Bước 2 Lấy dữ liệu
            string sQuery1 = "select * from hoadon_ban";
            string sQuery2 = "select * from chitiet_ban";

            SqlDataAdapter adapter1 = new SqlDataAdapter(sQuery1, con);
            SqlDataAdapter adapter2 = new SqlDataAdapter(sQuery2, con);

            DataSet dsthoadonb = new DataSet();

            adapter1.Fill(dsthoadonb, "Hoadon_ban");


            dataGridView1.DataSource = dsthoadonb.Tables["Hoadon_ban"];

            con.Close(); //Bước 3
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
