using OSRSClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OSRSClientSide.Controllers
{
    public class PaymentController : Controller
    {
        OrdersDTO orderDetail = new OrdersDTO() { order_id = 2, userid = 8, amount = 50000 };
        private const string baseurl = "https://localhost:44357/";
        private const string paymentPostUrl = "api/Payment";
        [HttpGet]
        public ActionResult Index()
        {
            int amount=0;
            int userid=8;
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //static values for development purpose

            Session["orderDetails"] = orderDetail;

            //actual code
            if (Session["price"] != null)
            {
                amount = int.Parse(Session["price"].ToString());
                userid = int.Parse(Session["userid"].ToString());
            }

            return View(new PaymentDTO() { amount = amount,userid=userid,order_id=orderDetail.order_id });
        }
        public bool validateName(string name)
        {
            bool isValidName = true;
            string regexUsernameValidationString = @"^[a-zA-Z]+$";
            Regex regexUsernameValidation = new Regex(regexUsernameValidationString);
            if (!regexUsernameValidation.IsMatch(name))
            {
                isValidName = false;
                //ViewBag.SelectPaymentMethod = "Name only contains alphabet";
            }

            return isValidName;
        }
        [HttpPost]
        public ActionResult Index(PaymentDTO payment)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (!string.IsNullOrEmpty(payment.netBankingName) ||
                (!string.IsNullOrEmpty(payment.nameOnCard) &&  payment.cardNumber != 0 && payment.cvv != 0 && payment.expiryDate != null )  )
            {
                
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(baseurl);
                    if (payment.order_id != orderDetail.order_id && payment.userid != orderDetail.userid && payment.amount != orderDetail.amount)
                    {
                        payment.order_id = orderDetail.order_id;
                        payment.userid = orderDetail.userid;
                        payment.amount = orderDetail.amount;
                    }

                    if (payment.netBankingName != null)
                    {
                        payment.cardNumber = null;
                        payment.cvv = null;
                        payment.expiryDate = null;
                    }
                    var response = client.PostAsJsonAsync<PaymentDTO>(paymentPostUrl, payment);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.PaymentStatus = "Payment successful";
                    }

                    return View();
                }
            }
            else
            {
                ViewBag.SelectPaymentMethod = "Please select any one payment method and fill out all required information with correct details";
                return View(new PaymentDTO() { amount = orderDetail.amount});
            }
            
        }
    }
}