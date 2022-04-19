using System.Web.Mvc;

namespace ELF.Areas.NguoiDung
{
    public class NguoiDungAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NguoiDung";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NguoiDung_default",
                "NguoiDung/{controller}/{action}/{id}",
            new { controller = "BaiDangSanPhams", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}