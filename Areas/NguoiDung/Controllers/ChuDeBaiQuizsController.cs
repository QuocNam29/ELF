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
    public class ChuDeBaiQuizsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/ChuDeBaiQuizs
        public ActionResult Index()
        {
            return View(db.ChuDeBaiQuizs.ToList());
        }

        // GET: NguoiDung/ChuDeBaiQuizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDeBaiQuiz chuDeBaiQuiz = db.ChuDeBaiQuizs.Find(id);
            if (chuDeBaiQuiz == null)
            {
                return HttpNotFound();
            }
            return View(chuDeBaiQuiz);
        }

        // GET: NguoiDung/ChuDeBaiQuizs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDung/ChuDeBaiQuizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maChuDe,tenChuDe")] ChuDeBaiQuiz chuDeBaiQuiz)
        {
            if (ModelState.IsValid)
            {
                db.ChuDeBaiQuizs.Add(chuDeBaiQuiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chuDeBaiQuiz);
        }

        // GET: NguoiDung/ChuDeBaiQuizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDeBaiQuiz chuDeBaiQuiz = db.ChuDeBaiQuizs.Find(id);
            if (chuDeBaiQuiz == null)
            {
                return HttpNotFound();
            }
            return View(chuDeBaiQuiz);
        }

        // POST: NguoiDung/ChuDeBaiQuizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maChuDe,tenChuDe")] ChuDeBaiQuiz chuDeBaiQuiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuDeBaiQuiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chuDeBaiQuiz);
        }

        // GET: NguoiDung/ChuDeBaiQuizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDeBaiQuiz chuDeBaiQuiz = db.ChuDeBaiQuizs.Find(id);
            if (chuDeBaiQuiz == null)
            {
                return HttpNotFound();
            }
            return View(chuDeBaiQuiz);
        }

        // POST: NguoiDung/ChuDeBaiQuizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChuDeBaiQuiz chuDeBaiQuiz = db.ChuDeBaiQuizs.Find(id);
            db.ChuDeBaiQuizs.Remove(chuDeBaiQuiz);
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
