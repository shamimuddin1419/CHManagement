using PCOHRApp.DA;
using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class InternetSerialNoWiseTranController : Controller
    {     
        public InternetSerialNoWiseTranController()
        {
          
        }
       
        [CustomSessionFilterAttribute]       
        //
        // GET: /InternetSerialNoWiseTran/
        public ActionResult Index()
        {
            return View();
        }
	}
}