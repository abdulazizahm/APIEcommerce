using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.DTO
{
    public class CustomRatingForProduct
    {
       
    
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime Created_at { get; set; }

     
        public string User_Id { get; set; }
      
        public int Product_Id { get; set; }
    }
}