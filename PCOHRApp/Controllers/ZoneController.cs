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
    public class ZoneController : Controller
    {
        private ZoneDA _zoneDA;
        public ZoneController()
        {
            _zoneDA = new ZoneDA();
        }
        // GET: Zone
        [CustomSessionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertOrUpdateZone(ZoneVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _zoneDA.InsertOrUpdateZone(_obj);
                if (_obj.zoneId == 0)
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
                    return Json(new { success = false, message = "Zone with this name Already Exists!!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetZoneList()
        {
            try
            {
                var _objList = _zoneDA.GetZoneList().OrderByDescending(x => x.zoneId).Select(x => new
                {
                    zoneId = x.zoneId,
                    zoneName = x.zoneName,
                    isActive = x.isActive == true ? "Yes" : "No"
                }).ToList();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetZoneListForDropdown()
        {
            try
            {
                var _objList = _zoneDA.GetZoneList().Where(x=>x.isActive).OrderByDescending(x => x.zoneId).Select(x => new
                {
                    id = x.zoneId,
                    text = x.zoneName,
                }).ToList();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetZoneById(int id)
        {
            try
            {
                var _obj = _zoneDA.GetZoneById(id);
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}