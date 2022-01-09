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
    public class BaiDangThongTinsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/BaiDangThongTins
        public ActionResult Index()
        {
            var baiDangThongTins = db.BaiDangThongTins.Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang);
            return View(baiDangThongTins.ToList());
        }

        // GET: NguoiDung/BaiDangThongTins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(id);
            if (baiDangThongTin == null)
            {
                return HttpNotFound();
            }
            return View(baiDangThongTin);
        }

        // GET: NguoiDung/BaiDangThongTins/Create
        public ActionResult Create()
        {
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai");
            return View();
        }

        // POST: NguoiDung/BaiDangThongTins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maBDTT,maND,noiDung,hinhAnh,video,maTT,ngayDang,ngayThayDoi,ghiChu")] BaiDangThongTin baiDangThongTin)
        {
            if (ModelState.IsValid)
            {
                db.BaiDangThongTins.Add(baiDangThongTin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangThongTin.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangThongTin.maTT);
            return View(baiDangThongTin);
        }

        // GET: NguoiDung/BaiDangThongTins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(id);
            if (baiDangThongTin == null)
            {
                return HttpNotFound();
            }
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangThongTin.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangThongTin.maTT);
            return View(baiDangThongTin);
        }

        // POST: NguoiDung/BaiDangThongTins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBDTT,maND,noiDung,hinhAnh,video,maTT,ngayDang,ngayThayDoi,ghiChu")] BaiDangThongTin baiDangThongTin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiDangThongTin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangThongTin.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangThongTin.maTT);
            return View(baiDangThongTin);
        }

        // GET: NguoiDung/BaiDangThongTins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(id);
            if (baiDangThongTin == null)
            {
                return HttpNotFound();
            }
            return View(baiDangThongTin);
        }

        // POST: NguoiDung/BaiDangThongTins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(id);
            db.BaiDangThongTins.Remove(baiDangThongTin);
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
