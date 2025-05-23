using Microsoft.IdentityModel.Protocols;
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

namespace QuanLySSCenter
{
    public partial class ThemNhanVien : Form
    {
        string sCon = "Data Source=.\\MINHNGOCHOANG;Initial Catalog=BTLNhom7;Integrated Security=True;";

        public ThemNhanVien()
        {
            InitializeComponent();
        }
        private void ThemNhanVien_Load(object sender, EventArgs e)
        {
            
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_luu_Click(object sender, EventArgs e)
        {
            //B1
        

            //B1: Lấy dữ liệu
            string sMaNV = txb_MaNV.Text.Trim();
            string sTenNV = txb_TenNV.Text;
            string sSdt = txb_Sdt.Text.Trim();

            //B2: Kiểm tra tính hợp lệ
            if (string.IsNullOrEmpty(sMaNV))
            {
                MessageBox.Show("Mã nhân viên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txb_MaNV.Focus(); // Đặt con trỏ vào ô lỗi
                return; // Dừng hàm lại
            }
            // Kiểm tra đúng 9 ký tự
            if (sMaNV.Length != 9)
            {
                MessageBox.Show("Mã nhân viên phải có 9 ký tự và theo định dạng NV*******.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txb_MaNV.Focus();
                return;
            }
            if (string.IsNullOrEmpty(sTenNV))
            {
                MessageBox.Show("Tên nhân viên không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txb_TenNV.Focus();
                return; // Dừng hàm lại
            }
            if (string.IsNullOrEmpty(sSdt))
            {
                MessageBox.Show("Số điện thoại không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txb_Sdt.Focus();
                return;
            }
            // Kiểm tra đúng 10 chữ số
            if (sSdt.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có đúng 10 chữ số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txb_Sdt.Focus();
                return;
            }
            // Lấy chuỗi kết nối từ App.config
            try
            {
                using (SqlConnection connection = new SqlConnection(sCon))
                {
                    connection.Open();

                    // Kiểm tra xem Mã nhân viên đã tồn tại chưa (quan trọng cho thêm mới)
                    string checkSql = "SELECT COUNT(*) FROM NhanVien WHERE sMaNV = @MaNV";
                    using (SqlCommand checkCommand = new SqlCommand(checkSql, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@MaNV", sMaNV);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Mã nhân viên này đã tồn tại. Vui lòng nhập mã khác.", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txb_MaNV.Focus();
                            return; // Dừng hàm
                        }
                    }

                    // Thực hiện câu lệnh INSERT (chỉ INSERT, không có UPDATE ở đây)
                    string sqlQuery = "INSERT INTO NhanVien (MaNV, TenNV, Sdt) VALUES (@MaNV, @TenNV, @SDT)";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaNV", sMaNV);
                        command.Parameters.AddWithValue("@TenNV", sTenNV);
                        command.Parameters.AddWithValue("@SDT", sSdt);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm nhân viên mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK; // Báo hiệu thành công cho Form chính
                            this.Close(); // Đóng Form này
                        }
                        else
                        {
                            MessageBox.Show("Không thể thêm nhân viên mới. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.None;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}\nMã lỗi: {ex.Number}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }
        
        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Báo hiệu hủy cho Form chính
            this.Close(); // Đóng Form này
        }
    }
}
