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
    public class DonQuasController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/DonQuas
        public ActionResult Index()
        {
            var donQuas = db.DonQuas.Include(d => d.NguoiDung).Include(d => d.QuaTang);
            return View(donQuas.ToList());
        }

        // GET: NguoiDung/DonQuas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonQua donQua = db.DonQuas.Find(id);
            if (donQua == null)
            {
                return HttpNotFound();
            }
            return View(donQua);
        }

        // GET: NguoiDung/DonQuas/Create
        public ActionResult Create(string path, string tenQua)
        {
            ViewBag.MaND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.MaQT = new SelectList(db.QuaTangs, "maQuaTang", "tenQuaTang");
            ViewBag.hinhAnh = path;
            ViewBag.tenQua = tenQua;
            return View();
        }

        // POST: NguoiDung/DonQuas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDQ,MaND,MaQT,NgayTao,TrangThai,TongDiem,DiaChi,GhiChu")] DonQua donQua)
        {
            if (ModelState.IsValid)
            {
                db.DonQuas.Add(donQua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", donQua.MaND);
            ViewBag.MaQT = new SelectList(db.QuaTangs, "maQuaTang", "tenQuaTang", donQua.MaQT);
            return View(donQua);
        }

        // GET: NguoiDung/DonQuas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonQua donQua = db.DonQuas.Find(id);
            if (donQua == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", donQua.MaND);
            ViewBag.MaQT = new SelectList(db.QuaTangs, "maQuaTang", "tenQuaTang", donQua.MaQT);
            return View(donQua);
        }

        // POST: NguoiDung/DonQuas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDQ,MaND,MaQT,NgayTao,TrangThai,TongDiem,DiaChi,GhiChu")] DonQua donQua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donQua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", donQua.MaND);
            ViewBag.MaQT = new SelectList(db.QuaTangs, "maQuaTang", "tenQuaTang", donQua.MaQT);
            return View(donQua);
        }

        // GET: NguoiDung/DonQuas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonQua donQua = db.DonQuas.Find(id);
            if (donQua == null)
            {
                return HttpNotFound();
            }
            return View(donQua);
        }

        // POST: NguoiDung/DonQuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonQua donQua = db.DonQuas.Find(id);
            db.DonQuas.Remove(donQua);
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
