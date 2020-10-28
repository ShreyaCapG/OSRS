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
        //public ActionResult LogOut()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Login");
        //}
        public ActionResult Index()
        {
            return View();
        }
        //int userid;
        private const string baseurl = "https://localhost:44357/";
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
        [HttpGet]
        // GET: Seller
        public ActionResult ViewCart()
        {
            List<CartItemsDTO> cartItems = new List<CartItemsDTO>();
            string viewProductsGetUrl = "api/Products?id=" + Session["userid"].ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                var response = client.GetAsync(viewProductsGetUrl);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<CartItemsDTO>>();
                    readTask.Wait();
                    cartItems = readTask.Result;
                }
                return View(cartItems);
            }
        }
    }
}