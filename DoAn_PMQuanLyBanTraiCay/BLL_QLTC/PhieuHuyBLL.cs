 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLTC;
using DTO_QLTC;


namespace BLL_QLTC
{
    public class PhieuHuyBLL
    {
        private PhieuHuyDAL dal = new PhieuHuyDAL();

        public List<PhieuHuyDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool Insert(PhieuHuyDTO ph)
        {
            return dal.Insert(ph);
        }
        public bool Update(PhieuHuyDTO ph)
        {
            return dal.Update(ph);
        }
        public bool Delete(int maPH)
        {
            return dal.Delete(maPH);
        }
        public List<PhieuHuyDTO> Search(string keyword)
        {
            return dal.Search(keyword);
        }
        public int InsertGetID(PhieuHuyDTO ph)
        {
            return dal.InsertGetID(ph);
        }

    }
}
