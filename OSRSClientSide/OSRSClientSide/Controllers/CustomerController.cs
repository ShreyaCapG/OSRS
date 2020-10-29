using OSRSClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OSRSClientSide.Controllers
{
    public class CustomerController : Controller
    {
        bool isChecked = false;
        public static List<ProductDTO> cart = new List<ProductDTO>();
        public const string baseurl = "https://localhost:44357/api/";
        public static List<ProductDTO> allproducts = new List<ProductDTO>();
        string errormessage = "Server error. Please Check the  URL";
        //public ActionResult LogOut()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Login");
        //}
        public ActionResult Cart()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Session["orderdetails"] = cart;
            return View(cart);
        }
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            IEnumerable<ProductDTO> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                //HTTP GET
                var responseTask = client.GetAsync("Products");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductDTO>>();
                    readTask.Wait();

                    products = readTask.Result;
                }
                else
                //web api sent error response 
                {
                    //log response status here..

                    products = Enumerable.Empty<ProductDTO>();

                    ModelState.AddModelError(string.Empty, errormessage);
                }
            }
            foreach (var item in products)
            {
                if (!allproducts.Contains(item))
                {
                    allproducts.Add(item);
                }
            }
            return View(products);
            //return View();
        }

        public ActionResult AddCart(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }


            ProductDTO p = allproducts.FirstOrDefault(x => x.product_id == id);
            if (!cart.Contains(p))
            {
                cart.Add(p);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            foreach (var item in cart)
            {
                if (item.product_id == id)
                {
                    cart.Remove(item);
                    isChecked = true;
                    if (isChecked) { break; }
                }
            }
            return View("Cart", cart);
        }
        //int userid;
        // private const string baseurl = "https://localhost:44357/";
        //public CustomerController()
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
        //[HttpGet]
        //// GET: Seller
        //public ActionResult ViewCart()
        //{
        //    if (Session["userid"] == null)
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //    List<CartItemsDTO> cartItems = new List<CartItemsDTO>();
        //    string viewProductsGetUrl = "api/Products?id=" + Session["userid"].ToString();
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
        }
    }
