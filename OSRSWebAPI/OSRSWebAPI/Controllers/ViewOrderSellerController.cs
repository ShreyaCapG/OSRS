using OSRSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OSRSWebAPI.Controllers
{
    public class ViewOrderSellerController : ApiController
    {
        public IHttpActionResult GetAllOrderDetails()
        {
            IList<ViewOrderDTO> order = null;

            using (var ctx = new OSRSEntities())
            {
                order = ctx.OrderItemMappings
                            .Select(s => new ViewOrderDTO()
                            {
                                name = s.OrderTable.UserTable.name,
                                product_name = s.Product.product_name,
                                product_category = s.Product.product_category,

                                userid = s.OrderTable.userid,
                                order_id = s.order_id,
                                order_date = s.OrderTable.order_date,
                                shipping_date = s.OrderTable.shipping_date,
                                amount = s.OrderTable.amount,
                                
                            }).ToList<ViewOrderDTO>();
               
            }

            if (order.Count == 0)
            {
                return NotFound();
            }
            return Ok(order);

        }
    }
}
