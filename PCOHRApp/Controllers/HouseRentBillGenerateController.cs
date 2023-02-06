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
    public class HouseRentBillGenerateController : Controller
    {
        private readonly RenterDA _renterDA;
        private readonly HouseBillGenerateDA _billGenerateDA;
        public HouseRentBillGenerateController()
        {
            _renterDA = new RenterDA();
            _billGenerateDA = new HouseBillGenerateDA();
        }
        // GET: HouseRentBillGenerate
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCurrentHouseRenterByHouse(int houseId,int effectiveMonth,int effectiveYear)
        {
            try
            {
                CurrnetHouseRenterVM result = _renterDA.GetCurrentHouseRenter(houseId, effectiveMonth, effectiveYear);
                if (result is null)
                {
                    return Json(new { success = false, message = "No Renter Found" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertBillGenerate(HouseBillGenerateVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _billGenerateDA.InsertBillGenerate(_obj);
                return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [CustomSessionFilterAttributeForAction]
        public JsonResult GetBillGenerateList(int month, int year)
        {
            try
            {
                List<HouseBillGenerateVM> _objList = _billGenerateDA.GetBillList(month, year)
                                                                    .OrderByDescending(x => x.billDetailId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.houseType.ToLower().Contains(searchValue.ToLower())
                        || x.houseName.ToLower().Contains(searchValue.ToLower())
                        || x.renterName.ToLower().Contains(searchValue.ToLower())
                        || x.renterPhone.ToLower().Contains(searchValue.ToLower())
                        || x.monthName.ToLower().Contains(searchValue.ToLower())
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
                int result = _billGenerateDA.DeleteBill(id, createdBy);
                return Json(new { success = true, message = "Bill Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [CustomSessionFilterAttributeForAction]
        public ViewResult UpdateBill()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUnupdatedBillForDropDown(int houseId)
        {
            try
            {
                List<DropdownVM> dropdownData = _billGenerateDA.GetUnUpdatedHouseBills(houseId)
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

        [HttpGet]
        public JsonResult GetHouseBillInformation(int billDetailId)
        {
            try
            {
                HouseBillInformationVM result = _billGenerateDA.GetHouseBillInformation(billDetailId);
                return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [CustomSessionFilterAttributeForAction]
        public JsonResult UpdateBillInfo(HouseBillInformationVM _obj)
        {
            try
            {
                int createdBy = Convert.ToInt32(Session["userId"]);
                int result = _billGenerateDA.UpdateGeneratedBill(_obj,createdBy);
                return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [CustomSessionFilterAttributeForAction]

        public JsonResult GetUpdatedHouseBillInformation(int year,int month)
        {
            try
            {
                List<HouseBillInformationVM> _objList = _billGenerateDA.GetUpdatedHouseBillInformation(year,month)
                                                                     .OrderByDescending(x => x.billDetailId).ToList();

                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.houseType.ToLower().Contains(searchValue.ToLower())
                        || x.houseName.ToLower().Contains(searchValue.ToLower())
                        || x.renterName.ToLower().Contains(searchValue.ToLower())
                        || x.unPaidMonth.ToLower().Contains(searchValue.ToLower())
                        ).ToList();
                }
                //_objList = _objList.OrderBy(sortColumnName + " " + sortDirection).ToList<CustomerVM>();
                _objList = _objList.Skip(start).Take(length).ToList();

                return Json(new { success = true, data = _objList, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomSessionFilterAttributeForAction]
        public JsonResult DeleteUpdatedBill(int id)
        {
            try
            {
                int createdBy = Convert.ToInt32(Session["userId"]);
                int result = _billGenerateDA.DeleteUpdatedHouseBill(id, createdBy);
                return Json(new { success = true, message = "Bill Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}