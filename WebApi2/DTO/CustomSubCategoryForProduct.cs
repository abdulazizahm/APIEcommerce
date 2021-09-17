using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.DTO
{
    public class CustomSubCategoryForProduct
    {
        public string Name { get; set; }
        public int? Parent_Id { get; set; }
     
        public int Cat_Id { get; set; }
        public string Photo { get; set; }
    }
}