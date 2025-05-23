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
using System.Text.RegularExpressions;
//using System.Data.SqlClient;

namespace QuanLySSCenter
{
    public partial class SuaNhanVien : Form
    {
        string sCon = "Data Source=MINHNGOCHOANG;Initial Catalog=BTLNhom7;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        // Biến để lưu trữ Mã Nhân Viên được truyền từ form NhanVien
        private string _maNVCanSua;
        public SuaNhanVien(string MaNV) : this() // Constructor nhận 1 tham số string
        {
            // Gán giá trị của tham số 'maNhanVien' (được truyền từ form NhanVien)
            // vào biến thành viên '_maNVCanSua' của form SuaNhanVien này.
            this._maNVCanSua = MaNV; // <-- Dòng này là rất quan trọng để lưu MaNV
        }
        public SuaNhanVien()
        {
            InitializeComponent(); // Khởi tạo các thành phần giao diện
                                   // Đặt txb_MaNV ở chế độ chỉ đọc ngay khi khởi tạo
                                   // Đảm bảo txb_MaNV là tên chính xác của TextBox Mã NV trên form SuaNhanVien
            if (txb_MaNV != null) // Kiểm tra null an toàn (mặc dù InitializeComponent() thường đảm bảo không null)
            {
                txb_MaNV.ReadOnly = true;
            }
        }
        private void SuaNhanVien_Load(object sender, EventArgs e)
        {
            // Sử dụng _maNVCanSua để tải dữ liệu của nhân viên từ CSDL
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@MaNV", _maNVCanSua); // Dùng biến đã lưu

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txb_MaNV.Text = reader["MaNV"].ToString(); // Điền Mã NV vào TextBox
                        txb_TenNV.Text = reader["TenNV"].ToString(); // Giả sử txb_TenNV là TextBox tên nhân viên
                        txb_Sdt.Text = reader["SDT"].ToString(); // Giả sử txb_SDT là TextBox số điện thoại
                                                                       // Điền các TextBox khác tương ứng với các cột trong bảng NhanVien
                                                                       // txtMST.Text = reader["MST"].ToString(); // Nếu có cột MST
                                                                       // txtDiaChiNCC.Text = reader["DiaChi"].ToString(); // Nếu có cột DiaChi
                                                                       // ...
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
        private void bt_Luu_Click(object sender, EventArgs e)
        {
            // ... (ValidateInput())
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();
                    string query = @"UPDATE NhanVien
                                 SET TenNV = @TenNV,
                                     SDT = @SDT

                                 WHERE MaNV = @MaNV"; // Cập nhật dựa trên MaNV ban đầu

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@MaNV", _maNVCanSua); // Dùng _maNVCanSua để xác định bản ghi cần sửa
                    cmd.Parameters.AddWithValue("@TenNV", txb_TenNV.Text);
                    cmd.Parameters.AddWithValue("@SDT", txb_Sdt.Text); // Lấy giá trị từ TextBox
                                                                       // Thêm các tham số khác tương ứng
                                                                       // cmd.Parameters.AddWithValue("@DiaChi", txb_DiaChi.Text);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK; // Báo hiệu thành công
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không cập nhật được! Có thể không có thay đổi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
