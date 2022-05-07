using PCOHRApp.DA;
using PCOHRApp.Models;
using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class DishMonthlyBillDeleteController : Controller
    {
        private DishMonthlyBillDeleteDA _objDishMonthlyBillDeleteDA = null;

        public DishMonthlyBillDeleteController()
        {
            _objDishMonthlyBillDeleteDA = new DishMonthlyBillDeleteDA();
        }
       [CustomSessionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }

       [CustomSessionFilterAttributeForAction]
       public JsonResult DishMonthlyBillDelete(BillCollectionVM _obj)
       {
           try
           {
               _obj.createdBy = Convert.ToInt32(Session["userId"]);
               string PageName = "DishMonthlyBillDeleteController";
               int status = _objDishMonthlyBillDeleteDA.CheckPassword(_obj, PageName);
               if (status > 0)
               {
                   string result = _objDishMonthlyBillDeleteDA.DishMonthlyBillDelete(_obj);
                   return Json(new { success = true, message = "Monthly bill deleted successfully." }, JsonRequestBehavior.AllowGet);
               }
               else
               {
                   return Json(new { success = true, message = "Please input correct Password." }, JsonRequestBehavior.AllowGet);
               }
           }

           catch (Exception ex)
           {
               return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
           }
       }

	}
}