﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Models;

namespace ELF.Areas.KiemDuyet.Controllers
{
    public class KiemDuyetsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: KiemDuyet/KiemDuyets
        public ActionResult Index()
        {
            var baiDangSanPhams = db.BaiDangSanPhams.Include(b => b.LoaiSanPham).Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maTT == 1).OrderByDescending(B => B.maBDSP);
            return View(baiDangSanPhams.ToList());
        }

        // GET: KiemDuyet/KiemDuyets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangSanPham baiDangSanPham = db.BaiDangSanPhams.Find(id);
            if (baiDangSanPham == null)
            {
                return HttpNotFound();
            }
            return View(baiDangSanPham);
        }

        // GET: KiemDuyet/KiemDuyets/Create
        public ActionResult Create()
        {
            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai");
            return View();
        }

        // POST: KiemDuyet/KiemDuyets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maBDSP,maND,maLSP,tenSP,noiDung,hinhAnh,video,maTT,gia,soLuong,ngayDang,ngayThayDoi,ghiChu")] BaiDangSanPham baiDangSanPham)
        {
            if (ModelState.IsValid)
            {
                db.BaiDangSanPhams.Add(baiDangSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP", baiDangSanPham.maLSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangSanPham.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangSanPham.maTT);
            return View(baiDangSanPham);
        }

        // GET: KiemDuyet/KiemDuyets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangSanPham baiDangSanPham = db.BaiDangSanPhams.Find(id);
            if (baiDangSanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP", baiDangSanPham.maLSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangSanPham.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangSanPham.maTT);
            return View(baiDangSanPham);
        }

        // POST: KiemDuyet/KiemDuyets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBDSP,maND,maLSP,tenSP,noiDung,hinhAnh,video,maTT,gia,soLuong,ngayDang,ngayThayDoi,ghiChu")] BaiDangSanPham baiDangSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiDangSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP", baiDangSanPham.maLSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangSanPham.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangSanPham.maTT);
            return View(baiDangSanPham);
        }

        // GET: KiemDuyet/KiemDuyets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangSanPham baiDangSanPham = db.BaiDangSanPhams.Find(id);
            if (baiDangSanPham == null)
            {
                return HttpNotFound();
            }
            return View(baiDangSanPham);
        }

        // POST: KiemDuyet/KiemDuyets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiDangSanPham baiDangSanPham = db.BaiDangSanPhams.Find(id);
            db.BaiDangSanPhams.Remove(baiDangSanPham);
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
