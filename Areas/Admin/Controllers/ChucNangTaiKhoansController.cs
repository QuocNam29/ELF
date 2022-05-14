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
    public class ChucNangTaiKhoansController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/ChucNangTaiKhoans
        public ActionResult Index()
        { 
            var chucNangTaiKhoans = db.ChucNangTaiKhoans.Include(c => c.LoaiTaiKhoan).Include(c => c.TaiKhoan);

            return View(chucNangTaiKhoans.ToList());
        }
        public ActionResult list_user(int? maLTK, string keyword)
        {
            

            var links = from l in db.ChucNangTaiKhoans.Include(c => c.LoaiTaiKhoan)
                        .Include(c => c.TaiKhoan).Where(c => c.maLoaiTK == maLTK)
                        select l;

            if (!string.IsNullOrEmpty(keyword))
            {
                links = links.Where(b => b.TaiKhoan.NguoiDung.hoVaTen.ToLower().Contains(keyword.ToLower())
                || b.TaiKhoan.email.ToLower().Contains(keyword.ToLower()));
                TempData["keyword"] = keyword;
                return PartialView("list_user", links.ToList());
            }

            return PartialView("list_user", links.ToList());
        }

        // GET: Admin/ChucNangTaiKhoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNangTaiKhoan chucNangTaiKhoan = db.ChucNangTaiKhoans.Find(id);
            if (chucNangTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(chucNangTaiKhoan);
        }

        // GET: Admin/ChucNangTaiKhoans/Create
        public ActionResult Create()
        {
            ViewBag.maLoaiTK = new SelectList(db.LoaiTaiKhoans, "maLoaiTK", "vaiTro");
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "email");
            return View();
        }

        // POST: Admin/ChucNangTaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_TaiKhoan,maLoaiTK")] ChucNangTaiKhoan chucNangTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.ChucNangTaiKhoans.Add(chucNangTaiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maLoaiTK = new SelectList(db.LoaiTaiKhoans, "maLoaiTK", "vaiTro", chucNangTaiKhoan.maLoaiTK);
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "email", chucNangTaiKhoan.ID_TaiKhoan);
            return View(chucNangTaiKhoan);
        }

        // GET: Admin/ChucNangTaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNangTaiKhoan chucNangTaiKhoan = db.ChucNangTaiKhoans.Find(id);
            if (chucNangTaiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.maLoaiTK = new SelectList(db.LoaiTaiKhoans, "maLoaiTK", "vaiTro", chucNangTaiKhoan.maLoaiTK);
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "email", chucNangTaiKhoan.ID_TaiKhoan);
            return View(chucNangTaiKhoan);
        }

        // POST: Admin/ChucNangTaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_TaiKhoan,maLoaiTK")] ChucNangTaiKhoan chucNangTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chucNangTaiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maLoaiTK = new SelectList(db.LoaiTaiKhoans, "maLoaiTK", "vaiTro", chucNangTaiKhoan.maLoaiTK);
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "email", chucNangTaiKhoan.ID_TaiKhoan);
            return View(chucNangTaiKhoan);
        }

        // GET: Admin/ChucNangTaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNangTaiKhoan chucNangTaiKhoan = db.ChucNangTaiKhoans.Find(id);
            if (chucNangTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(chucNangTaiKhoan);
        }

        // POST: Admin/ChucNangTaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChucNangTaiKhoan chucNangTaiKhoan = db.ChucNangTaiKhoans.Find(id);
            db.ChucNangTaiKhoans.Remove(chucNangTaiKhoan);
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
