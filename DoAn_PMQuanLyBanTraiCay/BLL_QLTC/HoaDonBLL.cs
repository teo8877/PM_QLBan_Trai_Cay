using DAL_QLTC;
using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLTC
{
    public class HoaDonBLL
    {
        private HoaDonDAL dal = new HoaDonDAL();

        public List<HoaDonDTO> GetAll()
        {
            return dal.GetAllHoaDon();
        }
        public int InsertAndGetID(HoaDonDTO hd)
        {
            return dal.InsertAndGetID(hd);
        }
        // Phương thức mới: Tìm kiếm hóa đơn theo từ khóa
        public List<HoaDonDTO> SearchHoaDon(string keyword)
        {
            return dal.SearchHoaDon(keyword);
        }

        // Phương thức mới: Xóa hóa đơn
        public bool DeleteHoaDon(int maHD)
        {
            return dal.DeleteHoaDon(maHD);
        }
        public bool UpdateHoaDon(HoaDonDTO hoaDon)
        {
            return dal.UpdateHoaDon(hoaDon);
        }
    }
}
