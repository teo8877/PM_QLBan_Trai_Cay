using DAL_QLTC;
using DTO_QLTC;
using DTO_QLTC.DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLTC
{
    public class CT_HoaDonBLL
    {
        private CT_HoaDonDAL dal = new CT_HoaDonDAL();

        public List<CT_HoaDonDTO> GetAllByMaHD(int maHD)
        {
            return dal.GetAllByMaHD(maHD);
        }

        public void Insert(CT_HoaDonDTO ct)
        {
            dal.Insert(ct); // Gọi phương thức DAL
        }
        // Lấy chi tiết hóa đơn theo mã hóa đơn
        public List<CT_HoaDonDTO> GetChiTietHoaDon(int maHD)
        {
            return dal.GetAllByMaHD(maHD);
        }
    }
}
