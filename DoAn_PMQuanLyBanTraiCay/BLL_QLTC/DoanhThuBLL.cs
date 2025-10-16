using DAL_QLTC;
using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLTC
{
    public class DoanhThuBLL
    {
        private DoanhThuDAL dal = new DoanhThuDAL();

        public List<DoanhThuDTO> LayDoanhThuTheoNgay()
        {
            return dal.GetDoanhThuTheoNgay();
        }
    }

}
