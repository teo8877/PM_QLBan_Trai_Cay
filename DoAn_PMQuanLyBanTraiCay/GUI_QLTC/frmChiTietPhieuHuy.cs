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
    public partial class frmChiTietPhieuHuy : Form
    {
        private int maPH;
        private CT_PhieuHuyBLL ctphieuHuyBLL = new CT_PhieuHuyBLL();
        
        public frmChiTietPhieuHuy(int maPH)
        {
            InitializeComponent();
            this.maPH = maPH;
        }
        private void frmChiTietPhieuHuy_Load(object sender, EventArgs e)
        {
            
            LoadCTPhieuHuyData();
           
        }
        private void LoadCTPhieuHuyData()
        {
            //dgvCTPhieuHuy.DataSource = ctphieuHuyBLL.GetAllByMaPH(maPH);
            var data = ctphieuHuyBLL.GetAllByMaPH(maPH);
            if (data == null || data.Count == 0)
            {
                MessageBox.Show("Không có chi tiết nào cho phiếu hủy mã: " + maPH);
            }
            dgvCTPhieuHuy.DataSource = data;
        }
    }
}
