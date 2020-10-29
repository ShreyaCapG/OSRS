using OSRSClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OSRSClientSide.Controllers
{
    public class ViewOrderController : Controller
    {
        string baseurl = "https://localhost:44357/api/";
        string errormessage = "Server error. Please Check the  URL";

        // GET: Product details
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<ViewOrderDTO> order = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                //HTTP GET
                var responseTask = client.GetAsync("ViewOrderSeller");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ViewOrderDTO>>();
                    readTask.Wait();

                    order = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    order = Enumerable.Empty<ViewOrderDTO>();

                    ModelState.AddModelError(string.Empty, errormessage);
                }
            }
            return View(order);
        }
        // GET: ViewOrder
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}