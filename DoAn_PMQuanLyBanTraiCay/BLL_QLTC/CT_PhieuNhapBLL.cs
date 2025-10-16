using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTC;

using DAL_QLTC;
using DTO_QLTC.DTO_QLTC;
namespace BLL_QLTC
{
    public class CT_PhieuNhapBLL
    {
        private CT_PhieuNhapDAL dal = new CT_PhieuNhapDAL();

        public List<CT_PhieuNhapDTO> GetAllByMaPN(int maPN)
        {
            return dal.GetAllByMaPN(maPN);
        }
        public bool Insert(CT_PhieuNhapDTO ct)
        {
            return dal.Insert(ct); // Gọi phương thức DAL
        }
         
        public List<CT_PhieuNhapDTO>GetChiTietPhieuNhap(int maPN)
        {
            return dal.GetAllByMaPN(maPN);
        }
        // Thêm chi tiết phiếu nhập
       
    }
}