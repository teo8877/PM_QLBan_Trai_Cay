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
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
        }
        private void btnLayLaiMK_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDN.Text.Trim();
            string email = txtEmail.Text.Trim();

            var mk = TaiKhoanBLL.LayMatKhau(tenDN, email);
            if (mk != null)
            {
                lblKetQua.ForeColor = Color.Green;
                lblKetQua.Text = "Mật khẩu của bạn là: " + mk;
            }
            else
            {
                lblKetQua.ForeColor = Color.Red;
                lblKetQua.Text = "Tên đăng nhập hoặc email không đúng!";
            }
        }
        
    }
}
