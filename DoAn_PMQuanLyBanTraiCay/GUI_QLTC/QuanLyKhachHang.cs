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
    public partial class QuanLyKhachHang : Form
    {

        private KhachHangBLL khachHangBLL = new KhachHangBLL();

        public QuanLyKhachHang()
        {
            InitializeComponent();
            LoadKhachHangData();
        }

        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu khách hàng lên DataGridView
            LoadKhachHangData();

            // Thêm các giá trị 'Thuong' và 'VIP' vào ComboBox
            cbLoaiKhachHang.Items.Clear();
            cbLoaiKhachHang.Items.Add("Thuong"); // Giá trị mặc định
            cbLoaiKhachHang.Items.Add("VIP");

            // Đặt giá trị mặc định cho ComboBox
            cbLoaiKhachHang.SelectedIndex = 0; // Chọn 'Thuong' làm giá trị mặc định
        }

        private void LoadKhachHangData()
        {
            // Lấy danh sách khách hàng từ BLL và hiển thị lên DataGridView
            dgvKhachHang.DataSource = khachHangBLL.GetAll();

            // Định dạng DataGridView
            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvKhachHang.Columns["SDT"].HeaderText = "Số ĐT";
            dgvKhachHang.Columns["Email"].HeaderText = "Email";
            dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvKhachHang.Columns["LoaiKhachHang"].HeaderText = "Loại KH";
            dgvKhachHang.Columns["NgayTao"].HeaderText = "Ngày Tạo";

           // dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Tìm kiếm khách hàng theo từ khóa
            string keyword = txtTimKiem.Text.Trim();
            dgvKhachHang.DataSource = khachHangBLL.SearchKhachHang(keyword);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Thêm khách hàng mới
            try
            {
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    HoTen = txtHoTen.Text.Trim(),
                    SDT = txtSDT.Text.Trim(),
                    Email = txtSDT.Text.Trim(),
                    DiaChi = txtEmail.Text.Trim(),
                    LoaiKhachHang = cbLoaiKhachHang.SelectedItem.ToString()
                };

                if (khachHangBLL.AddKhachHang(khachHang))
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                    LoadKhachHangData(); // Tải lại danh sách khách hàng
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng thất bại!", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            // Kiểm tra nếu không có dòng nào được chọn
            if (dgvKhachHang.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa!", "Lỗi");
                return;
            }

            // Lấy thông tin từ dòng được chọn
            var selectedRow = dgvKhachHang.SelectedRows[0];
            if (selectedRow.Cells["MaKH"].Value == null || string.IsNullOrEmpty(selectedRow.Cells["MaKH"].Value.ToString()))
            {
                MessageBox.Show("Không thể lấy mã khách hàng để sửa!", "Lỗi");
                return;
            }

            int maKH = Convert.ToInt32(selectedRow.Cells["MaKH"].Value);

            // Lấy dữ liệu khách hàng từ DataGridView
            txtHoTen.Text = selectedRow.Cells["HoTen"].Value.ToString();
            txtSDT.Text = selectedRow.Cells["SDT"].Value.ToString();
            txtEmail.Text = selectedRow.Cells["Email"].Value?.ToString();
            txtDiaChi.Text = selectedRow.Cells["DiaChi"].Value?.ToString();

            // Đặt giá trị cho ComboBox 'LoaiKhachHang'
            string loaiKhachHang = selectedRow.Cells["LoaiKhachHang"].Value?.ToString();
            if (!string.IsNullOrEmpty(loaiKhachHang) && (loaiKhachHang == "Thuong" || loaiKhachHang == "VIP"))
            {
                cbLoaiKhachHang.SelectedItem = loaiKhachHang; // Gán giá trị hợp lệ
            }
            else
            {
                cbLoaiKhachHang.SelectedIndex = 0; // Gán mặc định là 'Thuong' nếu giá trị không hợp lệ
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có dòng nào được chọn
            if (dgvKhachHang.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa!", "Lỗi");
                return;
            }

            // Lấy mã khách hàng từ dòng được chọn
            var selectedRow = dgvKhachHang.SelectedRows[0];
            if (selectedRow.Cells["MaKH"].Value == null || string.IsNullOrEmpty(selectedRow.Cells["MaKH"].Value.ToString()))
            {
                MessageBox.Show("Không thể lấy mã khách hàng để xóa!", "Lỗi");
                return;
            }

            int maKH = Convert.ToInt32(selectedRow.Cells["MaKH"].Value);

            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (khachHangBLL.DeleteKhachHang(maKH))
                    {
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo");
                        LoadKhachHangData(); // Tải lại danh sách khách hàng
                    }
                    else
                    {
                        MessageBox.Show("Xóa khách hàng thất bại!", "Lỗi");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
                }
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvKhachHang.DataSource = null;

            // Bước 1: Tải lại dữ liệu từ cơ sở dữ liệu và hiển thị trên DataGridView
            LoadKhachHangData();

            // Bước 2: Đặt lại các trường nhập liệu về trạng thái mặc định
            txtHoTen.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            cbLoaiKhachHang.SelectedIndex = 0; // Đặt ComboBox về giá trị mặc định ('Thuong')
            txtTimKiem.Clear();
        }

    }
}
