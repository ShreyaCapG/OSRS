using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OSRSWebAPI.Controllers
{
    public class GetCartController : ApiController
    {
        //[HttpGet]
        //public IHttpActionResult GetAllCart(int id)
        //{
        //    IList<CartDTO> cart = null;
        //    using (var ctx = new OSRSEntities())
        //    {
        //        cart = ctx.CartItemMappings.Where(s => s.Product.userid == id)

        //                    .Select(s => new CartDTO()
        //                    {
        //                        cart_id = s.cart_id,
        //                        product_id = s.product_id,
        //                        //userid = s.Product.userid,
        //                        // product_category = s.Product.product_category,
        //                        //product_name = s.Product.product_name,
        //                        price = s.Product.price,
        //                        quantity = s.quantity,
        //                        no_of_renting_days = s.no_of_renting_days
        //                        //cart_id=s.cart_id,userid=s.userid,price=s.price
        //                    }).ToList<CartDTO>();
        //    }

        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cart);
        //}
    }
}
