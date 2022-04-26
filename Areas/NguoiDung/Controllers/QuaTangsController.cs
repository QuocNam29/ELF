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
    public class QuaTangsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/QuaTangs
        public ActionResult Index()
        {
            return View(db.QuaTangs.ToList());
        }

        // GET: NguoiDung/QuaTangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuaTang quaTang = db.QuaTangs.Find(id);
            if (quaTang == null)
            {
                return HttpNotFound();
            }
            return View(quaTang);
        }

        // GET: NguoiDung/QuaTangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDung/QuaTangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maQuaTang,tenQuaTang,diemDoi,trangThai,ngayTao,ngayThayDoi,ghiChu")] QuaTang quaTang)
        {
            if (ModelState.IsValid)
            {
                db.QuaTangs.Add(quaTang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quaTang);
        }

        // GET: NguoiDung/QuaTangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuaTang quaTang = db.QuaTangs.Find(id);
            if (quaTang == null)
            {
                return HttpNotFound();
            }
            return View(quaTang);
        }

        // POST: NguoiDung/QuaTangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maQuaTang,tenQuaTang,diemDoi,trangThai,ngayTao,ngayThayDoi,ghiChu")] QuaTang quaTang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quaTang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quaTang);
        }

        // GET: NguoiDung/QuaTangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuaTang quaTang = db.QuaTangs.Find(id);
            if (quaTang == null)
            {
                return HttpNotFound();
            }
            return View(quaTang);
        }

        // POST: NguoiDung/QuaTangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuaTang quaTang = db.QuaTangs.Find(id);
            db.QuaTangs.Remove(quaTang);
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
