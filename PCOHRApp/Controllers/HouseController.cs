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
    public class HouseController : Controller
    {
        HouseDA _da;
        public HouseController()
        {
            _da = new HouseDA();
        }
        // GET: House
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetHousesByProjectId(int id)
        {
            try
            {
                List<HouseVM> _objList = _da.GetHouseListByProjectIdAndId(projectId: id, houseId: 0) ?? new List<HouseVM>();
                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetHousesById(int id)
        {
            try
            {
                HouseVM _obj = _da.GetHouseListByProjectIdAndId(projectId: 0, houseId: id).FirstOrDefault() ?? new HouseVM();
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertOrUpdateHouse(HouseVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);

                int result = _da.InsertOrUpdateHouse(_obj);
                if (_obj.houseId == 0)
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
                    return Json(new { success = false, message = "This house Name Under this Project!!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public JsonResult GetHouseListByProjectForDropdown(int id)
        {
            try
            {
                var _objList = _da.GetHouseListByProjectIdAndId(projectId: id).Select(x => new
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

    }
}