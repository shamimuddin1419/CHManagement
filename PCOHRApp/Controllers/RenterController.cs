using PCOHRApp.DA;
using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class RenterController : Controller
    {
        private RenterDA  _renterDA;
        public RenterController()
        {
            _renterDA = new RenterDA();
        }
        //
        // GET: /Renter/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRenterListForDropdown(string search, int page, int selectedId, string searchBy = "")
        {
            try
            {
                List<DropdownVM> _objListAll = new List<DropdownVM>();
                _objListAll = _renterDA.GetRenterList().Where(x => x.isActive && ((search == null || search == "") || x.renterId.ToString().ToLower().StartsWith(search.ToLower())
                    || x.renterName.ToLower().StartsWith(search.ToLower())
                    || x.renterNID.ToLower().StartsWith(search.ToLower())
                    || x.renterPhone.ToLower().StartsWith(search.ToLower()))).Select(x => new DropdownVM
                    {
                        id = x.renterId,
                        text = x.renterFullInfo
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