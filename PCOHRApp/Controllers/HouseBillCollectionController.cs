using PCOHRApp.DA;
using PCOHRApp.Models;
using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class HouseBillCollectionController : Controller
    {
        private readonly HouseBillCollectionDA _houseBillCollectionDA;

        public HouseBillCollectionController()
        {
            _houseBillCollectionDA = new HouseBillCollectionDA();
        }
        // GET: HouseBillCollection
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUnPaidBillForDropDown(int houseId)
        {
            try
            {
                List<DropdownVM> dropdownData = _houseBillCollectionDA.GetUnPaidHouseBills(houseId)
                                                                .Select(x => new DropdownVM
                                                                {
                                                                    id = x.billDetailId,
                                                                    text = x.unPaidMonth
                                                                }).ToList();

                return Json(new { success = true, data = dropdownData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertBillCollection(HouseRenterBillCollectionVM _obj)
        {
            try
            {
                int createdBy = Convert.ToInt32(Session["userId"]);
                _obj.collectionDate =  DateTime.ParseExact(_obj.collectedDateString, "dd/MM/yyyy", null);
                
                string result = _houseBillCollectionDA.InsertBillCollection(_obj,createdBy);
                return Json(new { success = true, message = "Bill collected successfully with reference number : " + result }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [CustomSessionFilterAttributeForAction]
        public JsonResult GetBillCollectionList()
        {
            try
            {
                List<HouseRenterBillCollectionVM> _objList = _houseBillCollectionDA.GetCollectionList()
                                                                    .OrderByDescending(x => x.collectionId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.billMonth.ToLower().Contains(searchValue.ToLower())
                        || x.billMonth.ToLower().Contains(searchValue.ToLower())
                        || x.renterName.ToLower().Contains(searchValue.ToLower())
                        || x.houseName.ToLower().Contains(searchValue.ToLower())
                        ).ToList();
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
                int result = _houseBillCollectionDA.DeleteCollection(id, createdBy);
                return Json(new { success = true, message = "Bill Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetBillByBillId(int id)
        {
            try
            {
                HouseRenterBillCollectionVM bill = _houseBillCollectionDA.GetHouseBillByBillId(id);
                return Json(new { success = true, data = bill }, JsonRequestBehavior.AllowGet); ;
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}