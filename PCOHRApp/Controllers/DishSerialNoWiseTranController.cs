using PCOHRApp.DA;
using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class DishSerialNoWiseTranController : Controller
    {
        private DishBillCollectionDA _DishBillCollectionDA;
        public DishSerialNoWiseTranController()
        {
            _DishBillCollectionDA = new DishBillCollectionDA();
        }

        [CustomSessionFilterAttribute]       
        //
        // GET: /DishSerialNoWiseTran/
        public ActionResult Index()
        {
            return View();
        }
	}
}