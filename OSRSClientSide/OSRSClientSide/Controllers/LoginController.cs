using OSRSClientSide.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using usertypeenum = OSRSClientSide.Models.EnumsAndConstants.Usertype;
namespace OSRSClientSide.Controllers
{
    public class LoginController : Controller
    {
        private static string username;
        private static string password;
        private static int usertype;
        private const string baseurl = "https://localhost:44357/";
        private string amSeller = "Index";
        private string amCustomer = "Index";
        private string controllerSeller = "Seller";
        private string controllerCustomer = "Customer";
        public enum Usertype
        {
            seller = 1,
            customer = 2
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginDTO loginDetails)
        {
            IList<UsertableView> userdetails;
             username = loginDetails.username;
             password = loginDetails.password;
             usertype = (int)loginDetails.usertype;
             string loginPostUrl = "api/Users?username=" + username
                                              + "&password=" + password
                                              + "&usertype=" + usertype;
            if (!(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || usertype == 0))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    var response = client.GetAsync(loginPostUrl);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<UsertableView>>();
                        readTask.Wait();
                        userdetails = readTask.Result;
                        if (userdetails.Count == 1)
                        {
                            Session["userid"] = userdetails[0].userid;
                            if (userdetails[0].usertype.roleid == (int)usertypeenum.seller)
                            {
                                return RedirectToAction(amSeller, controllerSeller);
                                
                            }
                            else if (userdetails[0].usertype.roleid == (int)usertypeenum.customer)
                            {
                               return RedirectToAction(amCustomer, controllerCustomer);
                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid credentials");
                        Response.Write("Login not successful");
                    }
                }
            }
                return View();
        }
    }
}