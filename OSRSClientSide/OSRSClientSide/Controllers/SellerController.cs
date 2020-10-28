using OSRSClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OSRSClientSide.Controllers
{
    public class SellerController : Controller
    {
        int userid;
        private const string baseurl = "https://localhost:44357/";
        string errormessage = "Server error. Please Check the  URL";
        //public SellerController()
        //{
        //    if (Session["userid"] != null)
        //    {
        //        userid = (int)Session["userid"];
        //    }
        //    else
        //    {
        //        RedirectToAction("Index", "Login");
        //    }
        //}

        public void Index()
        {
            //this.ShowProducts();
            //return View("ShowProducts");
            
        }
        //[HttpGet]
        //// GET: Seller
        //public ActionResult GetCartItemMapping()
        //{
        //    List<CartItemsDTO> cartItems = new List<CartItemsDTO>();
        //    string viewProductsGetUrl = "api/Products?id=" + userid;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseurl);
        //        var response = client.GetAsync(viewProductsGetUrl);
        //        response.Wait();
        //        var result = response.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<List<CartItemsDTO>>();
        //            readTask.Wait();
        //            cartItems = readTask.Result;
        //        }
        //        return View(cartItems);
        //    }
        //}

        public ActionResult ShowProducts()
        {
            var userid = Session["userid"];
            IEnumerable<ProductDTO> products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                //HTTP GET
                var responseTask = client.GetAsync("api/Products?userid=" + userid.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductDTO>>();
                    
                    //var readTask = result.Content.GetAsAsync<IList<ProductDTO>>();
                    readTask.Wait();

                    products = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<ProductDTO>();

                    ModelState.AddModelError(string.Empty, errormessage);
                }
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductDTO product)
        {
            if (Session["userid"] != null && ModelState.IsValid)
            {
                product.userid = (int)Session["userid"];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ProductDTO>("api/Products", product);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ShowProducts");
                    }
                }

                ModelState.AddModelError(string.Empty, errormessage);
            }

            //else
            //{
            //    ModelState.AddModelError()
            //}
            return View(product);

        }

        [HttpGet]
        public ActionResult UpdateProduct(int? id)
        {
            ProductDTO product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                var responseTask = client.GetAsync("api/Productv2?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductDTO>();
                    readTask.Wait();

                    product = readTask.Result;
                }
            }

            return View(product);
        }

        
        [HttpPost]
        public ActionResult UpdateProduct(ProductDTO product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ProductDTO>("api/Products", product);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }


        //might need some changes
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }


        //need some database changes, do not call if not necessary
        public ActionResult DeleteProduct(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/Products/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}