using BL.AppServices;
using BL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi2.Controllers
{
    public class WishListController : ApiController
    {
        WishListAppService wishListAppService = new WishListAppService();
        // GET: api/WishList
        public IHttpActionResult Get()
        {
            return Ok(wishListAppService.GetAllWishList());
        }

        // GET: api/WishList/5
        public IHttpActionResult Get(int id)
        {
            WishListViewModel wishListView = wishListAppService.GetWishList(id);
            if (wishListView == null)
            {
                return NotFound();
            }
            return Ok(wishListView);
        }

        // POST: api/WishList
        public IHttpActionResult Post(WishListViewModel wishListView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                wishListAppService.SaveNewWishList(wishListView);
                return CreatedAtRoute("DefaultApi", new { id = wishListView.Id }, wishListView);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/WishList/5
        public IHttpActionResult Put(WishListViewModel wishListView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                wishListAppService.UpdateWishList(wishListView);
                return CreatedAtRoute("DefaultApi", new { id = wishListView.Id }, wishListView);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/WishList/5
        public IHttpActionResult Delete(int id)
        {
            wishListAppService.DeleteWishList(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
