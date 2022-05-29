﻿using System;
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
    public class LoaiQuyenGopsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/LoaiQuyenGops
        public ActionResult Index()
        {
            return View(db.LoaiQuyenGops.ToList());
        }

        // GET: Admin/LoaiQuyenGops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiQuyenGop loaiQuyenGop = db.LoaiQuyenGops.Find(id);
            if (loaiQuyenGop == null)
            {
                return HttpNotFound();
            }
            return View(loaiQuyenGop);
        }

        // GET: Admin/LoaiQuyenGops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiQuyenGops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string loaiQG, string vitri, string noidung, string ghichu, HttpPostedFileBase img)
        {
            LoaiQuyenGop loaiQuyenGop = new LoaiQuyenGop();

            string filePath = "";
            string time = DateTime.Now.ToString().Replace("/", "-").Replace(":", "");
            if (img != null)
            {
                string fileName = System.IO.Path.GetFileName(img.FileName);
                filePath = time + fileName;
                Console.WriteLine(filePath);
                img.SaveAs(Server.MapPath("~/images/resources/" + filePath));
                /*string path = System.IO.Path.Combine(
                         Server.MapPath("~/images/"), fileName);*/

                /* img.SaveAs(path);*/



                loaiQuyenGop.tenLoai = loaiQG;
                loaiQuyenGop.noiDung = noidung;
                loaiQuyenGop.hinhAnh = filePath;
                loaiQuyenGop.viTriQP = vitri;
                loaiQuyenGop.trangThai = "Đang hoạt động";
                if (ghichu != null && ghichu !="")
                {
                    loaiQuyenGop.ghiChu = ghichu;
                }
                db.LoaiQuyenGops.Add(loaiQuyenGop);

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.FileStatus = "Bạn chưa chọn hình ảnh";

            }
            ViewBag.FileStatus = "File uploaded successfully.";





            return RedirectToAction("Index");
        }

        // GET: Admin/LoaiQuyenGops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiQuyenGop loaiQuyenGop = db.LoaiQuyenGops.Find(id);
            if (loaiQuyenGop == null)
            {
                return HttpNotFound();
            }
            return View(loaiQuyenGop);
        }

        // POST: Admin/LoaiQuyenGops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maLQG,tenLoai,noiDung,viTriQP,ghiChu,trangThai,hinhAnh")] LoaiQuyenGop loaiQuyenGop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiQuyenGop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiQuyenGop);
        }

        // GET: Admin/LoaiQuyenGops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiQuyenGop loaiQuyenGop = db.LoaiQuyenGops.Find(id);
            if (loaiQuyenGop == null)
            {
                return HttpNotFound();
            }
            return View(loaiQuyenGop);
        }

        // POST: Admin/LoaiQuyenGops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiQuyenGop loaiQuyenGop = db.LoaiQuyenGops.Find(id);
            db.LoaiQuyenGops.Remove(loaiQuyenGop);
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
