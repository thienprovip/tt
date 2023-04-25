using System;
using ECommerce.API.Models;
namespace ECommerce.API.DataAccess.Interface
{
    public interface IDe
    {
        bool Delete(int id);
        bool InsertDe(De de);
        bool UpdateDe(De de);
    }
}
