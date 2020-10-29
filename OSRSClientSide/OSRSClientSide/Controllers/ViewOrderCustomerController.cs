using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSRSClientSide.Controllers
{
    public class ViewOrderCustomerController : Controller
    {
        // GET: ViewOrderCustomer
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}