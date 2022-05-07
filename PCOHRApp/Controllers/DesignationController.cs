using PCOHRApp.DA;
using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class DesignationController : Controller
    {
        private DesignationDA _designationDA;
        public DesignationController()
        {
            _designationDA = new DesignationDA();
        }
        // GET: Designation
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDesignationList()
        {
            try
            {
                var _objList = _designationDA.GetDesignationList().Select(x => new
                {
                    id = x.designationId,
                    text = x.designationName
                }).ToList();
               return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}