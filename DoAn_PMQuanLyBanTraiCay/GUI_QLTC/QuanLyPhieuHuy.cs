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
    public partial class QuanLyPhieuHuy : Form
    {
        
        private SanPhamBLL sanphamBLL = new SanPhamBLL();

        private CT_PhieuHuyBLL ctphieuHuyBLL = new CT_PhieuHuyBLL();
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private PhieuHuyBLL phieuHuyBLL = new PhieuHuyBLL();
        public QuanLyPhieuHuy()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadPhieuHuyData();
            ConfigureDataGridView();
            BindEvents();
           
        }
        
        private void LoadComboBoxes()
        {
            cbNhanVien.DataSource = nhanVienBLL.GetAll();
            cbNhanVien.DisplayMember = "TenNV";
            cbNhanVien.ValueMember = "MaNV";
            cbMaSP.DataSource = sanphamBLL.GetAll();
            cbMaSP.DisplayMember = "TenSP";
            cbMaSP.ValueMember = "MaSP";
            cbNhanVien.SelectedIndex = -1; // Đặt giá trị mặc định là không chọn gì
            nudSoLuong.Value = 1; // Đặt giá trị mặc định là 1
            txtGhiChu.Text = ""; // Đặt giá trị mặc định là rỗng
            dtpNgayHuy.Value = DateTime.Now; // Đặt giá trị mặc định là ngày hiện tại
            txtMaPH.Text = ""; // Đặt giá trị mặc định là rỗng
        }

        private void LoadPhieuHuyData()
        {
            dgvPhieuHuy.DataSource = phieuHuyBLL.GetAll();
        }

        private void ConfigureDataGridView()
        {
            dgvPhieuHuy.AutoGenerateColumns = false;
            dgvPhieuHuy.Columns.Clear();
            
            dgvPhieuHuy.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaPH",
                Name = "MaPH",
                HeaderText = "Mã Phiếu Hủy"
            });

            dgvPhieuHuy.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaNV",
                Name = "MaNV",
                HeaderText = "Mã Nhân Viên"
            });

            dgvPhieuHuy.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayHuy",
                Name = "NgayHuy",
                HeaderText = "Ngày Hủy"
            });

            dgvPhieuHuy.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GhiChu",
                Name = "GhiChu",
                HeaderText = "Ghi Chú"
            });
        }

        private void BindEvents()
        {
            btnThem.Click += btnThem_Click;
            btnXoa.Click += btnXoa_Click;
            btnChiTiet.Click += btnChiTiet_Click;
            dgvPhieuHuy.CellClick += dgvPhieuHuy_CellClick;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbNhanVien.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Lỗi");
                return;
            }

            PhieuHuyDTO ph = new PhieuHuyDTO
            {
                MaNV = (int)cbNhanVien.SelectedValue,
                NgayHuy = dtpNgayHuy.Value,
                GhiChu = txtGhiChu.Text.Trim()
            };

            int maPH = phieuHuyBLL.InsertGetID(ph); // Insert và lấy ID mới
            if (maPH <= 0)
            {
                MessageBox.Show("Thêm phiếu hủy thất bại!");
                return;
            }

            CT_PhieuHuyDTO ct = new CT_PhieuHuyDTO
            {
                MaPH = maPH,
                MaSP = (int)cbMaSP.SelectedValue,
                SoLuong = (int)nudSoLuong.Value,
                LyDo = txtGhiChu.Text.Trim() // Nếu bạn muốn nhập lý do riêng thì nên dùng textbox riêng
            };

            bool success = ctphieuHuyBLL.Insert(ct);
            if (!success)
            {
                MessageBox.Show("Thêm chi tiết phiếu hủy thất bại!", "Lỗi");
                return;
            }

            MessageBox.Show("Thêm phiếu hủy và chi tiết thành công!");
            LoadPhieuHuyData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhieuHuy.SelectedRows.Count <= 0) return;

            int maPH = Convert.ToInt32(dgvPhieuHuy.SelectedRows[0].Cells["MaPH"].Value);
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (phieuHuyBLL.Delete(maPH))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadPhieuHuyData();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvPhieuHuy.SelectedRows.Count > 0)
            {
                int maPH = Convert.ToInt32(dgvPhieuHuy.SelectedRows[0].Cells["MaPH"].Value);
                PhieuHuyDTO ph = new PhieuHuyDTO
                {
                    MaPH = maPH,
                    MaNV = (int)cbNhanVien.SelectedValue,
                    NgayHuy = dtpNgayHuy.Value,
                    GhiChu = txtGhiChu.Text.Trim()
                };
                if (phieuHuyBLL.Update(ph))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadPhieuHuyData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu để sửa.");
            }
        }
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvPhieuHuy.SelectedRows.Count > 0)
            {
                int maPH = Convert.ToInt32(dgvPhieuHuy.SelectedRows[0].Cells["MaPH"].Value);
                frmChiTietPhieuHuy frm = new frmChiTietPhieuHuy(maPH);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu để xem chi tiết.");
            }
        }

        private void dgvPhieuHuy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhieuHuy.Rows[e.RowIndex];
                txtMaPH.Text = row.Cells["MaPH"].Value.ToString();
                cbNhanVien.SelectedValue = row.Cells["MaNV"].Value;
                dtpNgayHuy.Value = Convert.ToDateTime(row.Cells["NgayHuy"].Value);
                txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            }
        }
        
        private void QuanLyPhieuHuy_Load(object sender, EventArgs e)
        {
             
        }
        private void ResertForm()
        {
            txtMaPH.Text = "";
            cbNhanVien.SelectedIndex = -1;
            dtpNgayHuy.Value = DateTime.Now;
            txtGhiChu.Text = "";
            txtTimKiem.Text = "";

            LoadPhieuHuyData();
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            
            ResertForm();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                dgvPhieuHuy.DataSource = phieuHuyBLL.Search(keyword);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.");
            }
        }
    }
}
