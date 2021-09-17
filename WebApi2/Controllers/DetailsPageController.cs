using BL.AppServices;
using BL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.DTO;

namespace WebApi2.Controllers
{
    public class DetailsPageController : ApiController
    {
        ProductAppService ProductAppService = new ProductAppService();

        AccountAppService accountAppService = new AccountAppService();
        SubCategoryAppService subCategoryAppService = new SubCategoryAppService();
        RatingAppService ratingAppService = new RatingAppService();
        // GET api/<controller>
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var product = ProductAppService.GetBroduct(id);
            ProductDetailsDto dto = new ProductDetailsDto();
            dto.Id = product.Id;
            dto.Name = product.Name;
            dto.Offer_Price = product.Offer_Price;
            dto.Photo = product.Photo;
            dto.Price = product.Price;
            dto.Desc = product.Desc;
            dto.Quantity = product.Quantity;
            dto.Sell_Count = product.Sell_Count;
            dto.Sub_Cat_Id = product.Sub_Cat_Id;
            dto.Vendor_Name = product.Vendor_Name;
            dto.Vendor_User_id = product.Vendor_User_id;
            dto.Vendor_Name = accountAppService.FindById(product.Vendor_User_id).UserName;
            List<ProductViewModel> products = ProductAppService.GetAllBroducts().Where(p => p.Sub_Cat_Id == product.Sub_Cat_Id).Take(4).ToList();
            dto.RelatedProducts = new List<ProductDto>();
            for (int i = 0; i < products.Count; i++)
            {
                ProductDto prodtdto = new ProductDto();
                prodtdto.Id = products[i].Id;
                prodtdto.Name = products[i].Name;
                prodtdto.Offer_Price = products[i].Offer_Price;
                prodtdto.Photo = products[i].Photo;
                prodtdto.Price = products[i].Price;
                prodtdto.Quantity = products[i].Quantity;
                prodtdto.Sell_Count = products[i].Sell_Count;
                prodtdto.Sub_Cat_Id = products[i].Sub_Cat_Id;
                prodtdto.Vendor_Name = products[i].Vendor_Name;
                prodtdto.Vendor_User_id = products[i].Vendor_User_id;
                prodtdto.Brand_Id = products[i].Brand_Id;
                prodtdto.Vendor_Name = accountAppService.FindById(products[i].Vendor_User_id).UserName;
                prodtdto.Desc = products[i].Desc;
                prodtdto.Brand = products[i].Brand;
                if (products[i].Ratings.Count != 0)
                {
                    prodtdto.Ratings = new List<CustomRatingForProduct>();
                }
                foreach (var rat in products[i].Ratings)
                {
                    prodtdto.Ratings.Add(new CustomRatingForProduct() { Comment = rat.Comment, Created_at = rat.Created_at, Product_Id = rat.Product_Id, Rate = rat.Rate, User_Id = rat.User_Id });
                }
                if (products[i].wishLists.Count == 0)
                {
                    prodtdto.wishLists = new List<CustomWishListForProduct>();
                }
                foreach (var wish in products[i].wishLists)
                {
                    prodtdto.wishLists.Add(new CustomWishListForProduct { Id = wish.Id, Product_Id = wish.Product_Id, User_Id = wish.User_Id });
                }
                prodtdto.Sub_Category = new CustomSubCategoryForProduct { Name = products[i].Sub_Category.Name, Cat_Id = products[i].Sub_Category.Cat_Id, Parent_Id = products[i].Sub_Category.Parent_Id, Photo = products[i].Sub_Category.Photo };
                dto.RelatedProducts.Add(prodtdto);
            }
            var rating = ratingAppService.GetAllRating().Where(r => r.Product_Id == id);
            dto.Ratings = new List<RatingViewModel>();
            dto.Ratings= rating.Take(3).ToList();
            //prod.Vendor_Name = accountAppService.FindById(prod.Vendor_User_id).UserName;

            RatingDetailsForProductDto displaypresforrat = new RatingDetailsForProductDto();
            int count = rating.Count();
            displaypresforrat.count = count;
            int sum = 0;
            foreach (var item in rating)
            {
                sum += item.Rate;
            }
            int aveg = 0;
            if (count != 0)
            {
                aveg = (sum / count);
            }
            if (aveg != 0)
            {
                displaypresforrat.pres = (double)sum / (double)(5 * count);
                displaypresforrat.rat5 = rating.Where(r => r.Rate == 5).Count();
                displaypresforrat.rat4 = rating.Where(r => r.Rate == 4).Count();
                displaypresforrat.rat3 = rating.Where(r => r.Rate == 3).Count();
                displaypresforrat.rat2 = rating.Where(r => r.Rate == 2).Count();
                displaypresforrat.rat1 = rating.Where(r => r.Rate == 1).Count();
                int sumrat = displaypresforrat.rat1 + displaypresforrat.rat2 + displaypresforrat.rat3 + displaypresforrat.rat4 + displaypresforrat.rat5;
                displaypresforrat.rat5pres = ((double)displaypresforrat.rat5 / (double)sumrat) * 100.0;
                displaypresforrat.rat4pres = ((double)displaypresforrat.rat4 / (double)sumrat) * 100.0;
                displaypresforrat.rat3pres = ((double)displaypresforrat.rat3 / (double)sumrat) * 100.0;
                displaypresforrat.rat2pres = ((double)displaypresforrat.rat2 / (double)sumrat) * 100.0;
                displaypresforrat.rat1pres = ((double)displaypresforrat.rat1 / (double)sumrat) * 100.0;
            }

            //ViewBag.presrat5 = pres;
            displaypresforrat.aveg = aveg;
            dto.RatingDetail = new RatingDetailsForProductDto();
            dto.RatingDetail = displaypresforrat;
            foreach (var item in dto.Ratings)
            {
                item.Username = accountAppService.FindById(item.User_Id).UserName;
            }

            return Ok(dto);
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult AddRate(RatingViewModel ratingViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ratingViewModel.User_Id = accountAppService.Find(ratingViewModel.Username).Id;
                if (ratingAppService.GetAllRating().Where(r => r.User_Id == ratingViewModel.User_Id && r.Product_Id == ratingViewModel.Product_Id).Count() == 0)
                {
                    if (ratingViewModel.Rate == 0)
                    {
                        ratingViewModel.Rate = 3;
                    }

                    ratingViewModel.Created_at = DateTime.Now;
                    ratingAppService.SaveNewRating(ratingViewModel);
                    return CreatedAtRoute("DefaultApi", new { id = ratingViewModel.Id }, ratingViewModel);
                }
                return BadRequest("sorry! the request not apply,you rated this product before");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}