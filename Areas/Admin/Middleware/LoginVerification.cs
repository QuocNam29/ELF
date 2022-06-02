using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELF.Areas.Admin.Middleware
{
    public class LoginVerification : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["maND"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Admin_DangNhap/Admin_DangNhap");
                return;
            }
        }
    }
}