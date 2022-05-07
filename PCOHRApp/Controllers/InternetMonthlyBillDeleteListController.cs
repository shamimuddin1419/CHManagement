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
    public class InternetMonthlyBillDeleteListController : Controller
    {
         private InternetBillCollectionDA _InternetBillCollectionDA;
         public InternetMonthlyBillDeleteListController()
        {
            _InternetBillCollectionDA = new InternetBillCollectionDA();
        }
       
        [CustomSessionFilterAttribute]       
        // GET: /InternetMonthlyBillDeleteList/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetMonthlyBillDeleteList()
        {
            try
            {
                List<BillDelete> _objList = _InternetBillCollectionDA.GetMonthlyBillDeleteList().OrderByDescending(x => x.ID).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.CustomerSerial.ToLower().Contains(searchValue.ToLower())
                        || x.CustomerSerial.ToLower().Contains(searchValue.ToLower())
                        || x.CustomerName.ToLower().Contains(searchValue.ToLower())
                        || x.DeletedBy.ToLower().Contains(searchValue.ToLower())
                        || x.YearName.ToLower().Contains(searchValue.ToLower())
                        || x.Month.ToLower().Contains(searchValue.ToLower())).ToList();
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
	}
}