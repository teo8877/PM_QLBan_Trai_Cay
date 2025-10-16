using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO_QLTC;
using BLL_QLTC;
 
namespace GUI_QLTC
{
    public partial class DangNhap : Form
    {
        
        private TaiKhoanBLL tkBLL=new TaiKhoanBLL();
        public DangNhap()
        {
            InitializeComponent();
            txtMatKhau.UseSystemPasswordChar = true;

        }


        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
           
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
            
        }
        public string LoaiTaiKhoan { get; private set; }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            TaiKhoanBLL bll = new TaiKhoanBLL();
            TaiKhoanDTO taiKhoan = bll.DangNhap(txtTenDangNhap.Text, txtMatKhau.Text);

            if (taiKhoan != null)
            {
                if (taiKhoan.TrangThai == "Khoa")
                {
                    MessageBox.Show("Tài khoản đã bị khóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoaiTaiKhoan = taiKhoan.LoaiTK; // Lưu lại loại tài khoản
                this.DialogResult = DialogResult.OK; // Báo cho form cha biết là đăng nhập thành công
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKyKhachHang frm = new frmDangKyKhachHang();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void linklblQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMatKhau frm = new QuenMatKhau();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

      

        private void btnTrangChu_DN_Click(object sender, EventArgs e)
        {
            this.Hide();
            using(GiaoDienChinh frm=new GiaoDienChinh())
            {
                frm.ShowDialog();
            }
            this.Show();
        }
    }
}
