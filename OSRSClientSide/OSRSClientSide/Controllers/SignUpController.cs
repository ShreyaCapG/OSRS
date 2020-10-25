using System;
using System.Net.Http;
using System.Web.Mvc;
using OSRSClientSide.Models;

using usertypeenum = OSRSClientSide.Models.EnumsAndConstants.Usertype;
namespace OSRSClientSide.Controllers
{
    public class SignUpController : Controller
    {
        private const string baseurl = "https://localhost:44357/";
        private const string signupPostUrl = "api/Users";
        
        [HttpGet]
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UsertableDTO usertable)
        {
            bool isSignupSuccessful = false;
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri(baseurl);
                UsertableView usertableview = new UsertableView();
                usertableview.userid = usertable.userid;
                usertableview.name = usertable.name;
                usertableview.password = usertable.password;
                usertableview.email = usertable.email;
                usertableview.contact_number = usertable.contact_number;
                usertableview.username = usertable.username;
                //create constant to check usertype - use enum
                
                //if (usertable.usertype.ToString() == "seller")
                if(usertable.usertype.ToString()==usertypeenum.seller.ToString())
                {
                    usertableview.usertype = new RoletypeDTO()
                    {
                        //roleid = 1,
                        roleid = (int)usertypeenum.seller,
                        user_type = usertypeenum.seller.ToString(),
                        UsertableDTOs=null
                        
                    };

                }
                else
                {
                    usertableview.usertype = new RoletypeDTO()
                    {
                        //roleid=2,
                        roleid = (int)usertypeenum.customer,
                        user_type = usertypeenum.customer.ToString(),
                        UsertableDTOs=null
                    };
                   
                }
                //client.PostAsJsonAsync<>()
                var response = client.PostAsJsonAsync<UsertableView>(signupPostUrl, usertableview);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    //Response.Write("Sign up successful");
                    isSignupSuccessful = true;
                    ViewBag.Message = "Sign up successful";                   
                }
            }

            if (isSignupSuccessful == false)
            {
                ModelState.AddModelError("", "Error occured");
                Response.Write("Sign up not successful");
            }
            return View(usertable);
            
        }
    }
}