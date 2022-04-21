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
    public class DiemTichLuysController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/DiemTichLuys
        public ActionResult Index(int maND)
        {
            var diemTichLuys = db.DiemTichLuys.Include(d => d.BaiDangSanPham).Include(d => d.BaiDangThongTin).Include(d => d.NguoiDung).Include(d => d.QuyenGop).Where(dtl => dtl.maND == maND).OrderByDescending(dtl => dtl.maDTL);
            return View(diemTichLuys.ToList());
        }

        // GET: NguoiDung/DiemTichLuys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemTichLuy diemTichLuy = db.DiemTichLuys.Find(id);
            if (diemTichLuy == null)
            {
                return HttpNotFound();
            }
            return View(diemTichLuy);
        }

        // GET: NguoiDung/DiemTichLuys/Create
        public ActionResult Create()
        {
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP");
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi");
            return View();
        }

        // POST: NguoiDung/DiemTichLuys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDTL,maND,diem,thoiGian,maBDTT,maBDSP,maQG")] DiemTichLuy diemTichLuy)
        {
            if (ModelState.IsValid)
            {
                db.DiemTichLuys.Add(diemTichLuy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", diemTichLuy.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", diemTichLuy.maBDTT);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", diemTichLuy.maND);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", diemTichLuy.maQG);
            return View(diemTichLuy);
        }

        // GET: NguoiDung/DiemTichLuys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemTichLuy diemTichLuy = db.DiemTichLuys.Find(id);
            if (diemTichLuy == null)
            {
                return HttpNotFound();
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", diemTichLuy.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", diemTichLuy.maBDTT);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", diemTichLuy.maND);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", diemTichLuy.maQG);
            return View(diemTichLuy);
        }

        // POST: NguoiDung/DiemTichLuys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDTL,maND,diem,thoiGian,maBDTT,maBDSP,maQG")] DiemTichLuy diemTichLuy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemTichLuy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", diemTichLuy.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", diemTichLuy.maBDTT);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", diemTichLuy.maND);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", diemTichLuy.maQG);
            return View(diemTichLuy);
        }

        // GET: NguoiDung/DiemTichLuys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemTichLuy diemTichLuy = db.DiemTichLuys.Find(id);
            if (diemTichLuy == null)
            {
                return HttpNotFound();
            }
            return View(diemTichLuy);
        }

        // POST: NguoiDung/DiemTichLuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemTichLuy diemTichLuy = db.DiemTichLuys.Find(id);
            db.DiemTichLuys.Remove(diemTichLuy);
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
