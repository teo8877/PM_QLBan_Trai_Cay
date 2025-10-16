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
    public partial class QuanLySanPham : Form
    {
        
        private SanPhamBLL sanPhamBLL = new SanPhamBLL();

        public QuanLySanPham()
        {
            InitializeComponent();
        }

        private void QuanLySanPham_Load(object sender, EventArgs e)
        {
            LoadSanPhamData();
        }

        private void LoadSanPhamData()
        {
            List<SanPhamDTO> list = sanPhamBLL.GetAll();
            dgvSanPham.DataSource = null;
            dgvSanPham.DataSource = list;

            // Tùy chỉnh cột
            dgvSanPham.Columns["MaSP"].HeaderText = "Mã SP";
            dgvSanPham.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
            dgvSanPham.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvSanPham.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            dgvSanPham.Columns["GhiChu"].HeaderText = "Ghi Chú";
            dgvSanPham.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SanPhamDTO sp = new SanPhamDTO
                {
                    TenSP = txtTenSP.Text.Trim(),
                    DonGia = decimal.Parse(txtDonGia.Text),
                    DonViTinh = txtDonViTinh.Text.Trim(),
                    GhiChu = txtGhiChu.Text.Trim()
                };
                if (sanPhamBLL.Insert(sp))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadSanPhamData();
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại!");
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
                SanPhamDTO sp = new SanPhamDTO
                {
                    MaSP = int.Parse(txtMaSP.Text),
                    TenSP = txtTenSP.Text.Trim(),
                    DonGia = decimal.Parse(txtDonGia.Text),
                    DonViTinh = txtDonViTinh.Text.Trim(),
                    GhiChu = txtGhiChu.Text.Trim()
                };
                if (sanPhamBLL.Update(sp))
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!");
                    LoadSanPhamData();
                }
                else
                {
                    MessageBox.Show("Cập nhật sản phẩm thất bại!");
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
                int maSP = int.Parse(txtMaSP.Text);
                if (sanPhamBLL.Delete(maSP))
                {
                    MessageBox.Show("Xóa sản phẩm thành công!");
                    LoadSanPhamData();
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm thất bại!");
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
            dgvSanPham.DataSource = sanPhamBLL.Search(keyword);
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
                txtTenSP.Text = row.Cells["TenSP"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                txtDonViTinh.Text = row.Cells["DonViTinh"].Value.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtDonGia.Clear();
            txtDonViTinh.Clear();
            txtGhiChu.Clear();
            txtTimKiem.Clear();
            LoadSanPhamData();
        }

        
    }
}
