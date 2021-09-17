using BL.AppServices;
using BL.ViewModels;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi2.Controllers
{
    public class BrandController : ApiController
    {
        BrandAppService brandAppService = new BrandAppService();
       
        // GET: api/Brand
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            return Ok(brandAppService.GetAllBrand());
          
          
           
        }

        // GET: api/Brand/5
        public IHttpActionResult Get(int id)
        {
            return Ok(brandAppService.GetBrand(id));
        }

        // POST: api/Brand
        public IHttpActionResult Post()
        {
            var brandViewModel = new BrandViewModel();
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Name"]))
            {
                brandViewModel.Name = HttpContext.Current.Request.Form["Name"];
            }
            

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            try
            {
                ImageUploaderService imageUploaderService = new ImageUploaderService(file, Directories.Brands);
                brandViewModel.Photo = imageUploaderService.GetImageName();
                brandAppService.SaveNewBrand(brandViewModel);
                imageUploaderService.SaveImage();

                ImageUploaderService.RecreateFolder(Directories.Temp);
                return CreatedAtRoute("DefaultApi", new {id=brandViewModel.Id},brandViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Brand/5
        public IHttpActionResult Put(int id)
        {
            var brandViewModel = new BrandViewModel();
            brandViewModel.Id = id;
            ImageUploaderService imageUploaderService = null;
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Name"]))
            {
                brandViewModel.Name = HttpContext.Current.Request.Form["Name"];
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (file != null)
                {
                    imageUploaderService = new ImageUploaderService(file, Directories.Brands);
                    brandViewModel.Photo = imageUploaderService.GetImageName();
                }
                
                brandAppService.UpdateBrand(brandViewModel);

                imageUploaderService?.SaveImage(); 
                return Ok("Success Update");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Brand/5
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string imageName = brandAppService.GetBrand(id).Photo;
                brandAppService.DeleteBrand(id);
                if (imageName != "default.jpg")
                    ImageUploaderService.DeleteImage(imageName, Directories.Brands);
                return StatusCode(HttpStatusCode.NoContent);//204
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
