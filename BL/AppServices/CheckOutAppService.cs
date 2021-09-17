using BL.Bases;
using BL.ViewModels;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
   public  class CheckOutAppService:BaseAppService
    {
        #region CURD

        public List<CheckOutViewModel> GetAllCheckOut()
        {
            return Mapper.Map<List<CheckOutViewModel>>(TheUnitOfWork.CheckOut.GetAllCheckOuts());
        }
        public CheckOutViewModel GetCheckOut(int id)
        {
            return Mapper.Map<CheckOutViewModel>(TheUnitOfWork.CheckOut.GetCheckOutById(id));
        }



        public bool SaveNewCheckOut(CheckOutViewModel CheckOutViewModel)
        {
            bool result = false;
            var CheckOut = Mapper.Map<Order>(CheckOutViewModel);
            if (TheUnitOfWork.CheckOut.Insert(CheckOut))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateCheckOut(CheckOutViewModel CheckOutViewModel)
        {
            var CheckOut = Mapper.Map<Order>(CheckOutViewModel);
            TheUnitOfWork.CheckOut.Update(CheckOut);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteCheckOut(int id)
        {
            TheUnitOfWork.CheckOut.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckCheckOutExists(CheckOutViewModel CheckOutViewModel)
        {
            Order CheckOut = Mapper.Map<Order>(CheckOutViewModel);
            return TheUnitOfWork.CheckOut.CheckCheckOutExists(CheckOut);
        }
        #endregion
    }
}
