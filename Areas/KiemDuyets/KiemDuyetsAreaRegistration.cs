using System.Web.Mvc;

namespace ELF.Areas.KiemDuyets
{
    public class KiemDuyetsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KiemDuyets";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "KiemDuyets_default",
                "KiemDuyets/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}