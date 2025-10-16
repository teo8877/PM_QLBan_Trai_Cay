using DTO_QLTC;
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
using DTO_QLTC.DTO_QLTC;

namespace GUI_QLTC
{
    public partial class frmBanHang : Form
    {
        private HoaDonBLL hoaDonBLL = new HoaDonBLL();
        private CT_HoaDonBLL ctHoaDonBLL = new CT_HoaDonBLL();
        private SanPhamBLL sanPhamBLL = new SanPhamBLL();
        private KhachHangBLL khachHangBLL = new KhachHangBLL();
        private NhanVienBLL nhanVienBLL = new NhanVienBLL();

        //private List<CT_HoaDonDTO> dsCTHD = new List<CT_HoaDonDTO>();
        private BindingList<CT_HoaDonDTO> dsCTHD = new BindingList<CT_HoaDonDTO>();
        public frmBanHang()
        {
            InitializeComponent();
            LoadComboBoxes();
            ResetForm();
            WireEvents();
            ConfigureDataGridView();
            dgvCTHD.DataSource = dsCTHD;
           

        }
        private void ConfigureDataGridView()
        {
             
            dgvCTHD.Columns.Clear(); // Xóa cột cũ
            dgvCTHD.AutoGenerateColumns = false;

            // Thêm các cột thủ công
            dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSP", HeaderText = "Mã sản phẩm" });
            dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenSP", HeaderText = "Tên sản phẩm" });
            dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "Số lượng" });
            dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", HeaderText = "Đơn giá" });
            dgvCTHD.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", HeaderText = "Thành tiền" });

            // Gán danh sách dữ liệu làm nguồn cho DataGridView
            dgvCTHD.DataSource = dsCTHD;
            // Không cho sửa dữ liệu trong các ô
            this.dgvCTHD.ReadOnly = true;

 
        }
        private void WireEvents()
        {
            cbSanPham.SelectedIndexChanged -= cbSanPham_SelectedIndexChanged;
            cbSanPham.SelectedIndexChanged += cbSanPham_SelectedIndexChanged;

            nudSoLuong.ValueChanged -= nudSoLuong_ValueChanged;
            nudSoLuong.ValueChanged += nudSoLuong_ValueChanged;

            btnThem.Click -= btnThem_Click;
            btnThem.Click += btnThem_Click;

            btnXoa.Click -= btnXoa_Click;
            btnXoa.Click += btnXoa_Click;

            txtTienKhachDua.TextChanged -= txtTienKhachDua_TextChanged;
            txtTienKhachDua.TextChanged += txtTienKhachDua_TextChanged;

            btnLapHoaDon.Click -= btnLapHoaDon_Click;
            btnLapHoaDon.Click += btnLapHoaDon_Click;

            btnLamMoi.Click -= btnLamMoi_Click;
            btnLamMoi.Click += btnLamMoi_Click;
        }

        private void LoadComboBoxes()
        {
            cbSanPham.DataSource = sanPhamBLL.GetAll();
            cbSanPham.DisplayMember = "TenSP";
            cbSanPham.ValueMember = "MaSP";

            cbKhachHang.DataSource = khachHangBLL.GetAll();
            cbKhachHang.DisplayMember = "HoTen";
            cbKhachHang.ValueMember = "MaKH";
            //List<KhachHangDTO> khachHangs = khachHangBLL.GetAll();
            //KhachHangDTO khachVangLai = new KhachHangDTO
            //{
            //    MaKH = 0,
            //    HoTen = "Khách vãng lai"
            //};
            //cbKhachHang.Insert(0, khachVangLai);
            cbNhanVien.DataSource = nhanVienBLL.GetAll();
            cbNhanVien.DisplayMember = "TenNV";
            cbNhanVien.ValueMember = "MaNV";

        }
        private void ResetForm()
        {
         
            dtpNgayLap.Value = DateTime.Now;
           
            nudSoLuong.Value = 1;
            txtDonGia.Text = "";
            txtThanhTien.Text = "";
            dsCTHD.Clear();
            dgvCTHD.DataSource = null;
            txtTongTien.Text = "0";
            txtTienKhachDua.Text = "";
            txtTienThoiLai.Text = "";
        }
        private void cbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedItem is SanPhamDTO sp)
            {
                txtDonGia.Text = sp.DonGia.ToString("N0");
               nudSoLuong.Value = 1;

                UpdateThanhTien();
            }
        }
        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
            UpdateThanhTien();
        }
        private void UpdateThanhTien()
        {
            if (cbSanPham.SelectedItem is SanPhamDTO sp)
            {
                int soLuong = (int)nudSoLuong.Value;
                decimal donGia = sp.DonGia;
                txtThanhTien.Text = (soLuong * donGia).ToString("N0");
            }
        }
        private void btnThanhTien_Click(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedItem is SanPhamDTO sp)
            {
                int soLuong = (int)nudSoLuong.Value;
                decimal donGia = sp.DonGia;
                txtThanhTien.Text = (soLuong * donGia).ToString("N0");
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
             
            try
            {
                 
                if (cbSanPham.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra nếu số lượng nhỏ hơn hoặc bằng 0
                if (nudSoLuong.Value <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin sản phẩm
                SanPhamDTO sp = (SanPhamDTO)cbSanPham.SelectedItem;
                int soLuong = (int)nudSoLuong.Value;
                decimal donGia = sp.DonGia;
                decimal thanhTien = soLuong * donGia;

                // Kiểm tra nếu sản phẩm đã tồn tại trong danh sách chi tiết hóa đơn
                var existingItem = dsCTHD.FirstOrDefault(ct => ct.MaSP == sp.MaSP);
                if (existingItem != null)
                {
                    // Nếu sản phẩm đã tồn tại, tăng số lượng và cập nhật thành tiền
                    //existingItem.SoLuong += soLuong;
                    //existingItem.ThanhTien += thanhTien;

                    existingItem.SoLuong = existingItem.SoLuong + soLuong;
                    existingItem.ThanhTien = existingItem.SoLuong * existingItem.DonGia;
                    var index = dsCTHD.IndexOf(existingItem);
                    if (index >= 0)
                    {
                        dgvCTHD.Refresh(); // Cách đơn giản để đảm bảo cập nhật UI
                    }

                }
                else
                {
                    // Nếu sản phẩm chưa tồn tại, thêm sản phẩm mới vào danh sách
                    dsCTHD.Add(new CT_HoaDonDTO
                    {
                        MaSP = sp.MaSP,
                        TenSP = sp.TenSP,
                        SoLuong = soLuong,
                        DonGia = donGia,
                        ThanhTien = thanhTien
                    });
                }

                // Cập nhật DataGridView với danh sách chi tiết hóa đơn
                dgvCTHD.DataSource = null; // Xóa binding cũ
                dgvCTHD.DataSource = dsCTHD; // Gán binding mới

                // Cập nhật tổng tiền
                UpdateTongTien();

                // Reset các trường nhập liệu
                nudSoLuong.Value = 1;
                txtThanhTien.Text = "0";

                // Thông báo thành công
                MessageBox.Show("Thêm sản phẩm vào giỏ hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có ngoại lệ
                MessageBox.Show($"Có lỗi xảy ra khi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
         
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCTHD.CurrentRow != null)
            {
                int idx = dgvCTHD.CurrentRow.Index;
                if (idx >= 0 && idx < dsCTHD.Count)
                {
                    dsCTHD.RemoveAt(idx);
                    dgvCTHD.DataSource = null;
                    dgvCTHD.DataSource = dsCTHD;
                    UpdateTongTien();
                }
            }
        }
        private void UpdateTongTien()
        {
            decimal sum = 0;
            foreach (var item in dsCTHD)
                sum += item.ThanhTien;
            txtTongTien.Text = sum.ToString("N0");
        }
        private void txtTienKhachDua_TextChanged(object sender, EventArgs e)
        {
            decimal tienKhachDua = 0, tongTien = 0;
            decimal.TryParse(txtTienKhachDua.Text, out tienKhachDua);
            decimal.TryParse(txtTongTien.Text, out tongTien);
            txtTienThoiLai.Text = (tienKhachDua - tongTien).ToString("N0");
        }
        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (dsCTHD.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào!");
                return;
            }

            try
            {
                HoaDonDTO hd = new HoaDonDTO()
                {
                    NgayLap = dtpNgayLap.Value,
                    MaNV = (int)cbNhanVien.SelectedValue,
                    MaKH = (int)cbKhachHang.SelectedValue,
                    TongTien = decimal.Parse(txtTongTien.Text),
                    
                };

                // Save invoice and get generated ID
                int maHD = hoaDonBLL.InsertAndGetID(hd);

                foreach (var ct in dsCTHD)
                {
                    ct.MaHD = maHD;
                    ctHoaDonBLL.Insert(ct);
                }

                MessageBox.Show("Lập hóa đơn thành công!");
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

         
    }
}
