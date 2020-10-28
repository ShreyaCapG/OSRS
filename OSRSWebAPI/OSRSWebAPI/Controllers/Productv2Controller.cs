using OSRSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OSRSWebAPI.Controllers
{
    public class Productv2Controller : ApiController
    {
        public IHttpActionResult GetProductsById(int id)
        {
            ProductDTO productsseller = null;

            using (var ctx = new OSRSEntities())
            {
                productsseller = ctx.Products
                    .Where(s => s.product_id == id)
                    .Select(s => new ProductDTO()
                    {
                        product_id = s.product_id,
                        userid = s.userid,
                        product_name = s.product_name,
                        product_category = s.product_category,
                        price = s.price
                    }).FirstOrDefault<ProductDTO>();
            }

            if (productsseller == null)
            {
                return NotFound();
            }

            return Ok(productsseller);
        }
    }
}
