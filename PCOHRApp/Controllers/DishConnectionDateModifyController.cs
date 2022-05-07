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
    public class DishConnectionDateModifyController : Controller
    {
        private DishConnectionDateModifyDA _objDishConnectionDateModifyDA = null;
        public DishConnectionDateModifyController()
        {
            _objDishConnectionDateModifyDA = new DishConnectionDateModifyDA();
        }
        //
        // GET: /DishConnectionDateModify/
        public ActionResult Index()
        {
            return View();
        }

        [CustomSessionFilterAttributeForAction]
        public JsonResult DishConnectionDateUpdate(CustomerVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                string PageName = "DishConnectionDateModifyController";
                int status = _objDishConnectionDateModifyDA.CheckPassword(_obj, PageName);
                if (status > 0)
                {
                    int result = _objDishConnectionDateModifyDA.ConnectionDateUpdate(_obj);
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