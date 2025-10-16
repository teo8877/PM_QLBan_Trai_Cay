using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class CT_PhieuNhapDAL
    {
        public List<CT_PhieuNhapDTO> GetAllByMaPN(int maPN)
        {
            List<CT_PhieuNhapDTO> result = new List<CT_PhieuNhapDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM CT_PhieuNhap WHERE MaPN = @MaPN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPN", maPN);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new CT_PhieuNhapDTO
                    {
                        MaPN = (int)reader["MaPN"],
                        MaSP = (int)reader["MaSP"],
                        SoLuong = (int)reader["SoLuong"],
                        DonGia = (decimal)reader["DonGia"],
                        ThanhTien = (decimal)reader["ThanhTien"]
                    });
                }
            }
            return result;
        }

        public bool Insert(CT_PhieuNhapDTO ct)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "INSERT INTO CT_PhieuNhap (MaPN, MaSP, SoLuong, DonGia) VALUES (@MaPN, @MaSP, @SoLuong, @DonGia)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPN", ct.MaPN);
                cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", ct.DonGia);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}
