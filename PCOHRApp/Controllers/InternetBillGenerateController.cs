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
    public class InternetBillGenerateController : Controller
    {
        private InternetBillGenerateDA _internetBillGenerateDA;
        public InternetBillGenerateController()
        {
            _internetBillGenerateDA = new InternetBillGenerateDA();
        }
        [CustomSessionFilterAttribute]
        // GET: BillGenerate
        public ActionResult Index()
        {
            return View();
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertBillGenerate(BillGenerateVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _internetBillGenerateDA.InsertBillGenerate(_obj);
                return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetBillGenerateList(string month, int year)
        {
            try
            {
                List<BillGenerateVM> _objList = _internetBillGenerateDA.GetBillList(month,year).OrderByDescending(x => x.billDetailId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.customerSerial.ToLower().Contains(searchValue.ToLower())
                        || x.month.ToLower().Contains(searchValue.ToLower())
                        || x.yearName.ToLower().Contains(searchValue.ToLower())).ToList();
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
        public JsonResult DeleteBill(int id)
        {
            try
            {
                int createdBy = Convert.ToInt32(Session["userId"]);
                int result = _internetBillGenerateDA.DeleteBill(id, createdBy);
                return Json(new { success = true, message = "Bill Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}