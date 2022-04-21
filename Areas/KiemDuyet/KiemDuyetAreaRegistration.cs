using System.Web.Mvc;

namespace ELF.Areas.KiemDuyet
{
    public class KiemDuyetAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KiemDuyet";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KiemDuyet_default",
                "KiemDuyet/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}