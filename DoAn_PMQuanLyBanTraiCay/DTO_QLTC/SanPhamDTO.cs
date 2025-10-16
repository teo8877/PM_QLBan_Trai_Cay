using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLTC
{
    public class SanPhamDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGia { get; set; }
        public string DonViTinh { get; set; }
       
        public string GhiChu { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
