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
    public class CareTakerController : Controller
    {
        private CareTakerDA _da;
        public CareTakerController() 
        {
            _da = new CareTakerDA();
        }
        // GET: CareTaker
       // [CustomSessionFilter]
        public ActionResult Index()
        {
            return View();
        }
       // [CustomSessionFilterAttributeForAction]
        public JsonResult InsertOrUpdateCareTaker(CareTakerVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                if (_obj.joiningDateString != null)
                {
                    _obj.joiningDate = DateTime.ParseExact(_obj.joiningDateString, "dd/MM/yyyy",null);
                }
                int result = _da.InsertOrUpdateCareTaker(_obj);
                if (_obj.careTakerId == 0)
                {
                    return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = "Data Updated" }, JsonRequestBehavior.AllowGet);
                }

            }

            catch (Exception ex)
            {
                //if (ex.Message.Contains("Violation of UNIQUE KEY"))
                //{
                //    return Json(new { success = false, message = "Mobile Number Already Exists!!" }, JsonRequestBehavior.AllowGet);
                //}
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult GetCareTakerList()
        {
            try
            {
                var _objList = _da.GetCareTakerList().OrderByDescending(x => x.careTakerId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.careTakerName.ToLower().Contains(searchValue.ToLower())
                        || x.phoneNo.ToLower().Contains(searchValue.ToLower())
                        || x.email.ToLower().Contains(searchValue.ToLower())
                        || x.nid.ToLower().Contains(searchValue.ToLower())).ToList();
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
        public JsonResult GetCareTakerById(int id)
        {
            try
            {
                var _obj = _da.GetCareTakerById(id);
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCareTakerListForDropdown(string search, int page, int selectedId)
        {
            try
            {
                var _objListAll = _da.GetCareTakerList().Where(x => ((search == null || search == "") || x.careTakerName.ToLower().Contains(search.ToLower()) || x.phoneNo.ToLower().Contains(search.ToLower())) ).OrderByDescending(x => x.careTakerId).Select(x => new
                {
                    id = x.careTakerId,
                    text = x.careTakerName + " # " + x.phoneNo,
                }).ToList();

                if (_objListAll.Count > page * 10)
                {
                    var _objList = _objListAll.Skip((page - 1) * 10).Take(page * 10).ToList();
                    if (selectedId != 0)
                    {
                        if (!_objList.Where(x => x.id == selectedId).Any())
                        {
                            var selectedItem = _objListAll.Where(x => x.id == selectedId).FirstOrDefault();
                            _objList.Add(selectedItem);
                        }
                    }
                    return Json(new { success = true, results = _objList, pagination = new { more = true } }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = true, results = _objListAll, pagination = new { more = false } }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}