using OSRSClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OSRSClientSide.Controllers
{
    public class EditProfileController : Controller
    {
        private const string baseurl = "https://localhost:44357/";
        UsertableView userdetails;
        // GET: EditProfile
        [HttpGet]
        public ActionResult Index()
        {

            if (Session["userid"] != null)
            {
                int userid = int.Parse(Session["userid"].ToString());
                string editProfileGetUrl = "api/Users?userid=" + userid;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    var response = client.GetAsync(editProfileGetUrl);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<UsertableView>();
                        readTask.Wait();
                        userdetails = readTask.Result;

                        if (userdetails != null)
                        {
                            //UsertableDTO userDetails = 
                            Session["userdetailspost"] = userdetails;
                            return View(userdetails);
                        }
                    }
                }
            }
            return View("Index", "Login");
        }
        //[HttpPost]
        //public ActionResult Index(UsertableView updatedDetails)
        //{
        //    if(ModelState.IsValidField(updatedDetails.password))
        //    {
        //        //ModelState.AddModelError("password", "Enter valid password");
        //    }
        //    //UsertableView userdetails;

        //    if (Session["userid"] != null && ModelState.IsValid)
        //    {
        //        int userid = int.Parse(Session["userid"].ToString());
        //        string editProfileGetUrl = "api/Users";
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(baseurl);

        //            var response = client.PutAsJsonAsync<UsertableView>(editProfileGetUrl,updatedDetails);
        //            response.Wait();
        //            var result = response.Result;
        //            if (result.IsSuccessStatusCode)
        //            {
        //                ViewBag.EditProfileMessage = "Profile Edited Successfully and please login again with updated credentials";
        //            }
        //            else
        //            {
        //                ViewBag.EditProfileMessageFailed = "Failed to update details. Please try again";                                                                  
        //            }
        //        }
        //    }

        //    else
        //    {
        //        return View(updatedDetails);
        //    }
        //    return View();
        //}

        [HttpPost]
        public ActionResult Index(UsertableView updatedDetails)
        {
            userdetails = (UsertableView)Session["userdetailspost"];
            updatedDetails.usertype = new RoletypeDTO()
            {
                //roleid=2,
                roleid = userdetails.usertype.roleid,
                user_type = userdetails.usertype.user_type,
                UsertableDTOs = null
            };
            if (ModelState.IsValid && Session["userid"]!=null)
            {
                string editProfileGetUrl = "api/Users";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl);
                    var response = client.PutAsJsonAsync<UsertableView>(editProfileGetUrl, updatedDetails);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.EditProfileMessage = "Profile Edited Successfully and please login again with updated credentials";
                    }
                    else
                    {
                        ViewBag.EditProfileMessageFailed = "Failed to update details. Please try again";
                    }
                }
            }
            return View(updatedDetails);
        }
    }   
}