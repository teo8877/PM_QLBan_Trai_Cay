using DAL_QLTC;
using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLTC
{
    public class NhaCungCapBLL
    {
        private NhaCungCapDAL dal = new NhaCungCapDAL();

        public List<NhaCungCapDTO> LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        public bool Them(NhaCungCapDTO ncc)
        {
            return dal.Them(ncc);
        }

        public bool Xoa(int maNCC)
        {
            return dal.Xoa(maNCC);
        }

        public bool CapNhat(NhaCungCapDTO ncc)
        {
            return dal.CapNhat(ncc);
        }
        public static List<NhaCungCapDTO> TimKiem(string tuKhoa)
        {
            return NhaCungCapDAL.TimKiem(tuKhoa);
        }
    }
}
