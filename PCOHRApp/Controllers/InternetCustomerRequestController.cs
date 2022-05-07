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
    public class InternetCustomerRequestController : Controller
    {
        private InternetCustomerRequestDA _internetCustomerRequestDA;
        public InternetCustomerRequestController()
        {
            _internetCustomerRequestDA = new InternetCustomerRequestDA();
        }
        [CustomSessionFilterAttribute]
        // GET: InternetCustomerRequest
        public ActionResult Index()
        {
            return View();
        }

        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertCustomerRequest(CustomerRequestVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _internetCustomerRequestDA.InsertCustomerRequest(_obj);
                return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult GetCustomerRequestList()
        {
            try
            {
                List<CustomerRequestVM> _objList = _internetCustomerRequestDA.GetCustomerRequestList().OrderByDescending(x => x.requestId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.customerSerial.ToLower().Contains(searchValue.ToLower())
                        || x.customerName.ToLower().Contains(searchValue.ToLower())
                        || x.customerPhone.ToLower().Contains(searchValue.ToLower())
                        || x.customerSerial.ToLower().Contains(searchValue.ToLower())
                        || x.requestName.ToLower().Contains(searchValue.ToLower())
                        || x.requestMonth.ToLower().Contains(searchValue.ToLower())
                        || x.requestYearName.ToLower().Contains(searchValue.ToLower())).ToList();
                }
                //_objList = _objList.OrderBy(sortColumnName + " " + sortDirection).ToList<CustomerVM>();
                _objList = _objList.Skip(start).Take(length).ToList();

                return Json(new { success = true, data = _objList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRows }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult DeleteCustomerRequest(int id)
        {
            try
            {
                int createdBy = Convert.ToInt32(Session["userId"]);
                int result = _internetCustomerRequestDA.DeleteCustomerRequest(id, createdBy);
                return Json(new { success = true, message = "Request Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}