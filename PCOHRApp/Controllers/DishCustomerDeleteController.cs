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
    public class DishCustomerDeleteController : Controller
    {
        private DishCustomerDeleteDA _objDishCustomerDeleteDA = null;

        public DishCustomerDeleteController()
        {
            _objDishCustomerDeleteDA = new DishCustomerDeleteDA();
        }

      [CustomSessionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }

      [CustomSessionFilterAttributeForAction]
      public JsonResult DishCustomerDelete(BillCollectionVM _obj)
      {
          try
          {
              _obj.createdBy = Convert.ToInt32(Session["userId"]);
              string PageName = "DishCustomerDeleteController";
              int status = _objDishCustomerDeleteDA.CheckPassword(_obj, PageName);
              if (status > 0)
              {
                  string result = _objDishCustomerDeleteDA.DishCustomerDelete(_obj);
                  return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
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