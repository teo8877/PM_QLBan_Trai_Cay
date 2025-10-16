using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO_QLTC;
using BLL_QLTC;
namespace GUI_QLTC
{
    public partial class frmChiTietPhieuNhap : Form
    {
        private int maPN; // Biến để lưu mã phiếu nhập
        private CT_PhieuNhapBLL chiTietPhieuNhapBLL = new CT_PhieuNhapBLL();
        public frmChiTietPhieuNhap(int maPN)
        {
            InitializeComponent();
            this.maPN = maPN; // Gán mã phiếu nhập cho biến toàn cục
        }

       

        private void frmChiTietPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadChiTietPhieuNhap();
        }
        private void LoadChiTietPhieuNhap()
        {
            // Lấy chi tiết phiếu nhập từ BLL và hiển thị lên DataGridView
            dgvChiTietPhieuNhap.DataSource = chiTietPhieuNhapBLL.GetChiTietPhieuNhap(maPN);
        }


    }
}
