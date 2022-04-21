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
    public class LoaiQuyenGopsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/LoaiQuyenGops
        public ActionResult Index()
        {
            return View(db.LoaiQuyenGops.ToList());
        }

        // GET: NguoiDung/LoaiQuyenGops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiQuyenGop loaiQuyenGop = db.LoaiQuyenGops.Find(id);
            if (loaiQuyenGop == null)
            {
                return HttpNotFound();
            }
            return View(loaiQuyenGop);
        }

        // GET: NguoiDung/LoaiQuyenGops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDung/LoaiQuyenGops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maLQG,tenLoai,noiDung,viTriQP,ghiChu,trangThai")] LoaiQuyenGop loaiQuyenGop)
        {
            if (ModelState.IsValid)
            {
                db.LoaiQuyenGops.Add(loaiQuyenGop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiQuyenGop);
        }

        // GET: NguoiDung/LoaiQuyenGops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiQuyenGop loaiQuyenGop = db.LoaiQuyenGops.Find(id);
            if (loaiQuyenGop == null)
            {
                return HttpNotFound();
            }
            return View(loaiQuyenGop);
        }

        // POST: NguoiDung/LoaiQuyenGops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maLQG,tenLoai,noiDung,viTriQP,ghiChu,trangThai")] LoaiQuyenGop loaiQuyenGop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiQuyenGop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiQuyenGop);
        }

        // GET: NguoiDung/LoaiQuyenGops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiQuyenGop loaiQuyenGop = db.LoaiQuyenGops.Find(id);
            if (loaiQuyenGop == null)
            {
                return HttpNotFound();
            }
            return View(loaiQuyenGop);
        }

        // POST: NguoiDung/LoaiQuyenGops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiQuyenGop loaiQuyenGop = db.LoaiQuyenGops.Find(id);
            db.LoaiQuyenGops.Remove(loaiQuyenGop);
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
