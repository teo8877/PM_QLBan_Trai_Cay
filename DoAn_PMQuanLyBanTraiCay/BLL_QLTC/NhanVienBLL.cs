using DAL_QLTC;
using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QLTC
{
    public class NhanVienBLL
    {
        private NhanVienDAL dal = new NhanVienDAL();

        public List<NhanVienDTO> GetAll()
        {
            return dal.GetAll();
        }

        // Thêm nhân viên mới và trả về mã nhân viên
        public int ThemNhanVien(NhanVienDTO nv)
        {
            return dal.ThemVaLayMaNV(nv);
        }

        // Sửa thông tin nhân viên
        public bool SuaNhanVien(NhanVienDTO nv)
        {
            return dal.SuaNhanVien(nv);
        }

        // Xóa nhân viên
        public bool XoaNhanVien(int maNV)
        {
            return dal.XoaNhanVien(maNV);
        }

        // Tìm kiếm nhân viên theo từ khóa
        public List<NhanVienDTO> TimKiemNhanVien(string keyword)
        {
            return dal.TimKiemNhanVien(keyword);
        }
    }
}
