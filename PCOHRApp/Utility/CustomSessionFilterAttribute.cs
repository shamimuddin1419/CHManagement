using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Utility
{
    public class CustomSessionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session["user"] != null)
            {
                string actionName = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string url = controllerName + "/" + actionName;
                List<UserPageVM> userPages = (List<UserPageVM>)session["menuData"];
                if(userPages.Where(x=>x.pageUrl == url).Any())
                {
                    return;
                }
                else
                {
                    if (controllerName == "Home" && actionName == "Index")
                    {
                        return;
                    }
                    else
                    {
                        var uri = new UrlHelper(filterContext.RequestContext);
                        var loginUrl = uri.Content("~/Home/PermissionFailed");
                        filterContext.Result = new RedirectResult(loginUrl);
                        return;
                    }
                }
            }
            else
            {
                var uri = new UrlHelper(filterContext.RequestContext);
                var loginUrl = uri.Content("~/Home/Index");
                filterContext.Result = new RedirectResult(loginUrl);
                return;
            }
        }
    }

    public class CustomSessionFilterAttributeForAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session["user"] != null)
            {
                return;
            }
            else
            {
                var uri = new UrlHelper(filterContext.RequestContext);
                var loginUrl = uri.Content("~/Home/Index");
                filterContext.Result = new RedirectResult(loginUrl);
                return;
            }
        }
    }
}