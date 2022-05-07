using PCOHRApp.DA;
using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class BackUpController : Controller
    {
        private DishCustomerDeleteDA _objDishCustomerDeleteDA = null;
        public BackUpController()
        {
            _objDishCustomerDeleteDA = new DishCustomerDeleteDA();

        }
        //
        // GET: /BackUp/
        public ActionResult Index()
        {
            return View();
        }

        
            [CustomSessionFilterAttributeForAction]
        public JsonResult TakeBackup()
         {
             try
             {

                int result = _objDishCustomerDeleteDA.TakeBackup();
                return Json(new { success = true, message = "BackUp has been Taken Successfully." }, JsonRequestBehavior.AllowGet);
                
             }

             catch (Exception ex)
             {
                 return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
             }
         }
	}
}