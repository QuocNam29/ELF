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
    public class KetQuasController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/KetQuas
        public ActionResult Index()
        {
            int mand = int.Parse(Session["maND"].ToString());
            var ketQuas = db.KetQuas.Include(k => k.ChuDeBaiQuiz).Include(k => k.NguoiDung).Where(q => q.maND == mand).OrderByDescending(q => q.maKQ);
            return View(ketQuas.ToList());
        }

        // GET: NguoiDung/KetQuas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQua ketQua = db.KetQuas.Find(id);
            if (ketQua == null)
            {
                return HttpNotFound();
            }
            return View(ketQua);
        }

        // GET: NguoiDung/KetQuas/Create
        public ActionResult Create()
        {
            ViewBag.maCDBQ = new SelectList(db.ChuDeBaiQuizs, "maChuDe", "tenChuDe");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            return View();
        }

        // POST: NguoiDung/KetQuas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maKQ,maND,maCDBQ,TongDiem")] KetQua ketQua)
        {
            if (ModelState.IsValid)
            {
                db.KetQuas.Add(ketQua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maCDBQ = new SelectList(db.ChuDeBaiQuizs, "maChuDe", "tenChuDe", ketQua.maCDBQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", ketQua.maND);
            return View(ketQua);
        }

        // GET: NguoiDung/KetQuas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQua ketQua = db.KetQuas.Find(id);
            if (ketQua == null)
            {
                return HttpNotFound();
            }
            ViewBag.maCDBQ = new SelectList(db.ChuDeBaiQuizs, "maChuDe", "tenChuDe", ketQua.maCDBQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", ketQua.maND);
            return View(ketQua);
        }

        // POST: NguoiDung/KetQuas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maKQ,maND,maCDBQ,TongDiem")] KetQua ketQua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ketQua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maCDBQ = new SelectList(db.ChuDeBaiQuizs, "maChuDe", "tenChuDe", ketQua.maCDBQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", ketQua.maND);
            return View(ketQua);
        }

        // GET: NguoiDung/KetQuas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQua ketQua = db.KetQuas.Find(id);
            if (ketQua == null)
            {
                return HttpNotFound();
            }
            return View(ketQua);
        }

        // POST: NguoiDung/KetQuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KetQua ketQua = db.KetQuas.Find(id);
            db.KetQuas.Remove(ketQua);
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
