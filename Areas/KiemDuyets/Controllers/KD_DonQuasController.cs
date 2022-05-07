﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Models;

namespace ELF.Areas.KiemDuyets.Controllers
{
    public class KD_DonQuasController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: KiemDuyets/KD_DonQuas
        public ActionResult Index()
        {
            var donQuas = db.DonQuas.Include(d => d.NguoiDung).Include(d => d.QuaTang);
            return View(donQuas.ToList());
        }

        // GET: KiemDuyets/KD_DonQuas/Details/5
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

        // GET: KiemDuyets/KD_DonQuas/Create
        public ActionResult Create()
        {
            ViewBag.MaND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.MaQT = new SelectList(db.QuaTangs, "maQuaTang", "tenQuaTang");
            return View();
        }

        // POST: KiemDuyets/KD_DonQuas/Create
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

        // GET: KiemDuyets/KD_DonQuas/Edit/5
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

        public ActionResult EditTranngThai(int? maDQ)
        {
            if (maDQ == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonQua donQua = db.DonQuas.Find(maDQ);
            if (donQua == null)
            {
                return HttpNotFound();
            }
            if (donQua.TrangThai == "Chờ xác nhận")
            {
                donQua.TrangThai = "Đang giao hàng";
                db.ThongBaos.Add(new ThongBao
                {
                    maDQ = donQua.MaDQ,
                    maND = donQua.MaND,
                    tinhTrang = "Đang giao hàng",
                    ngayTB = DateTime.Now,
                    noiDung = "Đơn quà của bạn đã được xác nhận thành công, bạn vui lòng đợi vài ngày để chúng mình giao hàng đến cho bạn"
                });
            }
            else 
            {
                donQua.TrangThai = "Đã hoàn tất";
                db.ThongBaos.Add(new ThongBao
                {
                    maDQ = donQua.MaDQ,
                    maND = donQua.MaND,
                    tinhTrang = "Đã hoàn tất",
                    ngayTB = DateTime.Now,
                    noiDung = "Đơn quà đã được giao đến cho bạn, mong bạn thích món quà của ELF"
                });
            }

            db.Entry(donQua).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        public ActionResult Delete_DonQua(int? maDQ, string lydo)
        {
            if (maDQ == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonQua donQua = db.DonQuas.Find(maDQ);
            if (donQua == null)
            {
                return HttpNotFound();
            }
            donQua.TrangThai = "Hủy đơn";
            donQua.GhiChu = lydo;
            db.Entry(donQua).State = EntityState.Modified;

            db.ThongBaos.Add(new ThongBao
            {
                maDQ = maDQ,
                maND = donQua.MaND,
                tinhTrang = "Hủy đơn",
                ngayTB = DateTime.Now,
                noiDung = "Đơn quà của bạn đã bị hủy với lý do: "+lydo
            });

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: KiemDuyets/KD_DonQuas/Delete/5
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