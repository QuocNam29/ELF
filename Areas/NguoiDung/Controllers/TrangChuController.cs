using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.NguoiDung.Middleware;
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    [LoginVerification]
    public class TrangChuController : Controller
    {
        ELFVanLang2021Entities model = new ELFVanLang2021Entities();
        // GET: NguoiDung/TrangChu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult DangKyTaiKhoan()
        {
            return View();
        }
        public ActionResult DangKyTaiKhoan2()
        {
            using (model)
            {
                return View();
            }
        }

    }
}