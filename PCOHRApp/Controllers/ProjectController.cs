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
    public class ProjectController : Controller
    {
        private ProjectDA _da;
        public ProjectController()
        {
            _da = new ProjectDA();
        }
        // GET: Project
        [CustomSessionFilter]
        public ActionResult Index()
        {
            return View();
        }
        //[CustomSessionFilterAttributeForAction]
        [HttpPost]
        public JsonResult InsertOrUpdateProject(ProjectVM _obj)
        {
            try
            {
                _obj.createdBy = Convert.ToInt32(Session["userId"]);
                
                int result = _da.InsertOrUpdateProject(_obj);
                if (_obj.projectId == 0)
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
                    return Json(new { success = false, message = "Project with this name already Exists!!" }, JsonRequestBehavior.AllowGet);
                }
                if (ex.Message.Contains(@"Violation of PRIMARY KEY constraint 'PK_tblProjectCareTaker'"))
                {
                    return Json(new { success = false, message = "Duplicate CareTaker cannot be added!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult GetProjectList()
        {
            try
            {
                List<ProjectVM> _objList = _da.GetProjectList().OrderByDescending(x => x.projectId).ToList();
                int totalRows = _objList.Count;
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];

                if (!string.IsNullOrEmpty(searchValue))
                {
                    _objList = _objList.Where(x => x.projectName.ToLower().Contains(searchValue.ToLower())
                        || x.projectType.ToLower().Contains(searchValue.ToLower())
                        || x.projectAddress.ToLower().Contains(searchValue.ToLower())
                        || x.apartmentBuildingType.ToLower().Contains(searchValue.ToLower())).ToList();
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
        public JsonResult GetProjectById(int id)
        {
            try
            {
                var _obj = _da.GetProjectById(id);
                return Json(new { success = true, data = _obj }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetProjectListForDropdown()
        {
            try
            {
                var _objList = _da.GetProjectList().OrderByDescending(x => x.projectId).Select(x => new
                {
                    id = x.projectId,
                    text = x.projectName,
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