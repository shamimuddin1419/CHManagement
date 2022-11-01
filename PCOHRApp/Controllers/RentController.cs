using PCOHRApp.DA;
using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class RentController : Controller
    {
        private RentDA _rentDA;
        public RentController()
        {
           _rentDA = new RentDA();
        }
        // GET: Rent
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAvailableRentForDropdown(int projectId)
        {
            try
            {
                var _objList = _rentDA.GetAvailableRent(projectId: projectId).OrderByDescending(x => x.houseId).Select(x => new
                {
                    id = x.houseId,
                    text = x.houseName,
                }).ToList();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
      
        public JsonResult GetAvailabilityInfo(int houseId)
        {
            try
            {
                RentVM result = new RentVM();
                result = _rentDA.GetAvailableRent().FirstOrDefault(x=>x.houseId == houseId);
                return Json(new { success = true, results = result, pagination = new { more = true } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult InsertRentHouse(RentVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _rentDA.InsertRentHouse(_obj);
                return Json(new { success = true, message = "Data Saved " }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

    }
}