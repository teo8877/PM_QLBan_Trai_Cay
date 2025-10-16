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
    public partial class QuanLyNhanVien : Form
    {
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();

         

        private void QuanLyNhanVienForm_Load(object sender, EventArgs e)
        {
            LoadNhanVienData();
            SetupComboBox();
        }

        private void SetupComboBox()
        {
            cbGioiTinh.Items.Clear();
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nu");
            cbGioiTinh.Items.Add("Khac");
            cbGioiTinh.SelectedIndex = 0; // Mặc định chọn "Nam"
        }

        private void LoadNhanVienData()
        {
            dgvNhanVien.DataSource = nhanVienBLL.GetAll();
            
            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            dgvNhanVien.Columns["TenNV"].HeaderText = "Tên NV";
            dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvNhanVien.Columns["SDT"].HeaderText = "Số ĐT";
            dgvNhanVien.Columns["Email"].HeaderText = "Email";
            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvNhanVien.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo đối tượng NhanVienDTO từ thông tin trong form
                NhanVienDTO nv = new NhanVienDTO
                {
                    TenNV = txtTenNV.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cbGioiTinh.SelectedItem.ToString(),
                    SDT = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim()
                };

                // Gọi phương thức thêm nhân viên từ BLL
                int maNV = nhanVienBLL.ThemNhanVien(nv);
                if (maNV > 0)
                {
                    MessageBox.Show($"Thêm nhân viên thành công! Mã nhân viên: {maNV}");
                    LoadNhanVienData(); // Cập nhật lại danh sách nhân viên
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa!", "Lỗi");
                return;
            }

            try
            {
                NhanVienDTO nv = new NhanVienDTO
                {
                    MaNV = Convert.ToInt32(txtMaNV.Text),
                    TenNV = txtTenNV.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = cbGioiTinh.SelectedItem.ToString(),
                    SDT = txtSDT.Text,
                    Email = txtEmail.Text,
                    DiaChi = txtDiaChi.Text
                };

                bool isUpdated = nhanVienBLL.SuaNhanVien(nv);
                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                    LoadNhanVienData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!", "Lỗi");
                return;
            }

            try
            {
                int maNV = Convert.ToInt32(txtMaNV.Text);
                bool isDeleted = nhanVienBLL.XoaNhanVien(maNV);
                if (isDeleted)
                {
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadNhanVienData();
                }
                else
                {
                    MessageBox.Show("Xóa nhân viên thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            dgvNhanVien.DataSource = nhanVienBLL.TimKiemNhanVien(keyword);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            cbGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Now;
            txtTimKiem.Clear();
            LoadNhanVienData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtTenNV.Text = row.Cells["TenNV"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                cbGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvNhanVien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0) // Kiểm tra nếu dòng được chọn hợp lệ
            //{
            //    DataGridViewRow row = dgvHoaDon.Rows[e.RowIndex];

            //    // Đổ dữ liệu từ DataGridView vào các TextBox
            //    txtMaHD.Text = row.Cells["MaHD"].Value.ToString();
            //    txtMaHD.ReadOnly = true; // Không cho phép sửa Mã hóa đơn
            //    txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
            //    txtMaKH.ReadOnly = true; // Không cho phép sửa Mã khách hàng
            //    txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
            //    txtMaNV.ReadOnly = true; // Không cho phép sửa Mã nhân viên
            //    dtpNgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
            //    txtTongTien.Text = row.Cells["TongTien"].Value.ToString();
            //}
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                // Đổ dữ liệu từ DataGridView vào các TextBox
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtTenNV.Text = row.Cells["TenNV"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                cbGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            }
        }
    }
}
