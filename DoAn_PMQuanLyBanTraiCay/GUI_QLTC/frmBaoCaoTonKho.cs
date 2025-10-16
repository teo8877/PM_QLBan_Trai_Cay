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
    public partial class frmBaoCaoTonKho : Form
    {
        
        private TonKhoBLL tonKhoBLL = new TonKhoBLL();

        public frmBaoCaoTonKho()
        {
            InitializeComponent();
            LoadTonKho();
        }

        private void LoadTonKho()
        {
            List<TonKhoDTO> data = tonKhoBLL.GetBaoCaoTonKho();
            dgvTonKho.DataSource = data;
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadTonKho();
        }

         
    }
}
