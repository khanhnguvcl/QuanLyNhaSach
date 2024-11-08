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
    public partial class QuanLyNhanVien : Form
    {
        private string connectionString = "Data Source=LAPTOP-Q12JULH6\\KHANHKHIEMTON;Initial Catalog=dbQUANLYNHASACH;Integrated Security=True";

        public QuanLyNhanVien()
        {
            InitializeComponent();
            LoadNhanVienData();
        }

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
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

        private void quảnLýSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            QuanLySach changetoform = new QuanLySach();
            changetoform.Show();
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            QuanLyKhachHang changetoform = new QuanLyKhachHang();
            changetoform.Show();
        }

        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            QuanLyHoaDon changetoform = new QuanLyHoaDon();
            changetoform.Show();
        }

        private void quảnLýPhiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            QuanLyPhieuNhap changetoform = new QuanLyPhieuNhap();
            changetoform.Show();
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            QuanLyNhanVien changetoform = new QuanLyNhanVien();
            changetoform.Show();
        }

        private void quảnLýNhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            QuanLyNhaCungCap changetoform = new QuanLyNhaCungCap();
            changetoform.Show();
        }

        private void LoadNhanVienData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM NHANVIEN";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    dataGridViewNhanVien.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu nhân viên: " + ex.Message);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO NHANVIEN (IDNhanVien, HoTen, SoDienThoai, TaiKhoan, MatKhau) " +
                                   "VALUES (@IDNhanVien, @HoTen, @SoDienThoai, @TaiKhoan, @MatKhau)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDNhanVien", tbIDNhanVien.Text);
                    command.Parameters.AddWithValue("@HoTen", tbHoTen.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", tbSoDienThoai.Text);
                    command.Parameters.AddWithValue("@TaiKhoan", tbTaiKhoan.Text);
                    command.Parameters.AddWithValue("@MatKhau", tbMatKhau.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhân viên thành công!");
                    LoadNhanVienData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
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
                    string query = "UPDATE NHANVIEN SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, " +
                                   "TaiKhoan = @TaiKhoan, MatKhau = @MatKhau WHERE IDNhanVien = @IDNhanVien";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDNhanVien", tbIDNhanVien.Text);
                    command.Parameters.AddWithValue("@HoTen", tbHoTen.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", tbSoDienThoai.Text);
                    command.Parameters.AddWithValue("@TaiKhoan", tbTaiKhoan.Text);
                    command.Parameters.AddWithValue("@MatKhau", tbMatKhau.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật nhân viên thành công!");
                    LoadNhanVienData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message);
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
                    string query = "DELETE FROM NHANVIEN WHERE IDNhanVien = @IDNhanVien";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDNhanVien", tbIDNhanVien.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadNhanVienData(); // Cập nhật lại DataGridView nếu có
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
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
                    string query = "SELECT * FROM NHANVIEN WHERE IDNhanVien = @IDNhanVien";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDNhanVien", tbTimKiem.Text);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tbIDNhanVien.Text = reader["IDNhanVien"].ToString();
                        tbHoTen.Text = reader["HoTen"].ToString();
                        tbSoDienThoai.Text = reader["SoDienThoai"].ToString();
                        tbTaiKhoan.Text = reader["TaiKhoan"].ToString();
                        tbMatKhau.Text = reader["MatKhau"].ToString();
                        MessageBox.Show("Đã tìm thấy nhân viên!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên với mã đã nhập.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm nhân viên: " + ex.Message);
                }
            }
        }

        private void dataGridViewSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewNhanVien.Rows[e.RowIndex];
                tbIDNhanVien.Text = row.Cells["IDNhanVien"].Value.ToString();
                tbHoTen.Text = row.Cells["HoTen"].Value.ToString();
                tbSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                tbTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
                tbMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            tbIDNhanVien.Clear();
            tbHoTen.Clear();
            tbSoDienThoai.Clear();
            tbTaiKhoan.Clear();
            tbMatKhau.Clear();
        }
    }
}
