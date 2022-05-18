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
    public class DiemTichLuysController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/DiemTichLuys
        public ActionResult Index()
        {
            int maND = int.Parse(Session["maND"].ToString());
            var diemTichLuys = db.DiemTichLuys.Include(d => d.BaiDangSanPham).Include(d => d.BaiDangThongTin).Include(d => d.NguoiDung).Include(d => d.QuyenGop).Where(dtl => dtl.maND == maND).OrderByDescending(dtl => dtl.maDTL);


            return View(diemTichLuys.ToList());
        }

        public ActionResult TongDTL_header()
        {
            int maND = int.Parse(Session["maND"].ToString());
            var diemTichLuys = db.DiemTichLuys.Include(d => d.BaiDangSanPham).Include(d => d.BaiDangThongTin).Include(d => d.NguoiDung).Include(d => d.QuyenGop).Where(dtl => dtl.maND == maND).OrderByDescending(dtl => dtl.maDTL);

            return PartialView("TongDTL_header", diemTichLuys.ToList());
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
