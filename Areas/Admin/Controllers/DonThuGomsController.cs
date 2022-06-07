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
    public class DonThuGomsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/DonThuGoms
        public ActionResult Index(string keyword, int? maDVTG)
        {
            var links = from l in db.DonThuGoms.Include(q => q.DonViThuGom)
                       .Where(q => q.maDVTG == maDVTG )
                        select l;
            TempData["maDVTG"] = maDVTG;
            TempData["keyword"] = keyword;
            return View(links);
        }
        public ActionResult list_DTG(int? maDVTG, string trangThai, string keyword)
        {
            var links = from l in db.DonThuGoms.Include(q => q.DonViThuGom)
                         .Where(q => q.maDVTG == maDVTG && q.trangThai == trangThai)
                        select l;
            TempData["maDVTG"] = maDVTG;
            if (!string.IsNullOrEmpty(keyword))
            {
                links = links.Where(b => b.thanhPhan.Contains(keyword)||
                                b.viTriTG.Contains(keyword)||
                                b.ghiChu.Contains(keyword));
                TempData["keyword"] = keyword;

                return PartialView("list_DTG", links.ToList());
            }


            return PartialView("list_DTG", links.ToList());
        }

        // GET: Admin/DonThuGoms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonThuGom donThuGom = db.DonThuGoms.Find(id);
            if (donThuGom == null)
            {
                return HttpNotFound();
            }
            return View(donThuGom);
        }

        // GET: Admin/DonThuGoms/Create
        public ActionResult Create()
        {
            ViewBag.maDVTG = new SelectList(db.DonViThuGoms, "maDVTG", "tenDVTG");
            return View();
        }

        // POST: Admin/DonThuGoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDonTG,maDVTG,trangThai,ngayGui,ngayXacNhan,ngayHoanTat,viTriTG,thanhPhan,khoiLuong,ghiChu")] DonThuGom donThuGom)
        {
            if (ModelState.IsValid)
            {
                db.DonThuGoms.Add(donThuGom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maDVTG = new SelectList(db.DonViThuGoms, "maDVTG", "tenDVTG", donThuGom.maDVTG);
            return View(donThuGom);
        }

        // GET: Admin/DonThuGoms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonThuGom donThuGom = db.DonThuGoms.Find(id);
            if (donThuGom == null)
            {
                return HttpNotFound();
            }
            ViewBag.maDVTG = new SelectList(db.DonViThuGoms, "maDVTG", "tenDVTG", donThuGom.maDVTG);
            return View(donThuGom);
        }

        // POST: Admin/DonThuGoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDonTG,maDVTG,trangThai,ngayGui,ngayXacNhan,ngayHoanTat,viTriTG,thanhPhan,khoiLuong,ghiChu")] DonThuGom donThuGom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donThuGom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maDVTG = new SelectList(db.DonViThuGoms, "maDVTG", "tenDVTG", donThuGom.maDVTG);
            return View(donThuGom);
        }

        // GET: Admin/DonThuGoms/Delete/5
        public ActionResult Delete(int? id)
        {
            Models.DonViThuGom donViThuGom = db.DonViThuGoms.Find(id);
            donViThuGom.trangThai = "Đã xóa";
            db.Entry(donViThuGom).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/DonThuGoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonThuGom donThuGom = db.DonThuGoms.Find(id);
            db.DonThuGoms.Remove(donThuGom);
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
