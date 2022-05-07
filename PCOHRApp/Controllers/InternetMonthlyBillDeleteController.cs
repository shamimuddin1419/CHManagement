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
    public class InternetMonthlyBillDeleteController : Controller
    {
        private InternetMonthlyBillDeleteDA _objInternetMonthlyBillDeleteDA = null;

        public InternetMonthlyBillDeleteController()
        {
            _objInternetMonthlyBillDeleteDA = new InternetMonthlyBillDeleteDA();
        }
        [CustomSessionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [CustomSessionFilterAttributeForAction]
        public JsonResult InternetMonthlyBillDelete(BillCollectionVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                string PageName = "InternetMonthlyBillDeleteController";
                int status = _objInternetMonthlyBillDeleteDA.CheckPassword(_obj, PageName);
                if (status > 0)
                {
                    string result = _objInternetMonthlyBillDeleteDA.InternetMonthlyBillDelete(_obj);
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