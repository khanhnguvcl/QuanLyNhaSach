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
    public partial class QuanLyNhaCungCap : Form
    {
        private string connectionString = "Data Source=LAPTOP-Q12JULH6\\KHANHKHIEMTON;Initial Catalog=dbQUANLYNHASACH;Integrated Security=True";

        public QuanLyNhaCungCap()
        {
            InitializeComponent();
            LoadNhaCungCapData();
        }

        private void QuanLyNhaCungCap_Load(object sender, EventArgs e)
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
        private void LoadNhaCungCapData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM NHACUNGCAP";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    dataGridViewNhaCungCap.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu nhà cung cấp: " + ex.Message);
                }
            }
        }

        private void dataGridViewSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewNhaCungCap.Rows[e.RowIndex];
                tbIDNhaCungCap.Text = row.Cells["IDNhaCungCap"].Value.ToString();
                tbTenNhaCungCap.Text = row.Cells["TenNhaCungCap"].Value.ToString();
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
                    string query = "INSERT INTO NHACUNGCAP (IDNhaCungCap, TenNhaCungCap, DiaChi, SoDienThoai, Email) " +
                                   "VALUES (@IDNhaCungCap, @TenNhaCungCap, @DiaChi, @SoDienThoai, @Email)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDNhaCungCap", tbIDNhaCungCap.Text);
                    command.Parameters.AddWithValue("@TenNhaCungCap", tbTenNhaCungCap.Text);
                    command.Parameters.AddWithValue("@DiaChi", tbDiaChi.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", tbSoDienThoai.Text);
                    command.Parameters.AddWithValue("@Email", tbEmail.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm nhà cung cấp thành công!");
                    LoadNhaCungCapData(); // Cập nhật lại DataGridView nếu có
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm nhà cung cấp: " + ex.Message);
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
                    string query = "UPDATE NHACUNGCAP SET TenNhaCungCap = @TenNhaCungCap, DiaChi = @DiaChi, " +
                                   "SoDienThoai = @SoDienThoai, Email = @Email WHERE IDNhaCungCap = @IDNhaCungCap";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDNhaCungCap", tbIDNhaCungCap.Text);
                    command.Parameters.AddWithValue("@TenNhaCungCap", tbTenNhaCungCap.Text);
                    command.Parameters.AddWithValue("@DiaChi", tbDiaChi.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", tbSoDienThoai.Text);
                    command.Parameters.AddWithValue("@Email", tbEmail.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật nhà cung cấp thành công!");
                    LoadNhaCungCapData(); // Cập nhật lại DataGridView nếu có
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật nhà cung cấp: " + ex.Message);
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
                    string query = "DELETE FROM NHACUNGCAP WHERE IDNhaCungCap = @IDNhaCungCap";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDNhaCungCap", tbIDNhaCungCap.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa nhà cung cấp thành công!");
                    LoadNhaCungCapData(); // Cập nhật lại DataGridView nếu có
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nhà cung cấp: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            tbIDNhaCungCap.Clear();
            tbTenNhaCungCap.Clear();
            tbDiaChi.Clear();
            tbSoDienThoai.Clear();
            tbEmail.Clear();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM NHACUNGCAP WHERE IDNhaCungCap = @IDNhaCungCap";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDNhaCungCap", tbTimKiem.Text);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) 
                    {
                        tbIDNhaCungCap.Text = reader["IDNhaCungCap"].ToString();
                        tbTenNhaCungCap.Text = reader["TenNhaCungCap"].ToString();
                        tbDiaChi.Text = reader["DiaChi"].ToString();
                        tbSoDienThoai.Text = reader["SoDienThoai"].ToString();
                        tbEmail.Text = reader["Email"].ToString();
                        MessageBox.Show("Đã tìm thấy nhà cung cấp!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhà cung cấp với mã đã nhập.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm nhà cung cấp: " + ex.Message);
                }
            }
        }
    }
}
