using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSRSClientSide.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSRSClientSide.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        [TestMethod()]
        public void LoginHttpGetIndexTest()
        {
            LoginController controller = new LoginController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

       
    }
}