using DAL_QLTC;
using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLTC
{
    public class SanPhamBLL
    {
        private SanPhamDAL dal = new SanPhamDAL();

        public List<SanPhamDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool Insert(SanPhamDTO sp)
        {
            return dal.Insert(sp);
        }
        public bool Update(SanPhamDTO sp)
        {
            return dal.Update(sp);
        }
        public bool Delete(int maSP)
        {
            return dal.Delete(maSP);
        }
        public List<SanPhamDTO> Search(string keyword)
        {
            return dal.Search(keyword);
        }
        
    }
}
