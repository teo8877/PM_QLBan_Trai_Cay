using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using DTO_QLTC;

using DAL_QLTC;

namespace BLL_QLTC
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL dal = new TaiKhoanDAL();
        public  List<TaiKhoanDTO> GetAll()
        {
            return dal.GetAll();
        }
       
            public static string LayMatKhau(string tenDN, string email)
            {
                return TaiKhoanDAL.LayMatKhau(tenDN, email);
            }
        

        public TaiKhoanDTO DangNhap(string loginName, string password)
        {
            // Nếu cần mã hóa password, xử lý ở đây trước khi truyền xuống DAL
            return dal.DangNhap(loginName, password);
        }

        public bool TaoTaiKhoan(TaiKhoanDTO tk)
        {
            return dal.TaoTaiKhoan(tk);
        }

         

        public bool DoiMatKhau(string tenDN, string mkMoi)
        {
            TaiKhoanDAL dal = new TaiKhoanDAL();
            return dal.DoiMatKhau(tenDN, mkMoi);
        }
        public bool KiemTraDangNhap(string loginName, string password)
        {
            if (string.IsNullOrWhiteSpace(loginName))
            {
                MessageBox.Show("Tên đăng nhập không được bỏ trống.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Mật khẩu không được bỏ trống.");
                return false;
            }

            // Lấy tài khoản theo tên đăng nhập
            TaiKhoanDTO tk = dal.GetByTenDangNhap(loginName);
            if (tk == null)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại.");
                return false;
            }

            if (tk.MK != password)
            {
                MessageBox.Show("Mật khẩu không đúng.");
                return false;
            }

            if (tk.TrangThai == "Khoa")
            {
                MessageBox.Show("Tài khoản đã bị khóa.");
                return false;
            }
            // Đăng nhập thành công
            return true;
        }
        public bool DangKyTaiKhoanKhachHang(KhachHangDTO kh, string tenDN, string matKhau)
        {
            // Bước 1: Thêm khách hàng mới vào bảng KhachHang
            KhachHangDAL khDAL = new KhachHangDAL();
            int maKH = khDAL.ThemVaLayMaKH(kh);
            if (maKH <= 0)
                return false;

            // Bước 2: Tạo tài khoản với MaKH vừa thêm
            TaiKhoanDTO tk = new TaiKhoanDTO
            {
                TenDN = tenDN,
                MK = matKhau, // Nên hash mật khẩu ở đây
                LoaiTK = "KhachHang",
                TrangThai = "HoatDong",
                Email = kh.Email,
                SDT = kh.SDT,
                MaNV = null,
                MaKH = maKH
            };
            return dal.TaoTaiKhoan(tk);
        }

        //Tạo tài khoản nhân viên
        public bool DangKyTaiKhoanNhanVien(NhanVienDTO nv, string tenDN, string matKhau)
        {
            NhanVienDAL nvDAL = new NhanVienDAL();
            int maNV = nvDAL.ThemVaLayMaNV(nv); // Trả về mã nhân viên tự tăng
            if (maNV <= 0) return false;

            TaiKhoanDTO tk = new TaiKhoanDTO
            {
                TenDN = tenDN,
                MK = matKhau,
                LoaiTK = "NhanVien",
                TrangThai = "HoatDong",
                Email = nv.Email,
                SDT = nv.SDT,
                MaNV = maNV,
                MaKH = null
            };
            return dal.TaoTaiKhoan(tk);
        }
        public string LayLoaiTaiKhoan(string tenDangNhap)
        {
            return dal.LayLoaiTaiKhoan(tenDangNhap);
        }

        // frm Quản lý tài khoản
        public bool TaoTaiKhoanfrmQL(TaiKhoanDTO tk)
        {
            return dal.TaoTaiKhoanfrmQL(tk);
        }

        public bool SuaTaiKhoan(TaiKhoanDTO tk)
        {
            return dal.SuaTaiKhoan(tk);
        }

        public bool XoaTaiKhoan(string tenDN)
        {
            return dal.XoaTaiKhoan(tenDN);
        }

        public List<TaiKhoanDTO> TimKiemTaiKhoan(string keyword)
        {
            return dal.TimKiemTaiKhoan(keyword);
        }
        
    }
}