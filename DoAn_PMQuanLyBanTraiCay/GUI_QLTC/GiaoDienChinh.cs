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
    public partial class GiaoDienChinh : Form
    {
        private string loaiTK;
        private bool isLoggedIn = false;

        public GiaoDienChinh(string loaiTK="")
        {
            InitializeComponent();
            this.loaiTK = loaiTK;
           
        }
        private void UpdateLoginUI()
        {
            btnDangNhap.Visible = !isLoggedIn;
            btnDangXuat.Visible = isLoggedIn;
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            QuanLyNhanVien frm = new QuanLyNhanVien(); // Form bạn muốn mở
            LoadFormVaoPanel(frm);
        }
        private void LoadFormVaoPanel(Form formCon)
        {
            frmHome.Controls.Clear();           // Xóa các controls cũ trong panel
            formCon.TopLevel = false;                 // Đặt form con không phải top-level
            formCon.FormBorderStyle = FormBorderStyle.None;  // Xóa viền
            formCon.Dock = DockStyle.Fill;            // Fill hết panel
            frmHome.Controls.Add(formCon);     // Thêm form vào panel
            formCon.Show();                           // Hiển thị form
        }
        

        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            QuanLyTaiKhoan frm = new QuanLyTaiKhoan();
            LoadFormVaoPanel(frm);
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            QuanLySanPham frm=new QuanLySanPham();
            LoadFormVaoPanel(frm);
        }

        private void btnQuanLyTonKho_Click(object sender, EventArgs e)
        {
            frmBaoCaoTonKho  frm = new frmBaoCaoTonKho();
            LoadFormVaoPanel(frm);
        }

      

        

         

        

        

       

        private void btnNhanVien_click(object sender, EventArgs e)
        {
            QuanLyNhanVien frm=new QuanLyNhanVien();
            LoadFormVaoPanel(frm);
        }

        private void btnTaiKhoan_click(object sender, EventArgs e)
        {
            QuanLyTaiKhoan frm = new QuanLyTaiKhoan();
            LoadFormVaoPanel(frm);
        }

        private void btnHoaDon_click(object sender, EventArgs e)
        {
            QuanLyHoaDon frm = new QuanLyHoaDon();
            LoadFormVaoPanel(frm);
        }

         

        

      

        private void btnSanPham_click(object sender, EventArgs e)
        {
            QuanLySanPham frm = new QuanLySanPham();
            LoadFormVaoPanel(frm);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {

            panelMenu.Visible = false;  // Ẩn menu chứa các nút như btnTrangChu

            GiaoDienChinh frm = new GiaoDienChinh(loaiTK);  // Đừng quên truyền lại loaiTK nếu cần
            LoadFormVaoPanel(frm);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loaiTK))
            {
                MessageBox.Show("Bạn chưa đăng nhập, không thể đăng xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Ẩn form hiện tại
                this.Hide();

                // Mở lại giao diện chính nhưng ở trạng thái chưa đăng nhập
                GiaoDienChinh frm = new GiaoDienChinh(); // Gửi loaiTK = "" mặc định
                frm.Show();

                // Hoặc để chắc chắn, bạn có thể sửa GiaoDienChinh.cs constructor như:
                // public GiaoDienChinh(string loaiTK = "", bool isLoggedIn = false)
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
               
                Application.Exit();
            }
        }

        

        
        

     

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DoiMatKhau frm = new DoiMatKhau();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKyNhanVien  frm = new frmDangKyNhanVien();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKyKhachHang frm = new frmDangKyKhachHang();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
        private void btnNCC_Click(object sender, EventArgs e)
        {
            QuanLyNhaCungCap frm = new QuanLyNhaCungCap();
            LoadFormVaoPanel(frm);
        }
        private void btnTaiKhoan_Click_1(object sender, EventArgs e)
        {
            QuanLyTaiKhoan frm = new QuanLyTaiKhoan();
            LoadFormVaoPanel(frm);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            QuanLyKhachHang frm = new QuanLyKhachHang();
            LoadFormVaoPanel(frm);
        }

        private void DangNhap_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            using (DangNhap frm = new DangNhap())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Lấy loại tài khoản từ form đăng nhập
                    this.loaiTK = frm.LoaiTaiKhoan;
                    this.isLoggedIn = true;

                    UpdateLoginUI();
                    PhanQuyenMenu();
                }
            }
            this.Show();
        }
    
        private void btnChuyenBH_Click(object sender, EventArgs e)
        {
            frmBanHang frm = new frmBanHang();
            LoadFormVaoPanel(frm);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            frmThongKeDoanhThu frm = new frmThongKeDoanhThu();
            LoadFormVaoPanel(frm);
        }


        private void PhanQuyenMenu()
        {
            // Ẩn tất cả nút trước
            foreach (Control c in panelMenu.Controls)
            {
                if (c is Button btn)
                    btn.Visible = false;
            }

            // Hiện theo quyền
            if (string.IsNullOrEmpty(loaiTK))
            {
                btnTrangChu.Visible = true;
            }
            else if (loaiTK == "NhanVien")
            {
                btnHoaDon.Visible = true;
            }
            else if (loaiTK == "Admin")
            {
                foreach (Control c in panelMenu.Controls)
                {
                    if (c is Button btn)
                        btn.Visible = true;
                }
            }
        }
        private void GiaoDienChinh_Load(object sender, EventArgs e)
        {
            PhanQuyenMenu();
            
            UpdateLoginUI();
        }

      

     

        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
            QuanLyPhieuNhap frm = new QuanLyPhieuNhap();
            LoadFormVaoPanel(frm);
        }
       
        private void btnPhieuHuy_Click(object sender, EventArgs e)
        {
            QuanLyPhieuHuy frm = new QuanLyPhieuHuy();
            LoadFormVaoPanel(frm);
        }

        private void btnNCC_Click_1(object sender, EventArgs e)
        {
            QuanLyNhaCungCap frm = new QuanLyNhaCungCap();
            LoadFormVaoPanel(frm);
        }

        private void btnBaoCaoTonKho_click(object sender, EventArgs e)
        {
            frmBaoCaoTonKho frm = new frmBaoCaoTonKho();
            LoadFormVaoPanel(frm);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMatKhau frm = new QuenMatKhau();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}
