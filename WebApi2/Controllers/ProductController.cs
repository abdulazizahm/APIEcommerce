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
    public class ProductController : ApiController
    {
        ProductAppService productAppService = new ProductAppService();
        AccountAppService accountAppService = new AccountAppService();
        // GET: api/Product
        public IHttpActionResult Get()
        {
         List<ProductViewModel> productViewModels  = productAppService.GetAllBroducts();
            List<ProductDto> productDto = new List<ProductDto>();
            for (int i=0;i<productViewModels.Count;i++)
            {
                ProductDto dto = new ProductDto();
                dto.Id = productViewModels[i].Id;
                dto.Name = productViewModels[i].Name;
                dto.Offer_Price = productViewModels[i].Offer_Price;
                dto.Photo = productViewModels[i].Photo;
                dto.Price = productViewModels[i].Price;
                dto.Quantity = productViewModels[i].Quantity;
                dto.Sell_Count = productViewModels[i].Sell_Count;
                dto.Sub_Cat_Id = productViewModels[i].Sub_Cat_Id;
                dto.Vendor_Name = productViewModels[i].Vendor_Name;
                dto.Vendor_User_id = productViewModels[i].Vendor_User_id;
                dto.Brand_Id = productViewModels[i].Brand_Id;
                dto.Vendor_Name = accountAppService.FindById(productViewModels[i].Vendor_User_id).UserName;
                dto.Desc = productViewModels[i].Desc;
                dto.Brand = productViewModels[i].Brand;
                if (productViewModels[i].Ratings.Count!=0)
                {
                    dto.Ratings = new List<CustomRatingForProduct>();
                }
                foreach (var rating in productViewModels[i].Ratings) {
                    dto.Ratings.Add(new CustomRatingForProduct() { Comment = rating.Comment, Created_at = rating.Created_at, Product_Id = rating.Product_Id, Rate = rating.Rate, User_Id = rating.User_Id });
                }
                if (productViewModels[i].wishLists.Count==0)
                {
                    dto.wishLists = new List<CustomWishListForProduct>();
                }
                foreach (var wish in productViewModels[i].wishLists) {
                    dto.wishLists.Add(new CustomWishListForProduct {Id=wish.Id,Product_Id=wish.Product_Id,User_Id=wish.User_Id });
                }
                dto.Sub_Category = new CustomSubCategoryForProduct { Name = productViewModels[i].Sub_Category.Name, Cat_Id = productViewModels[i].Sub_Category.Cat_Id, Parent_Id = productViewModels[i].Sub_Category.Parent_Id, Photo = productViewModels[i].Sub_Category.Photo };
                productDto.Add(dto);
            }
            return Ok(productDto);
            
        }

        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            ProductViewModel productViewModels = productAppService.GetBroduct(id);
            ProductDto dto = new ProductDto();
            if (productViewModels == null)
            {
                return NotFound();
            }
               
                dto.Id = productViewModels.Id;
                dto.Name = productViewModels.Name;
                dto.Offer_Price = productViewModels.Offer_Price;
                dto.Photo = productViewModels.Photo;
                dto.Price = productViewModels.Price;
                dto.Quantity = productViewModels.Quantity;
                dto.Sell_Count = productViewModels.Sell_Count;
                dto.Sub_Cat_Id = productViewModels.Sub_Cat_Id;
                dto.Vendor_Name = productViewModels.Vendor_Name;
                dto.Vendor_User_id = productViewModels.Vendor_User_id;
                dto.Brand_Id = productViewModels.Brand_Id;
                dto.Vendor_Name = accountAppService.FindById(productViewModels.Vendor_User_id).UserName;
                dto.Desc = productViewModels.Desc;
                dto.Brand = productViewModels.Brand;
                if (productViewModels.Ratings.Count == 0)
                {
                    dto.Ratings = new List<CustomRatingForProduct>();
                }
                foreach (var rating in productViewModels.Ratings)
                {
                    dto.Ratings.Add(new CustomRatingForProduct { Comment = rating.Comment, Created_at = rating.Created_at, Product_Id = rating.Product_Id, Rate = rating.Rate, User_Id = rating.User_Id });
                }
                if (productViewModels.wishLists.Count == 0)
                {
                    dto.wishLists = new List<CustomWishListForProduct>();
                }
                foreach (var wish in productViewModels.wishLists)
                {
                    dto.wishLists.Add(new CustomWishListForProduct { Id = wish.Id, Product_Id = wish.Product_Id, User_Id = wish.User_Id });
                }
                dto.Sub_Category = new CustomSubCategoryForProduct { Name = productViewModels.Sub_Category.Name, Cat_Id = productViewModels.Sub_Category.Cat_Id, Parent_Id = productViewModels.Sub_Category.Parent_Id, Photo = productViewModels.Sub_Category.Photo };
               
            
            return Ok(dto);
        }
        
        public IHttpActionResult GetProductBySubCategory(int Subid)
        {
            List<ProductViewModel> productViewModels = productAppService.GetAllBroducts().Where(b => b.Sub_Cat_Id == Subid).ToList();
            List<ProductDto> productDto = new List<ProductDto>();
            for (int i = 0; i < productViewModels.Count; i++)
            {
                ProductDto dto = new ProductDto();
                dto.Id = productViewModels[i].Id;
                dto.Name = productViewModels[i].Name;
                dto.Offer_Price = productViewModels[i].Offer_Price;
                dto.Photo = productViewModels[i].Photo;
                dto.Price = productViewModels[i].Price;
                dto.Quantity = productViewModels[i].Quantity;
                dto.Sell_Count = productViewModels[i].Sell_Count;
                dto.Sub_Cat_Id = productViewModels[i].Sub_Cat_Id;
                dto.Vendor_Name = productViewModels[i].Vendor_Name;
                dto.Vendor_User_id = productViewModels[i].Vendor_User_id;
                dto.Brand_Id = productViewModels[i].Brand_Id;
                dto.Vendor_Name = accountAppService.FindById(productViewModels[i].Vendor_User_id).UserName;
                dto.Desc = productViewModels[i].Desc;
                dto.Brand = productViewModels[i].Brand;
                if (productViewModels[i].Ratings.Count != 0)
                {
                    dto.Ratings = new List<CustomRatingForProduct>();
                }
                foreach (var rating in productViewModels[i].Ratings)
                {
                    dto.Ratings.Add(new CustomRatingForProduct() { Comment = rating.Comment, Created_at = rating.Created_at, Product_Id = rating.Product_Id, Rate = rating.Rate, User_Id = rating.User_Id });
                }
                if (productViewModels[i].wishLists.Count == 0)
                {
                    dto.wishLists = new List<CustomWishListForProduct>();
                }
                foreach (var wish in productViewModels[i].wishLists)
                {
                    dto.wishLists.Add(new CustomWishListForProduct { Id = wish.Id, Product_Id = wish.Product_Id, User_Id = wish.User_Id });
                }
                dto.Sub_Category = new CustomSubCategoryForProduct { Name = productViewModels[i].Sub_Category.Name, Cat_Id = productViewModels[i].Sub_Category.Cat_Id, Parent_Id = productViewModels[i].Sub_Category.Parent_Id, Photo = productViewModels[i].Sub_Category.Photo };
                productDto.Add(dto);
            }
            return Ok(productDto);
        }

        [Route("api/product/store")]

        public IHttpActionResult GetProductByVendor(string id)
        {
            List<ProductViewModel> productViewModels = productAppService.GetAllBroducts().Where(b => b.Vendor_User_id == id).ToList();
            List<ProductDto> productDto = new List<ProductDto>();
            for (int i = 0; i < productViewModels.Count; i++)
            {
                ProductDto dto = new ProductDto();
                dto.Id = productViewModels[i].Id;
                dto.Name = productViewModels[i].Name;
                dto.Offer_Price = productViewModels[i].Offer_Price;
                dto.Photo = productViewModels[i].Photo;
                dto.Price = productViewModels[i].Price;
                dto.Quantity = productViewModels[i].Quantity;
                dto.Sell_Count = productViewModels[i].Sell_Count;
                dto.Sub_Cat_Id = productViewModels[i].Sub_Cat_Id;
                dto.Vendor_Name = productViewModels[i].Vendor_Name;
                dto.Vendor_User_id = productViewModels[i].Vendor_User_id;
                dto.Brand_Id = productViewModels[i].Brand_Id;
                dto.Vendor_Name = accountAppService.FindById(productViewModels[i].Vendor_User_id).UserName;
                dto.Desc = productViewModels[i].Desc;
                dto.Brand = productViewModels[i].Brand;
                if (productViewModels[i].Ratings.Count != 0)
                {
                    dto.Ratings = new List<CustomRatingForProduct>();
                }
                foreach (var rating in productViewModels[i].Ratings)
                {
                    dto.Ratings.Add(new CustomRatingForProduct() { Comment = rating.Comment, Created_at = rating.Created_at, Product_Id = rating.Product_Id, Rate = rating.Rate, User_Id = rating.User_Id });
                }
                if (productViewModels[i].wishLists.Count == 0)
                {
                    dto.wishLists = new List<CustomWishListForProduct>();
                }
                foreach (var wish in productViewModels[i].wishLists)
                {
                    dto.wishLists.Add(new CustomWishListForProduct { Id = wish.Id, Product_Id = wish.Product_Id, User_Id = wish.User_Id });
                }
                dto.Sub_Category = new CustomSubCategoryForProduct { Name = productViewModels[i].Sub_Category.Name, Cat_Id = productViewModels[i].Sub_Category.Cat_Id, Parent_Id = productViewModels[i].Sub_Category.Parent_Id, Photo = productViewModels[i].Sub_Category.Photo };
                productDto.Add(dto);
            }
            return Ok(productDto);
        }

        [Route("api/product/search")]
        [HttpGet]
        public IHttpActionResult Search([FromUri]SearchViewModel searchViewModel)
        {
            List<ProductViewModel> productViewModels;
            List<ProductDto> productDto = new List<ProductDto>();

            if (searchViewModel.SubCatId != 0)
            {
                productViewModels = productAppService.GetAllBroducts().Where(p => p.Sub_Cat_Id == searchViewModel.SubCatId && p.Name == searchViewModel.SearchText).ToList();
            }
            else
            {
                productViewModels = productAppService.GetAllBroducts().Where(p => p.Name == searchViewModel.SearchText.ToLower()).ToList();
            }

            for (int i = 0; i < productViewModels.Count; i++)
            {
                ProductDto dto = new ProductDto();
                dto.Id = productViewModels[i].Id;
                dto.Name = productViewModels[i].Name;
                dto.Offer_Price = productViewModels[i].Offer_Price;
                dto.Photo = productViewModels[i].Photo;
                dto.Price = productViewModels[i].Price;
                dto.Quantity = productViewModels[i].Quantity;
                dto.Sell_Count = productViewModels[i].Sell_Count;
                dto.Sub_Cat_Id = productViewModels[i].Sub_Cat_Id;
                dto.Vendor_Name = productViewModels[i].Vendor_Name;
                dto.Vendor_User_id = productViewModels[i].Vendor_User_id;
                dto.Brand_Id = productViewModels[i].Brand_Id;
                dto.Vendor_Name = accountAppService.FindById(productViewModels[i].Vendor_User_id).UserName;
                dto.Desc = productViewModels[i].Desc;
                dto.Brand = productViewModels[i].Brand;
                if (productViewModels[i].Ratings.Count != 0)
                {
                    dto.Ratings = new List<CustomRatingForProduct>();
                }
                foreach (var rating in productViewModels[i].Ratings)
                {
                    dto.Ratings.Add(new CustomRatingForProduct() { Comment = rating.Comment, Created_at = rating.Created_at, Product_Id = rating.Product_Id, Rate = rating.Rate, User_Id = rating.User_Id });
                }
                if (productViewModels[i].wishLists.Count == 0)
                {
                    dto.wishLists = new List<CustomWishListForProduct>();
                }
                foreach (var wish in productViewModels[i].wishLists)
                {
                    dto.wishLists.Add(new CustomWishListForProduct { Id = wish.Id, Product_Id = wish.Product_Id, User_Id = wish.User_Id });
                }
                dto.Sub_Category = new CustomSubCategoryForProduct { Name = productViewModels[i].Sub_Category.Name, Cat_Id = productViewModels[i].Sub_Category.Cat_Id, Parent_Id = productViewModels[i].Sub_Category.Parent_Id, Photo = productViewModels[i].Sub_Category.Photo };
                productDto.Add(dto);
            }


            return Ok(productDto);
        }

        // POST: api/Product
        public IHttpActionResult Post(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                productAppService.SaveNewBroduct(productViewModel);
                return CreatedAtRoute("DefaultApi",new { id=productViewModel.Id},productViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Product/5
        public IHttpActionResult Put(int id, ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                productAppService.UpdateBroduct(productViewModel);
                return CreatedAtRoute("DefaultApi", new { id = productViewModel.Id }, productViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(int id)
        {
            productAppService.DeleteBroduct(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
