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
    public partial class frmDangKyKhachHang : Form
    {
        public frmDangKyKhachHang()
        {
            InitializeComponent();
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

        private void btnDangKyKH_Click(object sender, EventArgs e)
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
                MessageBox.Show("Vui lòng nhập đầy đủ họ tên, số điện thoại, tên đăng nhập và mật khẩu!");
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

            // Tạo đối tượng KhachHang
            KhachHangDTO khachHangMoi = new KhachHangDTO
            {
                HoTen = hoTen,
                SDT = sdt,
                Email = email,
                DiaChi = diaChi,
                LoaiKhachHang = "Thuong"
            };

            TaiKhoanBLL tkBLL = new TaiKhoanBLL();
            bool ok = tkBLL.DangKyTaiKhoanKhachHang(khachHangMoi, tenDN, matKhau);
            if (ok)
                MessageBox.Show("Đăng ký thành công!");
            else
                MessageBox.Show("Đăng ký thất bại!");
        }

        private void linkLabel1_DangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangNhap frm = new DangNhap();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
