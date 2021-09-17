using BL.AppServices;
using BL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi2.Controllers
{
    public class SubCategoryController : ApiController
    {
        // GET: api/SubCategory
        SubCategoryAppService sub_CategoryAppServices = new SubCategoryAppService();
                 Main_CatAppService main_CatApp = new Main_CatAppService();
        public IHttpActionResult GetAll()
        {
            List<SubCategoryViewModel> sub_Categories = sub_CategoryAppServices.GetAllSubCategories();
            foreach (var item in sub_Categories)
            {
                 item.Main_CategoryName = main_CatApp.GetMain_Category(item.Cat_Id).Name;
                if (item.Parent_Id != null)
                {
                    item.ParentSubCategoryName = sub_CategoryAppServices.GetSubCategory((int)item.Parent_Id).Name;
                }
            }
            return Ok(sub_Categories);
        }
        // GET: api/SubCategory/5
        public IHttpActionResult Get(int id)
        {
            SubCategoryViewModel sub_Category = sub_CategoryAppServices.GetSubCategory(id);
            sub_Category.Main_CategoryName = main_CatApp.GetMain_Category(sub_Category.Cat_Id).Name;
            if (sub_Category.Parent_Id != null)
            {
                sub_Category.ParentSubCategoryName = sub_CategoryAppServices.GetSubCategory((int)sub_Category.Parent_Id).Name;
            }  
            return Ok(sub_Category);
        }

        // POST: api/SubCategory
        public IHttpActionResult Post()
        {
            SubCategoryViewModel sub_Category = new SubCategoryViewModel();
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Id"]))
            {
                sub_Category.Id = Convert.ToInt32(HttpContext.Current.Request.Form["Id"]);
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Name"]))
            {
                sub_Category.Name = HttpContext.Current.Request.Form["Name"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Parent_Id"]))
            {
                sub_Category.Parent_Id = Convert.ToInt32(HttpContext.Current.Request.Form["Parent_Id"]);
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Cat_Id"]))
            {
                sub_Category.Cat_Id = Convert.ToInt32(HttpContext.Current.Request.Form["Cat_Id"]);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (sub_Category.Parent_Id == 0)
                {
                    sub_Category.Parent_Id = null;
                }
             
                ImageUploaderService imageUploaderService = new ImageUploaderService(file, Directories.Sub_Categories);
                sub_Category.Photo = imageUploaderService.GetImageName();
                imageUploaderService.SaveImage();
                sub_CategoryAppServices.SaveNewSubCategory(sub_Category);
                return CreatedAtRoute("DefaultApi", new { id = sub_Category.Id }, sub_Category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*public void SaveFile(string data, string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (FileStream outputStream = File.Create(filePath))
            {
                using (Stream inputStream = new MemoryStream(new Base64Converter().ConvertToByteArray(data)))
                {
                    inputStream.Seek(0, SeekOrigin.Current);
                    inputStream.CopyTo(outputStream);
                }
            }
        }*/

        // PUT: api/SubCategory/5
        public IHttpActionResult Put(int id)
        {
            SubCategoryViewModel sub_Category = new SubCategoryViewModel();
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            sub_Category.Id = Convert.ToInt32(id);
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Name"]))
            {
                sub_Category.Name = HttpContext.Current.Request.Form["Name"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Parent_Id"]))
            {
                sub_Category.Parent_Id = Convert.ToInt32(HttpContext.Current.Request.Form["Parent_Id"]);
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Cat_Id"]))
            {
                sub_Category.Cat_Id = Convert.ToInt32(HttpContext.Current.Request.Form["Cat_Id"]);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (sub_Category.Parent_Id == 0)
                {
                    sub_Category.Parent_Id = null;
                }
                if (file == null)
                {
                    sub_Category.Photo = HttpContext.Current.Request.Form["Photo"];
                }
                else
                {
                    ImageUploaderService.DeleteImage(HttpContext.Current.Request.Form["Photo"], Directories.Sub_Categories);
                    ImageUploaderService imageUploaderService = new ImageUploaderService(file, Directories.Sub_Categories);
                    sub_Category.Photo = imageUploaderService.GetImageName();
                    imageUploaderService.SaveImage();
                }
               
                sub_CategoryAppServices.UpdateNormal(sub_Category);
                return Ok("Success Update");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/SubCategory/5
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                sub_CategoryAppServices.DeleteSubCategory(id);

                return StatusCode(HttpStatusCode.NoContent);//204
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
