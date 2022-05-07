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
    public class HostController : Controller
    {
        private HostDA _hostDA; 
        public HostController()
        {
            _hostDA = new HostDA();
        }
        // GET: Host
        [CustomSessionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertOrUpdateHost(HostVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _hostDA.InsertOrUpdateHost(_obj);
                if (_obj.hostId == 0)
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
                if (ex.Message.Contains("Violation of UNIQUE KEY"))
                {
                    return Json(new { success = false, message = "Mobile Number Already Exists!!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult GetHostList()
        {
            try
            {
                var _objList = _hostDA.GetHostList().OrderByDescending(x => x.hostId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.hostName.ToLower().Contains(searchValue.ToLower())
                        || x.hostPhone.ToLower().Contains(searchValue.ToLower())
                        || x.isActiveString.ToLower().Contains(searchValue.ToLower())
                        || x.hostAddress.ToLower().Contains(searchValue.ToLower())).ToList();
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


        public JsonResult GetHostPhoneListForDropdown(string search, int page,int selectedId)
        {
            try
            {
                var _objListAll = _hostDA.GetHostList().Where(x => x.isActive && ((search == null || search == "") || x.hostPhone.ToLower().Contains(search.ToLower()))).OrderByDescending(x => x.hostId).Select(x => new
                {
                    id = x.hostId,
                    text = x.hostPhone,
                }).ToList();
                
                if (_objListAll.Count > page * 10)
                {
                    var _objList = _objListAll.Skip((page - 1) * 10).Take(page * 10).ToList();
                    if (selectedId != 0)
                    {
                        if (!_objList.Where(x=>x.id == selectedId).Any())
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
        public JsonResult GetHostById(int id)
        {
            try
            {
                var _obj = _hostDA.GetHostById(id);
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetHostListForDropdown(string search, int page, int selectedId)
        {
            try
            {
                var _objListAll = _hostDA.GetHostList().Where(x => x.isActive && ((search == null || search == "") || x.hostName.ToLower().Contains(search.ToLower()))).OrderByDescending(x => x.hostId).Select(x => new
                {
                    id = x.hostId,
                    text = x.hostName + " # " + x.hostPhone,
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