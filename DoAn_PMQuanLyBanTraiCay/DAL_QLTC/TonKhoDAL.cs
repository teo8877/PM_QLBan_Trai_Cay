using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTC;
namespace DAL_QLTC
{
    public class TonKhoDAL
    {


        public List<TonKhoDTO> GetBaoCaoTonKho(   )
        {
            List<TonKhoDTO> list = new List<TonKhoDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = @"
                SELECT 
                    sp.MaSP,
                    sp.TenSP,
                    ISNULL(nhap.TongNhap, 0) AS TongNhap,
                    ISNULL(huy.TongHuy, 0) AS TongHuy,
                    ISNULL(ban.TongBan, 0) AS TongBan,
                    (ISNULL(nhap.TongNhap, 0) - ISNULL(huy.TongHuy, 0) - ISNULL(ban.TongBan, 0)) AS TonKho
                FROM SanPham sp
                LEFT JOIN (
                    SELECT MaSP, SUM(SoLuong) AS TongNhap FROM CT_PhieuNhap GROUP BY MaSP
                ) AS nhap ON sp.MaSP = nhap.MaSP
                LEFT JOIN (
                    SELECT MaSP, SUM(SoLuong) AS TongHuy FROM CT_PhieuHuy GROUP BY MaSP
                ) AS huy ON sp.MaSP = huy.MaSP
                LEFT JOIN (
                    SELECT MaSP, SUM(SoLuong) AS TongBan FROM CT_HoaDon GROUP BY MaSP
                ) AS ban ON sp.MaSP = ban.MaSP";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new TonKhoDTO
                    {
                        MaSP = (int)reader["MaSP"],
                        TenSP = reader["TenSP"].ToString(),
                        TongNhap = (int)reader["TongNhap"],
                        TongHuy = (int)reader["TongHuy"],
                        TongBan = (int)reader["TongBan"],
                        TonKho = (int)reader["TonKho"]
                    });
                }
            }
            return list;
        }

    }
}
