using DTO_QLTC;
using DAL_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL_QLTC
{
    public class PhieuNhapBLL
    {
        private PhieuNhapDAL dal = new PhieuNhapDAL();

        public List<PhieuNhapDTO> GetAll()
        {
            return dal.GetAll();
        }
        public bool Insert(PhieuNhapDTO pn)
        {
            return dal.Insert(pn);
        }
        public bool Update(PhieuNhapDTO pn)
        {
            return dal.Update(pn);
        }
        public bool Delete(int maPN)
        {
            return dal.Delete(maPN);
        }
       
        public List<PhieuNhapDTO> Search(string keyword)
        {
            return dal.Search(keyword);
        }
        public int InsertGetID(PhieuNhapDTO pn)
        {
            return dal.InsertGetID(pn);
        }

        

        

       
 

    }
}
