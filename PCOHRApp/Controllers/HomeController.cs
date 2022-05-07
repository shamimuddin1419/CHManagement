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

    public class HomeController : Controller
    {
        private UserDA _userDA;
        private DashBoardDA _dashboardDA;
        public HomeController()
        {
            _userDA = new UserDA();
            _dashboardDA = new DashBoardDA();
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult GetDishDashboarddata()
        {
            try
            {
                DashBoardDataVM _obj = _dashboardDA.GetDishDashboarddata() ?? new DashBoardDataVM(); 
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public ActionResult GetInternetDashboarddata()
        {
            try
            {
                DashBoardDataVM _obj = _dashboardDA.GetInternetDashboarddata() ?? new DashBoardDataVM();
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult PermissionFailed()
        {
            return View();
        }
    }
}