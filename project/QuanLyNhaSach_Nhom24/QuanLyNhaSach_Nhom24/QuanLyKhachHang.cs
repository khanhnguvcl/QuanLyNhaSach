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

namespace QuanLyNhaSach_Nhom24
{
    public partial class QuanLyKhachHang : Form
    {
        private string connectionString = "Data Source=LAPTOP-Q12JULH6\\KHANHKHIEMTON;Initial Catalog=dbQUANLYNHASACH;Integrated Security=True";

        public QuanLyKhachHang()
        {
            InitializeComponent();
            LoadKhachHangData();
        }

        private void QuanLyKhachHang_Load(object sender, EventArgs e)
        {
            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                this.Close();
                DangNhap changetoform = new DangNhap();
                changetoform.Show();
            }
        }
        private void LoadKhachHangData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM KHACHHANG";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                   
                    dataGridViewKhachHang.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            tbIDKhachHang.Clear();
            tbHoTen.Clear();
            tbDiaChi.Clear();
            tbSoDienThoai.Clear();
            tbEmail.Clear();
        }

        private void dataGridViewSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dataGridViewKhachHang.Rows[e.RowIndex];
                tbIDKhachHang.Text = row.Cells["IDKhachHang"].Value.ToString();
                tbHoTen.Text = row.Cells["HoTen"].Value.ToString();
                tbDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                
                tbSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                tbEmail.Text = row.Cells["Email"].Value.ToString();
              
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO KHACHHANG (IDKhachHang, HoTen, DiaChi, SoDienThoai, Email) " +
                                   "VALUES (@IDKhachHang, @HoTen, @DiaChi, @SoDienThoai, @Email)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDKhachHang", tbIDKhachHang.Text);
                    command.Parameters.AddWithValue("@HoTen", tbHoTen.Text);
                    command.Parameters.AddWithValue("@DiaChi", tbDiaChi.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", tbSoDienThoai.Text);
                    command.Parameters.AddWithValue("@Email", tbEmail.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm khách hàng thành công!");
                    LoadKhachHangData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE KHACHHANG SET HoTen = @HoTen, DiaChi = @DiaChi, " +
                                   "SoDienThoai = @SoDienThoai, Email = @Email WHERE IDKhachHang = @IDKhachHang";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDKhachHang", tbIDKhachHang.Text);
                    command.Parameters.AddWithValue("@HoTen", tbHoTen.Text);
                    command.Parameters.AddWithValue("@DiaChi", tbDiaChi.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", tbSoDienThoai.Text);
                    command.Parameters.AddWithValue("@Email", tbEmail.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật khách hàng thành công!");
                    LoadKhachHangData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM KHACHHANG WHERE IDKhachHang = @IDKhachHang";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDKhachHang", tbIDKhachHang.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa khách hàng thành công!");
                    LoadKhachHangData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM KHACHHANG WHERE IDKhachHang = @IDKhachHang";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDKhachHang", tbTimKiem.Text);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tbIDKhachHang.Text = reader["IDKhachHang"].ToString();
                        tbHoTen.Text = reader["HoTen"].ToString();
                        tbDiaChi.Text = reader["DiaChi"].ToString();
                        tbSoDienThoai.Text = reader["SoDienThoai"].ToString();
                        tbEmail.Text = reader["Email"].ToString();
                        MessageBox.Show("Đã tìm thấy khách hàng!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng với mã đã nhập.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm khách hàng: " + ex.Message);
                }
            }
        }
    }
}
