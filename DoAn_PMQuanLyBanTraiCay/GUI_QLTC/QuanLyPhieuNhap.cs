using BLL_QLTC;
using DTO_QLTC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLTC
{
    public partial class QuanLyPhieuNhap : Form
    {

        private CT_PhieuNhapBLL ctphieuNhapBLL = new CT_PhieuNhapBLL();
        private PhieuNhapBLL phieuNhapBLL = new PhieuNhapBLL();
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();
        private SanPhamBLL sanPhamBLL = new SanPhamBLL();
        public QuanLyPhieuNhap()
        {
            InitializeComponent();
            WrieEvent();
            ResertForm();
            ConfigureDataGridView();
        }
        private void ConfigureDataGridView()
        {
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPN",
                DataPropertyName = "MaPN",
                HeaderText = "Mã Phiếu Nhập"
            });

            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNV",
                DataPropertyName = "MaNV",
                HeaderText = "Mã Nhân Viên"
            });

            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayNhap",
                DataPropertyName = "NgayNhap",
                HeaderText = "Ngày Nhập"
            });

            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TongTien",
                DataPropertyName = "TongTien",
                HeaderText = "Tổng Tiền"
            });

        }

        private void WrieEvent()
        {
            dgvPhieuNhap.CellClick += dgvPhieuNhap_CellClick;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTimKiem.Click += btnTimKiem_Click;
            btnChiTiet.Click += btnChiTiet_Click;
            btnSanPham.Click += btnSanPham_Click;
            
            nudSoLuong.ValueChanged += nudSoLuong_ValueChanged;
            LoadComBoBoxes();
        }
        private void QuanLyPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadPhieuNhapData();
        }

        private void LoadPhieuNhapData()
        {
            dgvPhieuNhap.DataSource = phieuNhapBLL.GetAll();
        }
        private void UpdateTongTien()
        {
            if (cbSanPham.SelectedItem is SanPhamDTO sp)
            {
                decimal donGia = sp.DonGia;
                int soLuong = (int)nudSoLuong.Value;
                decimal tongTien = donGia * soLuong;
                txtTongTien.Text = tongTien.ToString("N0");
            }
        }
       private void LoadComBoBoxes()
        {
            cbSanPham.DataSource = sanPhamBLL.GetAll();
            cbSanPham.DisplayMember = "TenSP";
            cbSanPham.ValueMember = "MaSP";
            cbSanPham.SelectedIndexChanged += cbSanPham_SelectedIndexChanged;

            cbNhanVien.DataSource = nhanVienBLL.GetAll();
            cbNhanVien.DisplayMember = "TenNV";
            cbNhanVien.ValueMember = "MaNV";
            LoadSanPhamData();//Lấy tất cả lại vào dgv
        }
        private void LoadSanPhamData()
        {
            cbSanPham.DataSource = sanPhamBLL.GetAll();
             
        }
        private void ResertForm()
        {
            
            txtTongTien.Clear();
            dtpNgayNhap.Value = DateTime.Now;
            cbSanPham.SelectedIndex = -1;
            cbNhanVien.SelectedIndex = -1;
            nudSoLuong.Value = 1;
        }
        private void cbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedItem is SanPhamDTO sp)
            {
                txtDonGia.Text = sp.DonGia.ToString("N0");
                nudSoLuong.Value = 1;
                UpdateTongTien();
            }
        }
        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
            UpdateTongTien();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbSanPham.SelectedItem == null || cbNhanVien.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm và nhân viên!", "Cảnh báo");
                    return;
                }

                if (nudSoLuong.Value <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!", "Cảnh báo");
                    return;
                }

                decimal donGia = decimal.Parse(txtDonGia.Text);
                int soLuong = (int)nudSoLuong.Value;
                decimal thanhTien = donGia * soLuong;

                PhieuNhapDTO pn = new PhieuNhapDTO
                {
                    NgayNhap = dtpNgayNhap.Value,
                    MaNV = (int)cbNhanVien.SelectedValue,
                    TongTien = thanhTien
                };

                int maPN = phieuNhapBLL.InsertGetID(pn); // Phải có hàm trả về ID sau khi insert
                if (maPN > 0)
                {
                    CT_PhieuNhapDTO ctpn = new CT_PhieuNhapDTO
                    {
                        MaPN = maPN,
                        MaSP = (int)cbSanPham.SelectedValue,
                        SoLuong = soLuong,
                        DonGia = donGia,
                        ThanhTien = thanhTien
                    };

                    bool ctSuccess = ctphieuNhapBLL.Insert(ctpn); // Gọi BLL để thêm chi tiết

                    if (ctSuccess)
                    {
                        MessageBox.Show("Thêm phiếu nhập và chi tiết thành công!", "Thông báo");
                        LoadPhieuNhapData();
                        ResertForm();
                    }
                    else
                    {
                        MessageBox.Show("Thêm chi tiết phiếu nhập thất bại!", "Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Thêm phiếu nhập thất bại!", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPN.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập để sửa!", "Lỗi");
                return;
            }

            if (!decimal.TryParse(txtTongTien.Text, out decimal tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ!", "Lỗi");
                return;
            }

            PhieuNhapDTO pn = new PhieuNhapDTO
            {
                
                NgayNhap = dtpNgayNhap.Value,
              
                TongTien = tongTien
            };
            CT_PhieuNhapDTO ctpn=new CT_PhieuNhapDTO()
            {
                SoLuong = (int)nudSoLuong.Value,
                DonGia = decimal.Parse(txtDonGia.Text),
                ThanhTien = decimal.Parse(txtTongTien.Text),
            };
            bool isSuccess = phieuNhapBLL.Update(pn);

            if (isSuccess)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadPhieuNhapData();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập để xóa!");
                return;
            }

            int maPN = Convert.ToInt32(dgvPhieuNhap.SelectedRows[0].Cells["MaPN"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (phieuNhapBLL.Delete(maPN))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadPhieuNhapData();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            dgvPhieuNhap.DataSource = phieuNhapBLL.Search(keyword);
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            
            if (dgvPhieuNhap.SelectedRows.Count > 0)
            {
                int maPN = Convert.ToInt32(dgvPhieuNhap.SelectedRows[0].Cells["MaPN"].Value);
                frmChiTietPhieuNhap frmChiTiet = new frmChiTietPhieuNhap(maPN);

                frmChiTiet.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xem chi tiết!", "Lỗi");

            }
            }
        private void btnSanPham_Click(object sender, EventArgs e)
        {
            
             
            

            frmDanhSachSanPham frm = new frmDanhSachSanPham();
            frm.ShowDialog();



        }
        
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResertForm();
        }

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhieuNhap.Rows[e.RowIndex];
                txtMaPN.Text = row.Cells["MaPN"].Value.ToString();
                cbNhanVien.SelectedValue = Convert.ToInt32(row.Cells["MaNV"].Value);
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                txtTongTien.Text = row.Cells["TongTien"].Value.ToString();
            }
        }

     

         
    }
}
