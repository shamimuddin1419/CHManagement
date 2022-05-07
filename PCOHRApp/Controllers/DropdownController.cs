using PCOHRApp.DA;
using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class DropdownController : Controller
    {
        private DropdownDA _dropdownDA;
        public DropdownController()
        {
            _dropdownDA = new DropdownDA();
        }

        public JsonResult GetYearList()
        {
            try
            {
                List<DropdownVM> _objList = _dropdownDA.GetYearList() ?? new List<DropdownVM>();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCustomerSerialList()
        {
            try
            {
                List<DropdownVM> _objList = _dropdownDA.GetCustomerSerialList() ?? new List<DropdownVM>();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCustomerRequestTypeList(string requestTypeGroup)
        {
            try
            {
                List<DropdownVM> _objList = _dropdownDA.GetCustomerRequestTypeList(requestTypeGroup) ?? new List<DropdownVM>();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}