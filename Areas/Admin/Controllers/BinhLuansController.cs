using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Models;

namespace ELF.Areas.Admin.Controllers
{
    public class BinhLuansController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        [HttpPost]
        public ActionResult InsertBinhLuansDTG(BinhLuan binhLuanDTG)
        {
            using (ELFVanLang2021Entities entities = new ELFVanLang2021Entities())
            {

                entities.BinhLuans.Add(binhLuanDTG);
                entities.SaveChanges();
            }
            return Json(binhLuanDTG);
        }

        public ActionResult listComment_DTG(int maDTG)
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maDonTG == maDTG);
            return PartialView("listComment_DTG", binhLuans.ToList());
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
