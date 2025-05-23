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
        //Biến kết nối tới database
        string sCon = "Data Source=.\\MINHNGOCHOANG;Initial Catalog=BTLNhom7;Integrated Security=True;TrustServerCertificate=True;"; public NhanVien()
        {
            InitializeComponent(); //Giao diện
        }

        private void NhanVien_Load(object sender, EventArgs e)      
        {
            // Load lại dữ liệu (an toàn)
            LoadData();
        }

        //Hàm load: Tải dữ liệu vào
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open(); // Cần phải mở kết nối
                    string query = "SELECT * FROM NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet(); // Đổi tên biến dt thành ds cho rõ ràng hơn khi dùng DataSet
                    adapter.Fill(ds, "NhanVien");

                    // Đảm bảo tên DataGridView chính xác
                    guna2DataGridView1.DataSource = ds.Tables["NhanVien"]; // Nếu bạn dùng GunaUI2 DataGridView
                                                                           // Hoặc: dataGridView1.DataSource = ds.Tables["NhanVien"]; // Nếu bạn dùng DataGridView mặc định
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void txb_NhapMa_TextChanged(object sender, EventArgs e)
        {

        }

        //Tìm kiếm
        private void bt_Tim_Click(object sender, EventArgs e)
        {
            string sMaNV = txb_NhapMa.Text.Trim();

            if (string.IsNullOrEmpty(sMaNV))
            {
                // Sửa thông báo này, vì nếu trống thì không phải "không tồn tại"
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();
                    // Lỗi logic: tên cột trong WHERE clause không khớp với tên biến SQL parameter
                    // string query = "SELECT * FROM NhanVien WHERE sMaNV = @MaNV"; // Lỗi ở 'sMaNV'
                    string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV"; // Cột trong DB thường là MaNV

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@MaNV", sMaNV);

                    DataTable dt = new DataTable();
                    da.Fill(dt); // DataAdapter tự mở và đóng kết nối nếu cần, nhưng tốt hơn là tự quản lý

                    guna2DataGridView1.DataSource = dt;
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên phù hợp.");
                    }
                }
                catch (Exception ex)
                {
                    // Bắt và hiển thị lỗi nếu có vấn đề trong quá trình kết nối hoặc truy vấn CSDL
                    MessageBox.Show("Lỗi khi tìm kiếm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        //Thêm: khi form đóng hiện lại LoadData()
        private void bt_Them_Click(object sender, EventArgs e)
        {
            ThemNhanVien fthemnv = new ThemNhanVien();

            fthemnv.FormClosed += (s, args) => LoadData();
            // ShowDialog() sẽ chặn Form NhanVien cho đến khi ThemNhanVienForm đóng
            fthemnv.ShowDialog(); // Hiển thị form thêm
        }

        //Sửa: Lâys dòng chọn -> form Sửa NV -> LoadData
        private void bt_Sua_Click(object sender, EventArgs e)
        {
            // Đảm bảo tên DataGridView chính xác
            if (guna2DataGridView1.SelectedRows.Count > 0) // Thay dataGridView1 bằng guna2DataGridView1
            {
                // Lấy MaNV từ hàng được chọn
                string MaNV = guna2DataGridView1.SelectedRows[0].Cells["MaNV"].Value.ToString(); // Thay dataGridView1 bằng guna2DataGridView1

                // Tạo instance của SuaNhanVien và truyền MaNV vào constructor
                SuaNhanVien fSuaNV = new SuaNhanVien(MaNV); // Truyền MaNV vào đây!

                // Xử lý kết quả trả về từ form SuaNhanVien (tùy chọn)
                if (fSuaNV.ShowDialog() == DialogResult.OK) // Nếu form SuaNhanVien trả về OK (đã lưu thành công)
                {
                    LoadData(); // Tải lại dữ liệu
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void bt_Xoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string MaNV = dataGridView1.SelectedRows[0].Cells["MaNV"].Value.ToString();

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                    return;

                using (SqlConnection con = new SqlConnection(sCon))
                {
                    try
                    {
                        con.Open();
                        string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@MaNV", MaNV);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Không xóa được!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bt_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
