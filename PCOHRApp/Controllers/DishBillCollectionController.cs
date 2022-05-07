using PCOHRApp.DA;
using PCOHRApp.Models;
using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class DishBillCollectionController : Controller
    {
        private DishBillCollectionDA _dishBillCollectionDA;
        public DishBillCollectionController()
        {
            _dishBillCollectionDA = new DishBillCollectionDA();
        }
        // GET: DishBillCollection
        [CustomSessionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult UpdateCollection()
        {
            return View();
        }

        [CustomSessionFilterAttributeForAction]
        public ActionResult DishBillCollectionModified()
        {
            return View();
        }

        public JsonResult GetBillUnCollectionList(string fromDate, string toDate, int receivedBy)
        {
            try
            {
                List<BillCollectionVM> _objList = _dishBillCollectionDA.GetBillUnCollectionList(DateTime.ParseExact(fromDate, "dd/MM/yyyy", null), DateTime.ParseExact(toDate, "dd/MM/yyyy", null), receivedBy) ?? new List<BillCollectionVM>();
                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult UpdateCollectionStatus(List<BillCollectionVM> objs, int userId)
        {
            try
            {
                if (objs.Any())
                {
                    int result = _dishBillCollectionDA.UpdateCollectionStatus(objs, userId);
                    return Json(new { success = true, message = "Collection Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No Collection Selected!!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPreviousInfoList(int id)
        {
            try
            {
                List<PreviousBillInfoVM> _objList = _dishBillCollectionDA.GetPreviousInfoList(id) ?? new List<PreviousBillInfoVM>();
                var monthList = _objList.Where(x => x.collectionType == "Monthly Bill")
                                .Select(x => new { billDetailId = x.billDetailId ?? 0, month = x.transactionMonth, year = Convert.ToInt16(x.yearName), Sort = DateTime.ParseExact(x.transactionMonth, "MMMM", CultureInfo.InvariantCulture) })
                                .OrderBy(x => x.year).ThenBy(x => x.Sort.Month)
                                .Select(x => new
                                {
                                    id = x.billDetailId,
                                    text = x.month + "-" + x.year.ToString()
                                }).ToList();
                var payedBill = _dishBillCollectionDA.GetLastPaidBill(id);
                return Json(new { success = true, data = _objList, data1 = monthList, data2 = payedBill }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertBillCollection(BillCollectionVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                _obj.collectionDate = DateTime.ParseExact(_obj.collectedDateString, "dd/MM/yyyy", null);
                string result = _dishBillCollectionDA.InsertBillCollection(_obj);
                return Json(new { success = true, message = "Data Saved. Reference Number is: " + result }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    return Json(new { success = false, message = "Dupliacte Page/Serial No" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetBillCollectionList()
        {
            try
            {
                List<BillCollectionVM> _objList = _dishBillCollectionDA.GetBillCollectionList().OrderByDescending(x => x.collectionId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.customerSerial.ToLower().Contains(searchValue.ToLower())
                        || x.customerSerial.ToLower().Contains(searchValue.ToLower())
                        || x.customerName.ToLower().Contains(searchValue.ToLower())
                        || x.fromMonthYear.ToLower().Contains(searchValue.ToLower())
                        || x.toMonthYear.ToLower().Contains(searchValue.ToLower())
                        || x.voucherNo.ToLower().Contains(searchValue.ToLower())).ToList();
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
        public JsonResult DeleteCollection(int id)
        {
            try
            {
                int createdBy = Convert.ToInt32(Session["userId"]);
                int result = _dishBillCollectionDA.DeleteCollection(id, createdBy);
                return Json(new { success = true, message = "Collection Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMonthlyBill(int cid, string fromMonthYear, string toMonthYear)
        {
            try
            {
                DateTime? fromMonthYearDate;
                DateTime? toMonthYearDate;
                if (fromMonthYear != "0" && toMonthYear != "0")
                {
                    fromMonthYearDate = Convert.ToDateTime("01-" + fromMonthYear);
                    toMonthYearDate = Convert.ToDateTime("01-" + toMonthYear);
                    if (fromMonthYearDate > toMonthYearDate)
                    {
                        return Json(new { success = false, message = "To Month cannot be greater than From Month" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        decimal result = _dishBillCollectionDA.GetMonthlyBill(cid, fromMonthYearDate, toMonthYearDate);
                        return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = true, data = 0 }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
       [HttpPost]
        public JsonResult GetMonthlyBillByDetailIds(BillCollectionVM _obj)
        {
            try
            {
                decimal result = _dishBillCollectionDA.GetMonthlyBill(_obj);
                return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public JsonResult GetPagesByYear(int yearId, int receivedBy)
       {
           try
           {
               List<BillCollectionVM> _objList = _dishBillCollectionDA.GetPagesByYear(yearId, receivedBy) ?? new List<BillCollectionVM>();
               var pageList = _objList
                               .Select(x => new
                               {
                                   id = x.pageNo,
                                   text = x.pageNo
                               }).ToList();
               return Json(new { success = true, data = pageList }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception ex)
           {
               
               return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
           }
       }
       public JsonResult GetPages(int yearId)
       {
           try
           {
               List<BillCollectionVM> _objList = _dishBillCollectionDA.GetPagesByYear(yearId) ?? new List<BillCollectionVM>();
               var pageList = _objList
                               .Select(x => new
                               {
                                   id = x.pageNo,
                                   text = x.pageNo
                               }).ToList();
               return Json(new { success = true, data = pageList }, JsonRequestBehavior.AllowGet);
           }
           catch (Exception ex)
           {

               return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
           }
       }

       [HttpPost]
       public JsonResult GetBillCollectionListBySerial(string pageNo, string serialNo, int yearID)
       {
           try
           {
               List<BillCollectionVM> _objList = _dishBillCollectionDA.GetBillCollectionListBySerial(pageNo, serialNo, yearID).OrderByDescending(x => x.collectionId).ToList();
               int totalRows = _objList.Count;
               int start = Convert.ToInt32(Request["start"]);
               int length = Convert.ToInt32(Request["length"]);
               string searchValue = Request["search[value]"];
               string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
               string sortDirection = Request["order[0][dir]"];

               if (!string.IsNullOrEmpty(searchValue))
               {
                   _objList = _objList.Where(x => x.customerSerial.ToLower().Contains(searchValue.ToLower())
                       || x.customerSerial.ToLower().Contains(searchValue.ToLower())
                       || x.customerName.ToLower().Contains(searchValue.ToLower())
                       || x.fromMonthYear.ToLower().Contains(searchValue.ToLower())
                       || x.toMonthYear.ToLower().Contains(searchValue.ToLower())
                       || x.voucherNo.ToLower().Contains(searchValue.ToLower())).ToList();
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