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

        

        // GET: NguoiDung/BinhLuans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        
        // GET: NguoiDung/BinhLuans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", binhLuan.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", binhLuan.maBDTT);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", binhLuan.maND);
            return View(binhLuan);
        }

        // POST: NguoiDung/BinhLuans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBL,maND,maBDSP,maBDTT,noiDung,trangThai,ngayBL")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", binhLuan.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", binhLuan.maBDTT);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", binhLuan.maND);
            return View(binhLuan);
        }

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
