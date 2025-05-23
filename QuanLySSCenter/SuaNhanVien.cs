using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
//using System.Data.SqlClient;

namespace QuanLySSCenter
{
    public partial class SuaNhanVien : Form
    {
        string sCon = "Data Source=.\\MINHNGOCHOANG;Initial Catalog=BTLNhom7;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        // Biến để lưu trữ Mã Nhân Viên được truyền từ form NhanVien
        private string MaNV;
        public SuaNhanVien()
        {
            InitializeComponent();
           // this.MaNV = MaNV;

        }
        private void SuaNhanVien_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(sCon))
            {
                con.Open();
                string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MaNV", MaNV);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txb_MaNV.Text = reader["MaNV"].ToString();
                    txb_TenNV.Text = reader["TenNV"].ToString();
                    txb_Sdt.Text = reader["SDT"].ToString();


                    // Chặn sửa mã NCC nhưng vẫn hiển thị đẹp
                    txb_MaNV.ReadOnly = true;
                    txb_MaNV.BackColor = Color.LightGray;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
        }
        private void bt_Luu_Click(object sender, EventArgs e)
        {
            string tenNV = txb_TenNV.Text.Trim();
            string sdt = txb_Sdt.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenNV))
            {
                MessageBox.Show("Tên nhân viên không được để trống.");
                txb_TenNV.Focus();
                return;
            }

            if (!Regex.IsMatch(sdt, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại phải có đúng 10 chữ số.");
                txb_Sdt.Focus();
                return;
            }

            using (SqlConnection con = new SqlConnection(sCon))
            {
                string updateQuery = "UPDATE NhanVien SET TenNV = @TenNV, SDT = @SDT WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@TenNV", tenNV);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@MaNV", MaNV);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Cập nhật thành công.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.");
                }
            }
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Báo hiệu hủy cho Form chính
            this.Close(); // Đóng Form này
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txb_MaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_TenNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txb_Sdt_TextChanged(object sender, EventArgs e)
        {

        }
                                             
        


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
