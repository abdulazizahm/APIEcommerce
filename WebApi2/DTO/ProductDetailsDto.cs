using BL.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi2.DTO
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Photo { get; set; }
        [Required]
        [MinLength(20)]
        public string Desc { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]

        [Display(Name = "Offer Price")]
        public decimal Offer_Price { get; set; }

        public int Sell_Count { get; set; }

        public DateTime Created_at = DateTime.Now;
        [Display(Name = "Vendor")]
        public string Vendor_User_id { get; set; }
        [Required]
        [Display(Name = "Sub Category")]
        public int Sub_Cat_Id { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public int Brand_Id { get; set; }


        //public virtual ApplicationUser Vendor { get; set; }
        public string Vendor_Name { get; set; }

       
        public virtual List<RatingViewModel> Ratings { get; set; }
        public virtual RatingDetailsForProductDto RatingDetail { get; set; }
        public virtual List<ProductDto> RelatedProducts { get; set; }

        // public List<CustomWishListForProduct> wishLists { set; get; }

    }
}