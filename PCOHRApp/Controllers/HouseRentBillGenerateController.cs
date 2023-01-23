using PCOHRApp.DA;
using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class HouseRentBillGenerateController : Controller
    {
        private readonly RenterDA _renterDA;
        private readonly RentMonthlyBillGenerateDA _rentMonthlyBillGenerateDA;
        public HouseRentBillGenerateController()
        {
            _renterDA = new RenterDA();
            _rentMonthlyBillGenerateDA = new RentMonthlyBillGenerateDA();
        }
        // GET: HouseRentBillGenerate
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCurrentHouseRenterByHouse(int houseId,int effectiveMonth,int effectiveYear)
        {
            try
            {
                CurrnetHouseRenterVM result = _renterDA.GetCurrentHouseRenter(houseId, effectiveMonth, effectiveYear);
                if (result is null)
                {
                    return Json(new { success = false, message = "No Renter Found" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult InsertBillGenerate(RentMonthlyBillGenerateReqVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _rentMonthlyBillGenerateDA.InsertBillGenerate(_obj);
                return Json(new { success = true, message = "Data Saved" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}