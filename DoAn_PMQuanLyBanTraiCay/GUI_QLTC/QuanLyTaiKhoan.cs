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
    public partial class QuanLyTaiKhoan : Form
    {
        public QuanLyTaiKhoan()
        {
            InitializeComponent();
            
        }
        private TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();

        

        private void QuanLyTaiKhoanForm_Load(object sender, EventArgs e)
        {
            LoadTaiKhoanData();
            SetupComboBoxes();
        }

        private void SetupComboBoxes()
        {
            // Loại tài khoản
            cbLoaiTK.Items.Clear();
            cbLoaiTK.Items.Add("Admin");
            cbLoaiTK.Items.Add("NhanVien");
            cbLoaiTK.Items.Add("KhachHang");
            cbLoaiTK.SelectedIndex = 0;

            // Trạng thái
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.Add("HoatDong");
            cbTrangThai.Items.Add("Khoa");
            cbTrangThai.SelectedIndex = 0;
        }

        private void LoadTaiKhoanData()
        {
            try
            {
                        
                List<TaiKhoanDTO> danhSachTaiKhoan = taiKhoanBLL.GetAll();
                
                dgvTaiKhoan.DataSource = null;
                dgvTaiKhoan.DataSource = danhSachTaiKhoan;

                // Tùy chỉnh cột DataGridView
                dgvTaiKhoan.Columns["TenDN"].HeaderText = "Tên Đăng Nhập";
                dgvTaiKhoan.Columns["MK"].HeaderText = "Mật Khẩu";
                dgvTaiKhoan.Columns["LoaiTK"].HeaderText = "Loại Tài Khoản";
                dgvTaiKhoan.Columns["TrangThai"].HeaderText = "Trạng Thái";
                dgvTaiKhoan.Columns["Email"].HeaderText = "Email";
                dgvTaiKhoan.Columns["SDT"].HeaderText = "SĐT";
                dgvTaiKhoan.Columns["MaNV"].HeaderText = "Mã NV";
                dgvTaiKhoan.Columns["MaKH"].HeaderText = "Mã KH";

                dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    TenDN = txtTenDN.Text.Trim(),
                    MK = txtMK.Text.Trim(), // Nên hash mật khẩu ở đây trong thực tế
                    LoaiTK = cbLoaiTK.SelectedItem.ToString(),
                    TrangThai = cbTrangThai.SelectedItem.ToString(),
                    Email = txtEmail.Text.Trim(),
                    SDT = txtSDT.Text.Trim(),
                    MaNV = null,  // Chỉ gán nếu loại tài khoản là "NhanVien"
                    MaKH = null   // Chỉ gán nếu loại tài khoản là "KhachHang"
                };

                // Kiểm tra loại tài khoản để gán MaNV hoặc MaKH
                if (tk.LoaiTK == "NhanVien")
                {
                    tk.MaNV = Convert.ToInt32(txtMaNV.Text); // Phải nhập Mã NV nếu là nhân viên
                }
                else if (tk.LoaiTK == "KhachHang")
                {
                    tk.MaKH = Convert.ToInt32(txtMaKH.Text); // Phải nhập Mã KH nếu là khách hàng
                }

                if (taiKhoanBLL.TaoTaiKhoan(tk))
                {
                    MessageBox.Show("Thêm tài khoản thành công!");
                    LoadTaiKhoanData();
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    TenDN = txtTenDN.Text.Trim(),
                    MK = txtMK.Text.Trim(),
                    LoaiTK = cbLoaiTK.SelectedItem.ToString(),
                    TrangThai = cbTrangThai.SelectedItem.ToString(),
                    Email = txtEmail.Text.Trim(),
                    SDT = txtSDT.Text.Trim(),
                    MaNV = null,
                    MaKH = null
                };

                if (tk.LoaiTK == "NhanVien")
                {
                    tk.MaNV = Convert.ToInt32(txtMaNV.Text);
                }
                else if (tk.LoaiTK == "KhachHang")
                {
                    tk.MaKH = Convert.ToInt32(txtMaKH.Text);
                }

                if (taiKhoanBLL.SuaTaiKhoan(tk))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!");
                    LoadTaiKhoanData();
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDN = txtTenDN.Text.Trim();
                if (taiKhoanBLL.XoaTaiKhoan(tenDN))
                {
                    MessageBox.Show("Xóa tài khoản thành công!");
                    LoadTaiKhoanData();
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenDN.Clear();
            txtMK.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtMaNV.Clear();
            txtMaKH.Clear();
            cbLoaiTK.SelectedIndex = 0;
            cbTrangThai.SelectedIndex = 0;
            LoadTaiKhoanData();
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                txtTenDN.Text = row.Cells["TenDN"].Value.ToString();
                txtMK.Text = row.Cells["MK"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                cbLoaiTK.SelectedItem = row.Cells["LoaiTK"].Value.ToString();
                cbTrangThai.SelectedItem = row.Cells["TrangThai"].Value.ToString();
                txtMaNV.Text = row.Cells["MaNV"].Value?.ToString();
                txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
            }
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy từ khóa từ TextBox
                string keyword = txtTimKiem.Text.Trim();

                // Gọi phương thức tìm kiếm từ BLL
                List<TaiKhoanDTO> danhSachTaiKhoan = taiKhoanBLL.TimKiemTaiKhoan(keyword);

                // Hiển thị kết quả tìm kiếm
                dgvTaiKhoan.DataSource = null;
                dgvTaiKhoan.DataSource = danhSachTaiKhoan;

                if (danhSachTaiKhoan.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
