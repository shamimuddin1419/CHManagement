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
    public class LordInfoController : Controller
    {
         private LordInfoDA _lordInfoDA; 
        public LordInfoController()
        {
            _lordInfoDA = new LordInfoDA();
        }
        // GET: LordInfo
      //  [CustomSessionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }
       // [CustomSessionFilterAttributeForAction]
        public JsonResult InsertOrUpdateLordInfo(LordInfoVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _lordInfoDA.InsertOrUpdateLordInfo(_obj);
                if (_obj.lordId == 0)
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
        public JsonResult GetLordInfoList()
        {
            try
            {
                var _objList = _lordInfoDA.GetLordInfoList().OrderByDescending(x => x.lordId).ToList();
                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetLordInfoById(int id)
        {
            try
            {
                var _obj = _lordInfoDA.GetLordInfoById(id);
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult GetLordInfoListForDropdown(string search, int page, int selectedId)
        //{
        //    try
        //    {
        //        var _objListAll = _lordInfoDA.GetLordInfoList().Where(x => x.isActive && ((search == null || search == "") || x.lordInfoName.ToLower().Contains(search.ToLower()))).OrderByDescending(x => x.lordInfoId).Select(x => new
        //        {
        //            id = x.lordInfoId,
        //            text = x.lordInfoName + " # " + x.lordInfoPhone,
        //        }).ToList();

        //        if (_objListAll.Count > page * 10)
        //        {
        //            var _objList = _objListAll.Skip((page - 1) * 10).Take(page * 10).ToList();
        //            if (selectedId != 0)
        //            {
        //                if (!_objList.Where(x => x.id == selectedId).Any())
        //                {
        //                    var selectedItem = _objListAll.Where(x => x.id == selectedId).FirstOrDefault();
        //                    _objList.Add(selectedItem);
        //                }
        //            }
        //            return Json(new { success = true, results = _objList, pagination = new { more = true } }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {

        //            return Json(new { success = true, results = _objListAll, pagination = new { more = false } }, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}