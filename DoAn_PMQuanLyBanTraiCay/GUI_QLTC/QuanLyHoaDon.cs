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
    public partial class QuanLyHoaDon : Form
    {
        private HoaDonBLL hoaDonBLL = new HoaDonBLL();
        public QuanLyHoaDon()
        {
            InitializeComponent();
        }

        
        private void frmQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDonData();
        }

        private void LoadHoaDonData()
        {
            // Lấy danh sách hóa đơn từ BLL và hiển thị lên DataGridView
            dgvHoaDon.DataSource = hoaDonBLL.GetAll();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Tìm kiếm hóa đơn theo từ khóa
            string keyword = txtTimKiem.Text.Trim();
            dgvHoaDon.DataSource = hoaDonBLL.SearchHoaDon(keyword);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có dòng nào được chọn
            if (dgvHoaDon.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa!", "Lỗi");
                return;
            }

            // Lấy mã hóa đơn từ dòng được chọn
            var selectedRow = dgvHoaDon.SelectedRows[0];
            if (selectedRow.Cells["MaHD"].Value == null || string.IsNullOrEmpty(selectedRow.Cells["MaHD"].Value.ToString()))
            {
                MessageBox.Show("Không thể lấy mã hóa đơn để xóa!", "Lỗi");
                return;
            }

            int maHD = Convert.ToInt32(selectedRow.Cells["MaHD"].Value);

            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Gọi phương thức xóa trong BLL
                bool isDeleted = hoaDonBLL.DeleteHoaDon(maHD);

                if (isDeleted)
                {
                    MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo");
                    LoadHoaDonData(); // Tải lại danh sách hóa đơn
                }
                else
                {
                    MessageBox.Show("Xóa hóa đơn thất bại!", "Lỗi");
                }
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            // Hiển thị chi tiết hóa đơn
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                int maHD = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["MaHD"].Value);
                frmChiTietHoaDon frmChiTiet = new frmChiTietHoaDon(maHD);
                frmChiTiet.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xem chi tiết!", "Lỗi");
            }
        }

        
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu thông tin cần thiết đã được nhập
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để sửa!", "Lỗi");
                return;
            }

            // Lấy dữ liệu từ các TextBox
            int maHD = Convert.ToInt32(txtMaHD.Text);
            DateTime ngayLap = dtpNgayLap.Value;
            decimal tongTien;

            // Kiểm tra giá trị Tổng Tiền có hợp lệ không
            if (!decimal.TryParse(txtTongTien.Text, out tongTien))
            {
                MessageBox.Show("Tổng tiền phải là một số hợp lệ!", "Lỗi");
                return;
            }

            // Tạo đối tượng HoaDonDTO để lưu thông tin
            HoaDonDTO hoaDon = new HoaDonDTO
            {
                MaHD = maHD,
                NgayLap = ngayLap,
                TongTien = tongTien
            };

            // Gọi phương thức cập nhật từ BLL
            bool isUpdated = hoaDonBLL.UpdateHoaDon(hoaDon);

            if (isUpdated)
            {
                MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo");
                LoadHoaDonData(); // Tải lại danh sách hóa đơn
            }
            else
            {
                MessageBox.Show("Cập nhật hóa đơn thất bại!", "Lỗi");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
          
            txtTimKiem.Clear();
            txtMaHD.Clear();
            txtMaKH.Clear();
            txtTongTien.Clear();
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu dòng được chọn hợp lệ
            {
                DataGridViewRow row = dgvHoaDon.Rows[e.RowIndex];

                // Đổ dữ liệu từ DataGridView vào các TextBox
                txtMaHD.Text = row.Cells["MaHD"].Value.ToString();
                txtMaHD.ReadOnly = true; // Không cho phép sửa Mã hóa đơn
                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtMaKH.ReadOnly = true; // Không cho phép sửa Mã khách hàng
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtMaNV.ReadOnly = true; // Không cho phép sửa Mã nhân viên
                dtpNgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
                txtTongTien.Text = row.Cells["TongTien"].Value.ToString();
            }
        }

       
         
         
    }
}
