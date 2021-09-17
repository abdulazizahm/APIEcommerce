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
    public class SubCategoryAppService : BaseAppService
    {
        #region CURD

        public List<SubCategoryViewModel> GetAllSubCategories()
        {
            return Mapper.Map<List<SubCategoryViewModel>>(TheUnitOfWork.SubCategory.GetAllSubCategories());
        }
        public SubCategoryViewModel GetSubCategory(int id)
        {
            return Mapper.Map<SubCategoryViewModel>(TheUnitOfWork.SubCategory.GetSubCategoryById(id));
        }



        public bool SaveNewSubCategory(SubCategoryViewModel SubCategory)
        {
            bool result = false;
            var subcategory = Mapper.Map<Sub_Category>(SubCategory);
            if (TheUnitOfWork.SubCategory.Insert(subcategory))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateSubCategory(SubCategoryViewModel SubCategory)
        {
            var subcategory = Mapper.Map<Sub_Category>(SubCategory);
            TheUnitOfWork.SubCategory.Update(subcategory);
            TheUnitOfWork.Commit();

            return true;
        }
        public void UpdateNormal(SubCategoryViewModel SubCategory) // problem  in update becouse use same entity
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var subcategory = db.Sub_Categories.Where(s => s.Id == SubCategory.Id).FirstOrDefault();
            subcategory.Name = SubCategory.Name;
            subcategory.Parent_Id = SubCategory.Parent_Id;
            subcategory.Cat_Id = SubCategory.Cat_Id;
            subcategory.Photo = SubCategory.Photo;
            db.SaveChanges();
        }


        public bool DeleteSubCategory(int id)
        {
            TheUnitOfWork.SubCategory.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckSubCategoryExists(SubCategoryViewModel SubCategory)
        {
            Sub_Category subcategory= Mapper.Map<Sub_Category>(SubCategory);
            return TheUnitOfWork.SubCategory.CheckSubCategoryExists(subcategory);
        }
        #endregion
    }
}
