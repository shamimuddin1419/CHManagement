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
    public class InternetCustomerController : Controller
    {
        private InternetCustomerDA _internetCustomerDA;
        public InternetCustomerController()
        {
            _internetCustomerDA = new InternetCustomerDA();
        }
        // GET: InternetCustomer
        [CustomSessionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertOrUpdateCustomer(CustomerVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                if (_obj.EntryDateString != "" && _obj.EntryDateString != null)
                {
                    _obj.EntryDate = DateTime.ParseExact(_obj.EntryDateString, "dd/MM/yyyy", null);
                }
                string result = _internetCustomerDA.InsertOrUpdateCustomer(_obj);
                return Json(new { success = true, message = "Data Saved Serial Number is " + result }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY") && ex.Message.Contains("IX_tblInternetCustomers_cid_sid"))
                {
                    return Json(new { success = false, message = "Customer with this serial already exists!!" }, JsonRequestBehavior.AllowGet);
                }
                else if (ex.Message.Contains("Violation of UNIQUE KEY") && ex.Message.Contains("IX_tblInternetCustomers_phoneNumber"))
	            {
                    return Json(new { success = false, message = "Phone Number Already Exists!!" }, JsonRequestBehavior.AllowGet);
	            }
                else if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    return Json(new { success = false, message = "Customer with this serial already exists!!" }, JsonRequestBehavior.AllowGet);
                }
               
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult GetCustomerList()
        {
            try
            {
                List<CustomerVM> _objList = _internetCustomerDA.GetCustomerList().ToList();
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
                        || x.hostName.ToLower().Contains(searchValue.ToLower())
                        || x.zoneName.ToLower().Contains(searchValue.ToLower())
                        || x.assignedUserName.ToLower().Contains(searchValue.ToLower())).ToList();
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

        public JsonResult GetCustomerById(int id)
        {
            try
            {
                var _obj = _internetCustomerDA.GetCustomerById(id);
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCustomerAddresses(string search)
        {
            try
            {
                var _obj = _internetCustomerDA.GetCustomerList().Where(x => x.customerAddress.ToLower().Contains(search.ToLower())).Distinct()
                    .Select(x => x.customerAddress).Distinct().ToList();
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDuplicateByIdAndSerialPrefix(string id, int serialPrefixId)
        {
            if (serialPrefixId != 0)
            {
                CustomerVM _obj = _internetCustomerDA.GetCustomerByCustomeridAndSerialprefix(id, serialPrefixId) ?? new CustomerVM();
                if (_obj.customerSerialId == 0)
                {
                    return Json(new { success = true, isDuplicate = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, isDuplicate = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        private string GenerateTextForCustomerListForDropdown(CustomerVM obj)
        {
            return obj.customerSerial
                + "#" + obj.customerName
                + "#" + obj.customerPhone
                + "#" + obj.nid
                + "#" + obj.onuMCId
                + "#" + obj.remarks;
        }
        public JsonResult GetCustomerListForDropdown(string search, int page, int selectedId, string searchBy = "")
        {
            try
            {
                List<DropdownVM> _objListAll = new List<DropdownVM>();
                if (searchBy.ToLower() == "card")
                {
                    _objListAll = _internetCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "") || x.customerId.ToLower().StartsWith(search.ToLower())
                    || x.customerSerial.ToLower().StartsWith(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = GenerateTextForCustomerListForDropdown(x)
                    }).ToList();
                }
                else if (searchBy.ToLower() == "name")
                {
                    _objListAll = _internetCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "")
                    || x.customerName.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = GenerateTextForCustomerListForDropdown(x)
                    }).ToList();
                }
                else if (searchBy.ToLower() == "mobile")
                {
                    _objListAll = _internetCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "")
                    || x.customerPhone.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = GenerateTextForCustomerListForDropdown(x)
                    }).ToList();
                }
                else if (searchBy.ToLower() == "nid")
                {
                    _objListAll = _internetCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "")
                    || x.nid.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = GenerateTextForCustomerListForDropdown(x)
                    }).ToList();
                }
                else if (searchBy.ToLower() == "remarks")
                {
                    _objListAll = _internetCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "")
                    || x.remarks.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = GenerateTextForCustomerListForDropdown(x)
                    }).ToList();
                }
                else if (searchBy.ToLower() == "onumc")
                {
                    _objListAll = _internetCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "")
                    || x.onuMCId.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = GenerateTextForCustomerListForDropdown(x)
                    }).ToList();
                }
                else
                {
                    _objListAll = _internetCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "") || x.customerId.ToLower().Contains(search.ToLower())
                    || x.customerSerial.ToLower().Contains(search.ToLower())
                    || x.customerName.ToLower().Contains(search.ToLower())
                    || x.customerPhone.ToLower().Contains(search.ToLower()))).OrderByDescending(x => x.hostId).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = GenerateTextForCustomerListForDropdown(x)
                    }).ToList();
                }

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
        [CustomSessionFilter]
        public ActionResult CustomerCardInfo()
        {
            return View();
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertOrUpdateCustomerCardInfo(CustomerCardInfoVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                string result = _internetCustomerDA.InsertOrUpdateCustomerCardInfo(_obj);
                return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    return Json(new { success = false, message = "This customer card already exists!!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetCustomerCardInfoList()
        {
            try
            {
                List<CustomerCardInfoVM> _objList = _internetCustomerDA.GetCustomerCardInfoList().ToList();
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
                        || x.ownerName.ToLower().Contains(searchValue.ToLower())
                        || x.ownerPhone.ToLower().Contains(searchValue.ToLower())
                        || x.customerAddress.ToLower().Contains(searchValue.ToLower())).ToList();
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
        public JsonResult GetCustomerCardInfoListForDropdown(string search, int page, int selectedId, string searchBy = "")
        {
            try
            {
                List<DropdownVM> _objListAll = new List<DropdownVM>();
                if (searchBy.ToLower() == "card")
                {
                    _objListAll = _internetCustomerDA.GetCustomerCardInfoListForDropdown().Where(x => ((search == null || search == "") || x.customerId.ToLower().StartsWith(search.ToLower())
                    || x.customerSerial.ToLower().StartsWith(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = x.customerSerial + "#" + x.customerName + "#" + x.customerPhone,
                    }).ToList();
                }
                else if (searchBy.ToLower() == "name")
                {
                    _objListAll = _internetCustomerDA.GetCustomerCardInfoListForDropdown().Where(x => ((search == null || search == "")
                    || x.customerName.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = x.customerSerial + "#" + x.customerName + "#" + x.customerPhone,
                    }).ToList();
                }
                else if (searchBy.ToLower() == "mobile")
                {
                    _objListAll = _internetCustomerDA.GetCustomerCardInfoListForDropdown().Where(x => ((search == null || search == "")
                    || x.customerPhone.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = x.customerSerial + "#" + x.customerName + "#" + x.customerPhone,
                    }).ToList();
                }
                else
                {
                    _objListAll = _internetCustomerDA.GetCustomerCardInfoListForDropdown().Where(x => ((search == null || search == "") || x.customerId.ToLower().Contains(search.ToLower())
                    || x.customerSerial.ToLower().Contains(search.ToLower())
                    || x.customerName.ToLower().Contains(search.ToLower())
                    || x.customerPhone.ToLower().Contains(search.ToLower()))).OrderByDescending(x => x.hostId).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = x.customerSerial + "#" + x.customerName + "#" + x.customerPhone,
                    }).ToList();
                }

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
        public JsonResult GetCustomerCardInfoById(int id)
        {
            try
            {
                var _obj = _internetCustomerDA.GetCustomerCardInfoById(id);
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}