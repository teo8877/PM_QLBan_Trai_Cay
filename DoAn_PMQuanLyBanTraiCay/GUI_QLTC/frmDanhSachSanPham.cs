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
    public partial class frmDanhSachSanPham : Form
    {
       
        private SanPhamBLL sanPhamBLL = new SanPhamBLL();
       
        public frmDanhSachSanPham()
        {
            InitializeComponent();
            
        }
        private void frmChiTietSanPham_Load(object sender, EventArgs e)
        {
            LoadSanPham();
        }
        private void LoadSanPham()
        {
            dgvSanPham.DataSource = sanPhamBLL.GetAll();
        }
    }
}
