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
    public partial class QuanLySach : Form
    {
        private string connectionString = "Data Source=LAPTOP-Q12JULH6\\KHANHKHIEMTON;Initial Catalog=dbQUANLYNHASACH;Integrated Security=True";
        public QuanLySach()
        {
            InitializeComponent();
            LoadIDTheLoai();
            LoadSachData();
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
        private void LoadIDTheLoai()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDTheLoai FROM THELOAI";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cbIDTheLoai.Items.Add(reader["IDTheLoai"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void LoadSachData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM SACH"; 
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                
                    dataGridViewSach.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu sách: " + ex.Message);
                }
            }
        }

        private void quảnLýSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            QuanLySach changetoform = new QuanLySach();
            changetoform.Show();
        }

        private void QuanLySach_Load(object sender, EventArgs e)
        {
            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;
        }

        private void cbIDTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO SACH (IDSach, TenSach, TacGia, IDTheLoai, NhaXuatBan, NamXuatBan, GiaNhap, GiaBan, SoLuongTon) " +
                                   "VALUES (@IDSach, @TenSach, @TacGia, @IDTheLoai, @NhaXuatBan, @NamXuatBan, @GiaNhap, @GiaBan, @SoLuongTon)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDSach", tbIDSach.Text);
                    command.Parameters.AddWithValue("@TenSach", tbTenSach.Text);
                    command.Parameters.AddWithValue("@TacGia", tbTacGia.Text);
                    command.Parameters.AddWithValue("@IDTheLoai", cbIDTheLoai.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@NhaXuatBan", tbNhaXuatBan.Text);
                    command.Parameters.AddWithValue("@NamXuatBan", int.Parse(tbNamXuatBan.Text));
                    command.Parameters.AddWithValue("@GiaNhap", decimal.Parse(tbGiaNhap.Text));
                    command.Parameters.AddWithValue("@GiaBan", decimal.Parse(tbGiaBan.Text));
                    command.Parameters.AddWithValue("@SoLuongTon", int.Parse(tbSoLuongTon.Text));

                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm sách thành công!");
                    LoadSachData(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sách: " + ex.Message);
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
                    string query = "UPDATE SACH SET TenSach = @TenSach, TacGia = @TacGia, IDTheLoai = @IDTheLoai, " +
                                   "NhaXuatBan = @NhaXuatBan, NamXuatBan = @NamXuatBan, GiaNhap = @GiaNhap, GiaBan = @GiaBan, " +
                                   "SoLuongTon = @SoLuongTon WHERE IDSach = @IDSach";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDSach", tbIDSach.Text);
                    command.Parameters.AddWithValue("@TenSach", tbTenSach.Text);
                    command.Parameters.AddWithValue("@TacGia", tbTacGia.Text);
                    command.Parameters.AddWithValue("@IDTheLoai", cbIDTheLoai.SelectedItem.ToString());
                    command.Parameters.AddWithValue("@NhaXuatBan", tbNhaXuatBan.Text);
                    command.Parameters.AddWithValue("@NamXuatBan", int.Parse(tbNamXuatBan.Text));
                    command.Parameters.AddWithValue("@GiaNhap", decimal.Parse(tbGiaNhap.Text));
                    command.Parameters.AddWithValue("@GiaBan", decimal.Parse(tbGiaBan.Text));
                    command.Parameters.AddWithValue("@SoLuongTon", int.Parse(tbSoLuongTon.Text));

                    command.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật sách thành công!");
                    LoadSachData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật sách: " + ex.Message);
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
                    string query = "DELETE FROM SACH WHERE IDSach = @IDSach";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDSach", tbIDSach.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Xóa sách thành công!");
                    LoadSachData(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sách: " + ex.Message);
                }
            }
        }

        private void dataGridViewSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dataGridViewSach.Rows[e.RowIndex];

                
                tbIDSach.Text = row.Cells["IDSach"].Value.ToString();
                tbTenSach.Text = row.Cells["TenSach"].Value.ToString();
                tbTacGia.Text = row.Cells["TacGia"].Value.ToString();
                cbIDTheLoai.SelectedItem = row.Cells["IDTheLoai"].Value.ToString();
                tbNhaXuatBan.Text = row.Cells["NhaXuatBan"].Value.ToString();
                tbNamXuatBan.Text = row.Cells["NamXuatBan"].Value.ToString();
                tbGiaNhap.Text = row.Cells["GiaNhap"].Value.ToString();
                tbGiaBan.Text = row.Cells["GiaBan"].Value.ToString();
                tbSoLuongTon.Text = row.Cells["SoLuongTon"].Value.ToString();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            tbIDSach.Clear();
            tbTenSach.Clear();
            tbTacGia.Clear();
            cbIDTheLoai.SelectedIndex = -1;
            tbNhaXuatBan.Clear();
            tbNamXuatBan.Clear();
            tbGiaNhap.Clear();
            tbGiaBan.Clear();
            tbSoLuongTon.Clear();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM SACH WHERE IDSach = @IDSach";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IDSach", tbTimKiem.Text);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) 
                    {
                        
                        tbIDSach.Text = reader["IDSach"].ToString();
                        tbTenSach.Text = reader["TenSach"].ToString();
                        tbTacGia.Text = reader["TacGia"].ToString();
                        cbIDTheLoai.SelectedItem = reader["IDTheLoai"].ToString();
                        tbNhaXuatBan.Text = reader["NhaXuatBan"].ToString();
                        tbNamXuatBan.Text = reader["NamXuatBan"].ToString();
                        tbGiaNhap.Text = reader["GiaNhap"].ToString();
                        tbGiaBan.Text = reader["GiaBan"].ToString();
                        tbSoLuongTon.Text = reader["SoLuongTon"].ToString();
                        MessageBox.Show("Đã tìm thấy sách!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sách với mã sách đã nhập.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm sách: " + ex.Message);
                }
            }
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
    }
}
