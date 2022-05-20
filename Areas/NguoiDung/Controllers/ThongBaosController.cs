using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.NguoiDung.Middleware;
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    [LoginVerification]
    public class ThongBaosController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/ThongBaos
        public ActionResult Index()
        {
            int maND = int.Parse(Session["maND"].ToString());
            var thongBaos = db.ThongBaos.Include(t => t.BaiDangSanPham).Include(t => t.BaiDangThongTin).Include(t => t.BinhLuan).Include(t => t.DonQua).Include(t => t.NguoiDung).Include(t => t.NguoiDung1).Include(t => t.QuyenGop).Include(t => t.TraoDoi).Where(t => t.maND == maND).OrderByDescending(t => t.maTB);
            return View(thongBaos.ToList());
        }
        [ChildActionOnly]
        public ActionResult ThongBao_header()
        {
            int maND = int.Parse(Session["maND"].ToString());
            var thongBaos = db.ThongBaos.Include(t => t.BaiDangSanPham).Include(t => t.BaiDangThongTin).Include(t => t.BinhLuan).Include(t => t.DonQua).Include(t => t.NguoiDung).Include(t => t.NguoiDung1).Include(t => t.QuyenGop).Include(t => t.TraoDoi).Where(t => t.maND == maND).OrderByDescending(t => t.maTB);
           

            return PartialView("ThongBao_header", thongBaos.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
