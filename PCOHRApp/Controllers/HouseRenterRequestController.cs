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
    public class HouseRenterRequestController : Controller
    {
        private readonly HouseRenterRequestDA _houseRenterRequestDA;
        public HouseRenterRequestController()
        {
            _houseRenterRequestDA = new HouseRenterRequestDA();
        }
        // GET: HouseRenter
        [CustomSessionFilter]
        public ActionResult Index()
        {
            return View();
        }
        [CustomSessionFilterAttributeForAction]
        [HttpPost]
        public JsonResult InsertRequest(HouseRenterRequestReqVM _obj)
        {
            try
            {
                int createdBy = Convert.ToInt32(Session["userId"]);
                int result = _houseRenterRequestDA.InsertRequest(_obj,createdBy);
                return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult GetRequestList()
        {
            try
            {
                List<HouseRenterRequestInfoVM> _objList = _houseRenterRequestDA.GetRequestList().OrderByDescending(x => x.requestId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.houseName.ToLower().Contains(searchValue.ToLower())
                        || x.renterName.ToLower().Contains(searchValue.ToLower())
                        || x.renterPhone.ToLower().Contains(searchValue.ToLower())
                        || x.requestYearName.ToLower().Contains(searchValue.ToLower())
                        || x.requestName.ToLower().Contains(searchValue.ToLower())
                        || x.requestMonth.ToLower().Contains(searchValue.ToLower())
                        || x.renterNID.ToLower().Contains(searchValue.ToLower())
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
        public JsonResult DeleteRequest(int id)
        {
            try
            {
                int createdBy = Convert.ToInt32(Session["userId"]);
                int result = _houseRenterRequestDA.DeleteRequest(id, createdBy);
                return Json(new { success = true, message = "Request Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}