using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTC;
namespace DTO_QLTC
{
    namespace DTO_QLTC
    {
        public class CT_HoaDonDTO
        {   
            public string TenSP { get; set; } // Tên sản phẩm   
            public int MaHD { get; set; } // Mã hóa đơn
            public int MaSP { get; set; } // Mã sản phẩm
            public int SoLuong { get; set; } // Số lượng sản phẩm
            public decimal DonGia { get; set; }  // Đơn giá
            public decimal ThanhTien { get; set; } // Thành tiền
        }
    }
}
