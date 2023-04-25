using System;
using ECommerce.API.Models;
namespace ECommerce.API.DataAccess
{
    public interface IGiangVien
    {

        bool InsertGiangVien(GiangVien gv);
        GiangVien GetGiangVien(int id);
        bool Delete(int id);
        bool Update(GiangVien gv);
    }
}

