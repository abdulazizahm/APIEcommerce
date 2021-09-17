using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.DTO
{
    public class CustomWishListForProduct
    {
        public int Id { get; set; }

        public string User_Id { get; set; }
      
        public int Product_Id { get; set; }
    }
}