using BL.Bases;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class CheckOutRepository:BaseRepository<Order>
    {
        public CheckOutRepository(DbContext db) : base(db)
        {
        }

        #region CRUB

        public List<Order> GetAllCheckOuts()
        {
            return GetAll().ToList();
        }

        public bool InsertCheckOut(Order CheckOut)
        {
            return Insert(CheckOut);
        }
        public void UpdateCheckOut(Order CheckOut)
        {
            Update(CheckOut);
        }
        public void DeleteCheckOut(int id)
        {
            Delete(id);
        }

        public bool CheckCheckOutExists(Order CheckOut)
        {
            return GetAny(b => b.Id == CheckOut.Id);
        }
        public Order GetCheckOutById(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        #endregion
    }
}
