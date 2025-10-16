using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTC;
using DAL_QLTC;
public class CT_PhieuHuyBLL
{
    private CT_PhieuHuyDAL dal = new CT_PhieuHuyDAL();

    public List<CT_PhieuHuyDTO> GetAllByMaPH(int maPH)
    {
        return dal.GetAllByMaPH(maPH);
    }
    public List<CT_PhieuHuyDTO> GetAll()
    {
        return dal.GetAll();
    }
    public bool Insert(CT_PhieuHuyDTO ct)
    {
        return dal.Insert(ct); // Gọi phương thức DAL
    }
}
