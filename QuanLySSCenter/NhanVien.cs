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
        string sCon = "Data Source=.\\MINHNGOCHOANG;Initial Catalog=BTLNhom7;Integrated Security=True;";
        public NhanVien()
        {
            InitializeComponent(); //Giao diện
        }

        private void NhanVien_Load(object sender, EventArgs e)      
        {
            // Kết nối đến SQL Server
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open(); // Mở kết nối
                    string sQuery = "SELECT * FROM NhaCungCap";
                    SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "NhaCungCap");

                    dataGridView1.DataSource = ds.Tables["NhaCungCap"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
                }
            }

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
                    string query = "SELECT * FROM NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataSet dt = new DataSet();
                    adapter.Fill(dt, "NhanVien");

                    dataGridView1.DataSource = dt.Tables["NhanVien"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
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
                MessageBox.Show("Mã nhân viên không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection con = new SqlConnection(sCon))
            {
                string query = "SELECT * FROM NhanVien WHERE sMaNV = @MaNV";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@MaNV", sMaNV);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên phù hợp.");
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string MaNV = dataGridView1.SelectedRows[0].Cells["MaNV"].Value.ToString();

                SuaNhanVien fSuaNV = new SuaNhanVien();

                fSuaNV.ShowDialog();

                LoadData(); // Tải lại dữ liệu sau khi sửa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
