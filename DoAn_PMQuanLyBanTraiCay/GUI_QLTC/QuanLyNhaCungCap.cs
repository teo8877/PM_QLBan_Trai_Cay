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
    public partial class QuanLyNhaCungCap : Form
    {
        private NhaCungCapBLL bll = new NhaCungCapBLL();

        public QuanLyNhaCungCap()
        {
            InitializeComponent();
            dgvNCC.ReadOnly = true; // Không cho chỉnh sửa trực tiếp trong DataGridView
            LoadData();
        }

        private void LoadData()
        {
            dgvNCC.DataSource = bll.LayDanhSach();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var ncc = new NhaCungCapDTO
            {
                TenNCC = txtTen.Text,
                SDT = txtSDT.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text,
                GhiChu = txtGhiChu.Text
            };
            if (bll.Them(ncc))
            {
                MessageBox.Show("Thêm thành công");
                LoadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNCC.CurrentRow != null)
            {
                int ma = (int)dgvNCC.CurrentRow.Cells["MaNCC"].Value;
                if (bll.Xoa(ma))
                {
                    MessageBox.Show("Xóa thành công");
                    LoadData();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNCC.CurrentRow != null)
            {
                var ncc = new NhaCungCapDTO
                {
                    MaNCC = (int)dgvNCC.CurrentRow.Cells["MaNCC"].Value,
                    TenNCC = txtTen.Text,
                    SDT = txtSDT.Text,
                    Email = txtEmail.Text,
                    DiaChi = txtDiaChi.Text,
                    GhiChu = txtGhiChu.Text
                };
                if (bll.CapNhat(ncc))
                {
                    MessageBox.Show("Cập nhật thành công");
                    LoadData();
                }
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            dgvNCC.DataSource = NhaCungCapBLL.TimKiem(tuKhoa);
        }

        private void dgvNCC_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNCC.CurrentRow != null)
            {
                txtTen.Text = dgvNCC.CurrentRow.Cells["TenNCC"].Value.ToString();
                txtSDT.Text = dgvNCC.CurrentRow.Cells["SDT"].Value.ToString();
                txtEmail.Text = dgvNCC.CurrentRow.Cells["Email"].Value.ToString();
                txtDiaChi.Text = dgvNCC.CurrentRow.Cells["DiaChi"].Value.ToString();
                txtGhiChu.Text = dgvNCC.CurrentRow.Cells["GhiChu"].Value.ToString();
            }
        }
    }

}
