using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELF.Areas.DonViThuGom.Middleware
{
    public class LoginVerification : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["maND"] == null)
            {
                filterContext.Result = new RedirectResult("~/DonViThuGom/DVTG_DangNhap/DVTG_DangNhap");
                return;
            }
        }
    }
}