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
    public class InternetConnectionDateModifyController : Controller
    {
        private InternetConnectionDateModifyDA _objInternetConnectionDateModifyDA = null;
        public InternetConnectionDateModifyController()
        {
            _objInternetConnectionDateModifyDA = new InternetConnectionDateModifyDA();
        }
        //
        // GET: /InternetConnectionDateModify/
        public ActionResult Index()
        {
            return View();
        }

        [CustomSessionFilterAttributeForAction]
        public JsonResult InternetConnectionDateUpdate(CustomerVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                string PageName = "InternetConnectionDateModifyController";
                int status = _objInternetConnectionDateModifyDA.CheckPassword(_obj, PageName);
                if (status > 0)
                {
                    int result = _objInternetConnectionDateModifyDA.ConnectionDateUpdate(_obj);
                    return Json(new { success = true, message = "Date Modified successfully." }, JsonRequestBehavior.AllowGet);
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