using OSRSClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OSRSClientSide.Controllers
{
    public class PaymentController : Controller
    {
        OrdersDTO orderDetail = new OrdersDTO() { order_id = 2, userid = 8, amount = 900 };
        private const string baseurl = "https://localhost:44357/";
        private const string paymentPostUrl = "api/Payment";
        [HttpGet]
        public ActionResult Index()
        {
            //static values for development purpose
           
            Session["orderDetails"] = orderDetail;

            //actual code
            if (Session["orderDetails"]!=null)
            {}
           return View(new PaymentDTO() { amount = orderDetail.amount,userid=orderDetail.userid,order_id=orderDetail.order_id });
        }
        [HttpPost]
        public ActionResult Index(PaymentDTO payment)
        {


            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(baseurl);
                if (payment.order_id != orderDetail.order_id && payment.userid != orderDetail.order_id && payment.amount != orderDetail.amount)
                {
                    payment.order_id = orderDetail.order_id;
                    payment.userid = orderDetail.userid;
                    payment.amount = orderDetail.amount;
                    payment.expiryDate = DateTime.Now;
                }
                var response = client.PostAsJsonAsync<PaymentDTO>(paymentPostUrl, payment);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    //Response.Write("Sign up successful");

                    ViewBag.Message = "Payment successful";
                }

                return View();
            }
        }
    }
}