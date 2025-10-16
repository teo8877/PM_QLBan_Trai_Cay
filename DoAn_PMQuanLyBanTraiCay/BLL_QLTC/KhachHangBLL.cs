using DAL_QLTC;
using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLTC
{
    public class KhachHangBLL
    {
        private KhachHangDAL dal = new KhachHangDAL();

        public List<KhachHangDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool ThemKhachHang(KhachHangDTO kh)
        {
            return dal.ThemKhachHang(kh);
        }
        public bool AddKhachHang(KhachHangDTO khachHang)
        {
            return dal.AddKhachHang(khachHang);
        }

        public bool UpdateKhachHang(KhachHangDTO khachHang)
        {
            return dal.UpdateKhachHang(khachHang);
        }

        public bool DeleteKhachHang(int maKH)
        {
            return dal.DeleteKhachHang(maKH);
        }

        public List<KhachHangDTO> SearchKhachHang(string keyword)
        {
            return dal.SearchKhachHang(keyword);
        }
    }
}
