using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class HouseCustomerController : Controller
    {
        [CustomSessionFilter]
        // GET: HouseCustomer
        public ActionResult Index()
        {
            return View();
        }
    }
}