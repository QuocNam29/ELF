using System.Web.Mvc;

namespace ELF.Areas.DonViThuGom
{
    public class DonViThuGomAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DonViThuGom";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DonViThuGom_default",
                "DonViThuGom/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}