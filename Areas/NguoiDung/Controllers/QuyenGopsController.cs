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
    public class QuyenGopsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/QuyenGops
        public ActionResult Index()
        {
            var quyenGops = db.QuyenGops.Include(q => q.LoaiQuyenGop).Include(q => q.NguoiDung);
            return View(quyenGops.ToList());
        }

        // GET: NguoiDung/QuyenGops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenGop quyenGop = db.QuyenGops.Find(id);
            if (quyenGop == null)
            {
                return HttpNotFound();
            }
            return View(quyenGop);
        }

        // GET: NguoiDung/QuyenGops/Create
        public ActionResult Create()
        {
            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            return View();
        }

        // POST: NguoiDung/QuyenGops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maQG,maND,ngayQG,maLQG,soLuong,donVi,trangThai,hinhAnh,ghiChu")] QuyenGop quyenGop)
        {
            if (ModelState.IsValid)
            {
                db.QuyenGops.Add(quyenGop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai", quyenGop.maLQG);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", quyenGop.maND);
            return View(quyenGop);
        }

        // GET: NguoiDung/QuyenGops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenGop quyenGop = db.QuyenGops.Find(id);
            if (quyenGop == null)
            {
                return HttpNotFound();
            }
            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai", quyenGop.maLQG);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", quyenGop.maND);
            return View(quyenGop);
        }

        // POST: NguoiDung/QuyenGops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maQG,maND,ngayQG,maLQG,soLuong,donVi,trangThai,hinhAnh,ghiChu")] QuyenGop quyenGop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyenGop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai", quyenGop.maLQG);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", quyenGop.maND);
            return View(quyenGop);
        }

        // GET: NguoiDung/QuyenGops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenGop quyenGop = db.QuyenGops.Find(id);
            if (quyenGop == null)
            {
                return HttpNotFound();
            }
            return View(quyenGop);
        }

        // POST: NguoiDung/QuyenGops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuyenGop quyenGop = db.QuyenGops.Find(id);
            db.QuyenGops.Remove(quyenGop);
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
