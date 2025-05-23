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
        string sCon = "Data Source=.\\MINHNGOCHOANG;Initial Catalog=BTLNhom7;Integrated Security=True;";
        public NhanVien()
        {
            InitializeComponent();
            ClearNhanVienInfo();
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
            string sMaNV = txb_NhapMa.Text.Trim();

            if (string.IsNullOrEmpty(sMaNV))
            {
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearNhanVienInfo(); // Xóa thông tin cũ nếu có
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(sCon))
                {
                    connection.Open(); // Mở kết nối đến CSDL

                    // Chuỗi truy vấn SQL
                    string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số để tránh SQL Injection
                        command.Parameters.AddWithValue("@MaNV", sMaNV);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Nếu tìm thấy bản ghi
                            {
                                // Đọc dữ liệu từ SqlDataReader và hiển thị lên các controls
                                // Ví dụ:
                                lblTenNV.Text = reader["TenNV"].ToString();
                                lblSDT.Text = reader["SDT"].ToString();

                            }
                            else
                            {
                                // Không tìm thấy nhân viên
                                MessageBox.Show($"Không tìm thấy nhân viên có mã: {sMaNV}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //ClearNhanVienInfo(); // Xóa thông tin cũ
                                txb_NhapMa.Text = "";
                            }
                        }   
           
                    } 
                }
            }
            catch (SqlException ex)
            {
                // Xử lý các lỗi liên quan đến CSDL
                MessageBox.Show($"Lỗi CSDL: {ex.Message}\nVui lòng kiểm tra chuỗi kết nối hoặc quyền truy cập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearNhanVienInfo();
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi chung khác
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearNhanVienInfo();
            }
        }
        private void ClearNhanVienInfo()
        {
            lblTenNV.Text = "";
            lblSDT.Text = "";

            // Nếu bạn có DataGridView hiển thị kết quả chi tiết, cũng cần làm trống nó:
            // guna2DataGridView1.DataSource = null;
        }
        private void bt_Them_Click(object sender, EventArgs e)
        {
            ThemNhanVien fthemnv =  new ThemNhanVien();

            // ShowDialog() sẽ chặn Form NhanVien cho đến khi ThemNhanVienForm đóng
            fthemnv.ShowDialog();
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
