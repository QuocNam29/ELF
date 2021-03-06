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
    [Route("api/ImportExport")]
    public class BinhLuansController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        

        
        
        [HttpPost]
        public ActionResult InsertBinhLuansBDSP(BinhLuan binhLuanBDSP)
        {
            using (ELFVanLang2021Entities entities = new ELFVanLang2021Entities())
            {

                entities.BinhLuans.Add(binhLuanBDSP);
                entities.SaveChanges();
            }
            return Json(binhLuanBDSP);
        }

        [ChildActionOnly]
        public ActionResult listComment_BDSP(int maBDSP )
        {           
                var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDSP == maBDSP).OrderByDescending(bl => bl.maBL).Take(2);
                 return PartialView("listComment_BDSP", binhLuans.ToList());
        }
        public ActionResult listComment_oneBDSP(int maBDSP)
        {   
                var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDSP == maBDSP).OrderByDescending(bl => bl.maBL);
                return PartialView("listComment_oneBDSP", binhLuans.ToList());
        }

        public ActionResult listComment_BDTT(int maBDTT)
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDTT == maBDTT).OrderByDescending(bl => bl.maBL).Take(2);
            return PartialView("listComment_BDTT", binhLuans.ToList());
        }
        public ActionResult listComment_oneBDTT(int maBDTT)
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDTT == maBDTT).OrderByDescending(bl => bl.maBL);
            return PartialView("listComment_oneBDTT", binhLuans.ToList());
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
