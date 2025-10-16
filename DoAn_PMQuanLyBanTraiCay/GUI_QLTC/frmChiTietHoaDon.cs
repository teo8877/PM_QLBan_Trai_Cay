using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QLTC;  
using DTO_QLTC;
namespace GUI_QLTC
{
    public partial class frmChiTietHoaDon : Form
    {
       

        
        private int maHD; // Biến để lưu mã hóa đơn
        private CT_HoaDonBLL chiTietHoaDonBLL = new CT_HoaDonBLL();  

        // Constructor nhận mã hóa đơn
        public frmChiTietHoaDon(int maHD)
        {
            InitializeComponent();
            this.maHD = maHD; // Gán mã hóa đơn cho biến toàn cục
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadChiTietHoaDon();
        }

        private void LoadChiTietHoaDon()
        {
            // Lấy chi tiết hóa đơn từ BLL và hiển thị lên DataGridView
            dgvChiTietHoaDon.DataSource = chiTietHoaDonBLL.GetChiTietHoaDon(maHD);
            dgvChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }


    }
}
