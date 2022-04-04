using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    public class BinhLuansController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/BinhLuans
        public ActionResult Index()
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung);
            return View(binhLuans.ToList());
        }

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

        // GET: NguoiDung/BinhLuans/Create
        public void CreateBLBDSP(string coment, int maND, int maBDSP)
        {
            BinhLuan binhLuan = new BinhLuan();
            binhLuan.noiDung = coment;
            binhLuan.maND = maND;
            binhLuan.maBDSP = maBDSP;
            binhLuan.ngayBL = DateTime.Now;
            binhLuan.trangThai = "Hien";
            db.BinhLuans.Add(binhLuan);
            db.SaveChanges();
            
        }

        /*// POST: NguoiDung/BinhLuans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maBL,maND,maBDSP,maBDTT,noiDung,trangThai,ngayBL")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.BinhLuans.Add(binhLuan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", binhLuan.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", binhLuan.maBDTT);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", binhLuan.maND);
            return View(binhLuan);
        }*/

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

        // GET: NguoiDung/BinhLuans/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: NguoiDung/BinhLuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            db.BinhLuans.Remove(binhLuan);
            db.SaveChanges();
            return RedirectToAction("Index");
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
