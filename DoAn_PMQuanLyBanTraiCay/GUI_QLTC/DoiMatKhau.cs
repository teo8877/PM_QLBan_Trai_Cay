using BLL_QLTC;
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
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
            txtMatKhauCu.UseSystemPasswordChar = true;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            txtNhapLaiMatKhauMoi.UseSystemPasswordChar = true;


        }
        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu checkbox được tick -> hiện mật khẩu
            // Nếu không -> ẩn mật khẩu
            txtMatKhauCu.UseSystemPasswordChar = !chkHienMatKhau.Checked;
            txtMatKhauMoi.UseSystemPasswordChar = !chkHienMatKhau.Checked;
            txtNhapLaiMatKhauMoi.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDN.Text.Trim(); // LẤY GIÁ TRỊ TỪ Ô TÊN ĐĂNG NHẬP
            string matKhauCu = txtMatKhauCu.Text.Trim();
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string nhapLaiMatKhauMoi = txtNhapLaiMatKhauMoi.Text.Trim();

            if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matKhauCu)
                || string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(nhapLaiMatKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (matKhauMoi != nhapLaiMatKhauMoi)
            {
                MessageBox.Show("Mật khẩu mới không khớp!");
                return;
            }

            TaiKhoanBLL tkBLL = new TaiKhoanBLL();
            // Kiểm tra mật khẩu cũ đúng không
            if (!tkBLL.KiemTraDangNhap(tenDN, matKhauCu))
            {
                MessageBox.Show("Mật khẩu cũ không đúng hoặc tên đăng nhập không tồn tại!");
                return;
            }

            // Đổi mật khẩu
            bool ok = tkBLL.DoiMatKhau(tenDN, matKhauMoi);
            if (ok)
                MessageBox.Show("Đổi mật khẩu thành công!");
            else
                MessageBox.Show("Đổi mật khẩu thất bại!");
        }
    }
}
