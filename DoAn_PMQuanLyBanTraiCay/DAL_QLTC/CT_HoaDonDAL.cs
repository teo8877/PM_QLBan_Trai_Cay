using System;
using DTO_QLTC;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTC.DTO_QLTC;

namespace DAL_QLTC
{
    public class CT_HoaDonDAL
    {
        public List<CT_HoaDonDTO> GetAllByMaHD(int maHD)
        {
            List<CT_HoaDonDTO> result = new List<CT_HoaDonDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT cthd.MaHD, cthd.MaSP, sp.TenSP, cthd.SoLuong, cthd.DonGia, cthd.ThanhTien "
                             + "FROM CT_HoaDon cthd JOIN SanPham sp ON cthd.MaSP = sp.MaSP WHERE cthd.MaHD = @MaHD";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new CT_HoaDonDTO
                    {
                        MaHD = (int)reader["MaHD"],
                        MaSP = (int)reader["MaSP"],
                        TenSP = reader["TenSP"].ToString(),
                        SoLuong = (int)reader["SoLuong"],
                        DonGia = (decimal)reader["DonGia"],
                        ThanhTien = (decimal)reader["ThanhTien"]
                    });
                }
            }
            return result;
        }

        public void Insert(CT_HoaDonDTO ct)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = @"INSERT INTO CT_HoaDon (MaHD, MaSP, SoLuong, DonGia) 
                         VALUES (@MaHD, @MaSP, @SoLuong, @DonGia)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", ct.MaHD);
                cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", ct.DonGia);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
