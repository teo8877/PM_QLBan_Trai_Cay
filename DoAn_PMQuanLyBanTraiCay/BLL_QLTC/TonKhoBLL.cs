using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTC;
using DAL_QLTC;
namespace BLL_QLTC
{
    public  class TonKhoBLL
    {
        TonKhoDAL dal = new TonKhoDAL();
       
        public List<TonKhoDTO> GetBaoCaoTonKho()
        {
            return dal.GetBaoCaoTonKho( );
        }

    }
}
