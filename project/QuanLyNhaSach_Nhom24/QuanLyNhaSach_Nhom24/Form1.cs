using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaSach_Nhom24
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            label1.BackColor=Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DangNhap changetoform = new DangNhap();
            changetoform.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
