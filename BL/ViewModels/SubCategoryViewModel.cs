using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels
{
  public  class SubCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Parent_Id { get; set; }
        [Required]
        public int Cat_Id { get; set; }
        public string Photo { get; set; }
        public string ParentSubCategoryName { get; set; }
        public string Main_CategoryName { get; set; }
      
      //  public Sub_Category Parent { get; set; }

       // public List<Product> Products { get; set; }
    }
}
