using PCOHRApp.DA;
using PCOHRApp.Models;
using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{

    public class AccountController : Controller
    {
        private UserDA _userDA; 
        public AccountController()
        {
            _userDA = new UserDA();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ViewResult Registration()
        {
            return View();
        }
        [CustomSessionFilterAttributeForAction]
        public JsonResult InsertOrUpdateUser(UserVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                int result = _userDA.InsertOrUpdateUser(_obj);
                if (_obj.userId == 0)
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
                    return Json(new { success = false, message = "UserName Already Exists!!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetUserList()
        {
            try
            {
                var _objList = _userDA.GetUserList().OrderByDescending(x=>x.userId).Select(x => new
                {
                    userId = x.userId,
                    userFullName = x.userFullName,
                    userName = x.userName,
                    phoneNumber = x.phoneNumber,
                    email = x.email,
                    isAdmin = x.isAdmin == true ? "Yes" : "No",
                    isCableUser = x.isCableUser == true ? "Yes" : "No",
                    isHouseRentUser = x.isHouseRentUser == true ? "Yes" : "No",
                    designationName = x.designationName,
                    isActive = x.isActive == true ? "Yes" : "No"
                }).ToList();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetUserDropdownList()
        {
            try
            {
                var _objList = _userDA.GetUserList().Where(a => a.isActive).Select(x => new
                {
                    id = x.userId,
                    text = x.userName,
                }).ToList();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetUserDropdownListForManagement()
        {
            try
            {
                var _objList = _userDA.GetUserList().Where(a => a.isActive && a.isManagement).Select(x => new
                {
                    id = x.userId,
                    text = x.userName,
                }).ToList();

                return Json(new { success = true, data = _objList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetUserById(int id)
        {
            try
            {
                var _obj = _userDA.GetUserById(id);
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [CustomSessionFilterAttribute]
        public ViewResult UserPageMapping()
        {
            return View();
        }
        public JsonResult GetPageListByUserId(int id)
        {
            try
            {
                var _obj = _userDA.GetPageListByUserId(id).Select(x => new
                {
                    pageId = x.pageId,
                    pageName = x.pageName,
                    pageUrl = x.pageUrl,
                    pageType = x.pageType,
                    isPermitted = x.isPermitted
                }).ToList();
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Login(string userName,string password,string userType)
        {
            try
            {
                UserVM _obj = _userDA.GetUserByUserNameAndPass(userName, password,userType) ?? new UserVM();
                if (_obj.userName!=null)
                {
                    Session["userId"] = _obj.userId;
                    Session["user"] = _obj;
                    Session["menuData"] = _userDA.GetPageListByUserAndType(_obj.userId, _obj.userType);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "UserName or Password/UserType is incorrect!!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UserPageMap(List<UserPageVM> _objs,int userId)
        {
            try 
	        {	        
		        List<int> ids = new List<int>();
                ids = _objs.Select(x => x.pageId).ToList();
                if (ids.Any())
                {
                    int result = _userDA.UserPageMap(ids,userId,Convert.ToInt32(Session["userId"]));
                    return Json(new { success = true, message = "User Page Mapped successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Select Pages to map!!" }, JsonRequestBehavior.AllowGet);
                }
	        }
	        catch (Exception ex)
	        {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
	        }
        }
        public RedirectResult Logout()
        {
            Session.Clear();
            return Redirect("~/Home/Index");
        }


    }
}