using BLL_QLTC;
using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLTC
{
    public partial class frmDangKyNhanVien : Form
    {
        public frmDangKyNhanVien()
        {
            InitializeComponent();
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string tenDN = txtTenDN.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(sdt) ||
                string.IsNullOrWhiteSpace(tenDN) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (!IsValidPhoneNumber(sdt))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!");
                return;
            }
            if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ!");
                return;
            }

            NhanVienDTO nhanVienMoi = new NhanVienDTO
            {
                TenNV = hoTen,
                SDT = sdt,
                Email = email,
                DiaChi = diaChi
            };

            TaiKhoanBLL tkBLL = new TaiKhoanBLL();

            bool ok = tkBLL.DangKyTaiKhoanNhanVien(nhanVienMoi, tenDN, matKhau);

            if (ok)
                MessageBox.Show("Đăng ký nhân viên thành công!");
            else
                MessageBox.Show("Đăng ký thất bại!");
        }
        private bool IsValidPhoneNumber(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^0\d{9}$");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangNhap frm = new DangNhap();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

       
    }
}
