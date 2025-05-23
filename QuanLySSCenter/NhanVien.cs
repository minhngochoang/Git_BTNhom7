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
            
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void txb_NhapMa_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_Tim_Click(object sender, EventArgs e)
        {

        }

        private void bt_Them_Click(object sender, EventArgs e)
        {

        }
        private void bt_Sua_Click(object sender, EventArgs e)
        {

        }
        private void bt_Xoa_Click(object sender, EventArgs e)
        {

        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
