using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELF.Areas.KiemDuyets.Middleware
{
    public class LoginVerification : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["maND"] == null)
            {
                filterContext.Result = new RedirectResult("~/KiemDuyets/KiemDuyet_DangNhap/KiemDuyet_DangNhap");
                return;
            }
        }
    }
}