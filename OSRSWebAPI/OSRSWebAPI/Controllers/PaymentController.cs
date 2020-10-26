using OSRSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Http;

namespace OSRSWebAPI.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpGet]
        //public IHttpActionResult GetAllPayments()
        //{
        //    IList<PaymentDTO> payments = null;

        //    using (var ctx = new OSRSEntities())
        //    {
        //        payments = ctx.Payments.
        //                    Select(u => new PaymentDTO()
        //                    {
        //                        nameOnCard = u.nameOnCard,
        //                        username = u.username,
        //                        password = u.password,
        //                        email = u.email,
        //                        contact_number = u.contact_number,
        //                        userid = u.userid,
        //                        usertype = new RoletypeDTO()
        //                        {
        //                            roleid = u.RoleType.roleid,
        //                            user_type = u.RoleType.user_type
        //                        }
        //                    }).ToList<PaymentDTO>();
        //    }

        //    if (users.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(users);
        //}
        [HttpPost]
        public IHttpActionResult AddPaymentDetails(PaymentDTO payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            using (var ctx = new OSRSEntities())
            {
                
                ctx.addPaymentTransactions(payment.nameOnCard,
                                           payment.cardNumber,
                                           payment.cvv,
                                           payment.netBankingName,
                                           payment.order_id,
                                           payment.userid,
                                           payment.expiryDate,
                                           payment.amount);
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
