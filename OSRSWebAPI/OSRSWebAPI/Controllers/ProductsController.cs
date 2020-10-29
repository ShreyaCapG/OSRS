using OSRSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OSRSWebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        //public IHttpActionResult GetAllUsers()
        //{
        //    List<ProductDTO> products = new List<ProductDTO>();

        //    using (var ctx = new OSRSEntities())
        //    {

        //        var query= ctx.ViewProducts(8);
        ////         public int product_id { get; set; }
        ////public int userid { get; set; }
        ////public string product_name { get; set; }
        ////public double price { get; set; }
        ////public string product_category { get; set; }
        //        foreach(var item in query)
        //        {
        //            ProductDTO product = new ProductDTO();
        //            product.product_id = item.Product_ID;
        //            product.product_name = item.Product_Name;
        //            product.prxice = item.Price;
        //            products.Add(product);
        //        }
        //        //if (products.Count == 0)
        //        //{
        //        //    return NotFound();
        //        //}

        //        return Ok(products);
        //    }
        //}
        public IHttpActionResult GetAllProducts()
        {

            IList<ProductDTO> productsseller = null;

            using (var ctx = new OSRSEntities())
            {
                productsseller = ctx.Products
                            .Select(s => new ProductDTO()
                            {
                                product_id = s.product_id,
                                userid = s.userid,
                                product_category = s.product_category,
                                product_name = s.product_name,
                                price = s.price
                            }).ToList<ProductDTO>();
            }

            if (productsseller.Count == 0)
            {
                return NotFound();
            }

            return Ok(productsseller);
        }
        //2
        public IHttpActionResult GetProductsByUserId(int userid)
        {
            
            IList<OSRSWebAPI.Models.ProductDTO> productsseller = null;

            using (var ctx = new OSRSEntities())
            {
                //productsseller = ctx.Products
                //            .Select(s => new ProductSellerDTO()
                //            {
                //                product_id=s.product_id,
                //                userid = s.userid,
                //                product_category = s.product_category,
                //                product_name = s.product_name,
                //                price= s.price
                //            }).ToList<ProductSellerDTO>();

                //added userid to filter
                productsseller = ctx.Products
                                  .Where(s => s.userid == userid)
                            .Select(s => new ProductDTO()
                            {
                                product_id = s.product_id,
                                userid = s.userid,
                                product_category = s.product_category,
                                product_name = s.product_name,
                                price = s.price
                            }).ToList<ProductDTO>();
            }

            if (productsseller.Count == 0)
            {
                return NotFound();
            }

            return Ok(productsseller);
        }
        //2
        public IHttpActionResult PostNewProducts(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new OSRSEntities())
            {
                ctx.Products.Add(new Product()
                {

                    userid = product.userid,
                    product_category = product.product_category,
                    product_name = product.product_name,
                    price = product.price
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        //2
        public IHttpActionResult Put(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new OSRSEntities())
            {
                var existingproducts = ctx.Products.Where(s => s.product_id == product.product_id)
                                                        .FirstOrDefault<Product>();

                if (existingproducts != null)
                {
                    existingproducts.product_category = product.product_category;
                    existingproducts.product_name = product.product_name;
                    existingproducts.price = product.price;
                    ctx.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }

            
        }

        //2
        //need some changes
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Product id");

            using (var ctx = new OSRSEntities())
            {
                var deleteproduct = ctx.Products
                    .Where(s => s.product_id == id)
                    .FirstOrDefault();
                ctx.Entry(deleteproduct).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult GetViewCart(int id)
        {
            List<CartItemsDTO> cartItems = new List<CartItemsDTO>();
            var context = new OSRSEntities();

            try
            {

                //var query = from p in context.Products
                //            join c in context.CartItemMappings on
                //            p.product_id equals c.product_id
                //            join d in context.Carts on c.cart_id equals d.cart_id
                //            where p.userid == id
                //            select new { p.product_name, p.price, p.product_category, c.cart_id, c.quantity, c.no_of_renting_days, Total = p.price * c.quantity };
                var query = context.getcartitemmappingv3(id);
                foreach (var item in query)
                {
                    CartItemsDTO cartitem = new CartItemsDTO();
                    cartitem.product_name = item.product_name;
                    cartitem.price = item.price;
                    cartitem.product_category = item.product_category;
                    cartitem.quantity = item.quantity;
                    cartitem.no_of_renting_days = item.no_of_renting_days;
                    cartitem.cartid = item.cart_id;
                   //// cartitem.productid = item.product_id;
                    cartitem.total = item.total;
                    cartItems.Add(cartitem);
                }
                if (query == null)
                {
                    return NotFound();
                }
                return Ok(cartItems);

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
