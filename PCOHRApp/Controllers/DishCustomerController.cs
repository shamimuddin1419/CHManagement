using PCOHRApp.DA;
using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using PCOHRApp.Utility;

namespace PCOHRApp.Controllers
{
    public class DishCustomerController : Controller
    {
        private DishCustomerDA _dishCustomerDA; 
        public DishCustomerController()
        {
            _dishCustomerDA = new DishCustomerDA();
        }
        [CustomSessionFilterAttribute]
        // GET: DishCustomer
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
                if (_obj.EntryDateString != "" && _obj.EntryDateString!=null)
                {
                    _obj.EntryDate = DateTime.ParseExact(_obj.EntryDateString, "dd/MM/yyyy", null);
                }
                string result = _dishCustomerDA.InsertOrUpdateCustomer(_obj);
                return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY") && ex.Message.Contains("IX_tblDishCustomers_cid_sid"))
                {
                    return Json(new { success = false, message = "Customer with this serial already exists!!" }, JsonRequestBehavior.AllowGet);
                }
                else if (ex.Message.Contains("Violation of UNIQUE KEY") && ex.Message.Contains("IX_tblDishCustomers_phoneNumber"))
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
                List<CustomerVM> _objList = _dishCustomerDA.GetCustomerList().ToList();
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
                var _obj = _dishCustomerDA.GetCustomerById(id);
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
                var _obj = _dishCustomerDA.GetCustomerList().Where(x=>x.customerAddress.ToLower().Contains(search.ToLower())).Distinct()
                    .Select(x=>x.customerAddress).Distinct().ToList();
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
                CustomerVM _obj = _dishCustomerDA.GetCustomerByCustomeridAndSerialprefix(id, serialPrefixId) ?? new CustomerVM();
                if (_obj.customerSerialId == 0)
                {
                    return Json(new { success = true,isDuplicate = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true,isDuplicate = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerListForDropdown(string search, int page, int selectedId,string searchBy = "")
        {
            try
            {
                List<DropdownVM> _objListAll = new List<DropdownVM>();
                if (searchBy.ToLower() == "card")
                {
                    _objListAll = _dishCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "") || x.customerId.ToLower().StartsWith(search.ToLower())
                    || x.customerSerial.ToLower().StartsWith(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = x.customerSerial + "#" + x.customerName + "#" + x.customerPhone,
                    }).ToList();
                }
                else if (searchBy.ToLower() == "name")
                {
                    _objListAll = _dishCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "") 
                    || x.customerName.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = x.customerSerial + "#" + x.customerName + "#" + x.customerPhone,
                    }).ToList();
                }
                else if (searchBy.ToLower() == "mobile")
                {
                    _objListAll = _dishCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "") 
                    || x.customerPhone.ToLower().Contains(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.id,
                        text = x.customerSerial + "#" + x.customerName + "#" + x.customerPhone,
                    }).ToList();
                }
                else
                {
                    _objListAll = _dishCustomerDA.GetCustomerList().Where(x => x.isActive && ((search == null || search == "") || x.customerId.ToLower().Contains(search.ToLower())
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

    }
}