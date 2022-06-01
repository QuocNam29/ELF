using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.Admin.Middleware;
using ELF.Models;

namespace ELF.Areas.Admin.Controllers
{
    [LoginVerification]
    public class QuyenGopsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/QuyenGops
        public ActionResult Index(string keyword,int maLQG)
        {

            var links = from l in db.QuyenGops.Include(q => q.LoaiQuyenGop)
                         .Include(q => q.NguoiDung).Where(q => q.maLQG == maLQG)
                        select l;
            TempData["maLQG"] = maLQG;
            TempData["keyword"] = keyword;
              
            
            return View(links);
        }

        public ActionResult list_QG(int? maLQG, string trangThai, string keyword)
        {
            var links = from l in db.QuyenGops.Include(q => q.LoaiQuyenGop)
                         .Include(q => q.NguoiDung).Where(q => q.maLQG == maLQG && q.trangThai == trangThai)
                        select l;
            TempData["maLQG"] = maLQG;
            if (!string.IsNullOrEmpty(keyword))
            {
                links = links.Where(b => b.NguoiDung.hoVaTen.Contains(keyword));
                TempData["keyword"] = keyword;

                return PartialView("list_QG", links.ToList());
            }
           
           
            return PartialView("list_QG", links.ToList());
        }
        public ActionResult XoaBai_DonQG(int? maQG, string lydo, int maLQG)
        {
            if (maQG == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenGop quyenGop = db.QuyenGops.Find(maQG);
            if (quyenGop == null)
            {
                return HttpNotFound();
            }
            quyenGop.trangThai = "Đã hủy";
            quyenGop.ghiChu = lydo;
            db.Entry(quyenGop).State = EntityState.Modified;

            db.ThongBaos.Add(new ThongBao
            {
                maQG = maQG,
                maND = quyenGop.maND,
                tinhTrang = "Hien",
                ngayTB = DateTime.Now,
                noiDung = "Hủy duyệt"
            });


            db.SaveChanges();
            return RedirectToAction("Index", new {maLQG = maLQG });
        }
        public ActionResult DuyetBai_DonQG(int? maQG)
        {
            if (maQG == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenGop quyenGop = db.QuyenGops.Find(maQG);
            if (quyenGop == null)
            {
                return HttpNotFound();
            }
            quyenGop.trangThai = "Đã duyệt";
            db.Entry(quyenGop).State = EntityState.Modified;

            db.DiemTichLuys.Add(new DiemTichLuy
            {
                maND = quyenGop.maND,
                thoiGian = DateTime.Now,
                maQG = quyenGop.maQG,
                diem = 10
            });

            db.ThongBaos.Add(new ThongBao
            {
                maQG = maQG,
                maND = quyenGop.maND,
                tinhTrang = "Hien",
                ngayTB = DateTime.Now,
                noiDung = "Đã duyệt"
            });

            db.SaveChanges();
            return RedirectToAction("Index" , new { maLQG = quyenGop.maLQG });
        }


        // GET: Admin/QuyenGops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenGop quyenGop = db.QuyenGops.Find(id);
            if (quyenGop == null)
            {
                return HttpNotFound();
            }
            return View(quyenGop);
        }

        // GET: Admin/QuyenGops/Create
        public ActionResult Create()
        {
            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            return View();
        }

        // POST: Admin/QuyenGops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maQG,maND,ngayQG,maLQG,soLuong,donVi,trangThai,hinhAnh,ghiChu")] QuyenGop quyenGop)
        {
            if (ModelState.IsValid)
            {
                db.QuyenGops.Add(quyenGop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai", quyenGop.maLQG);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", quyenGop.maND);
            return View(quyenGop);
        }

        // GET: Admin/QuyenGops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenGop quyenGop = db.QuyenGops.Find(id);
            if (quyenGop == null)
            {
                return HttpNotFound();
            }
            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai", quyenGop.maLQG);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", quyenGop.maND);
            return View(quyenGop);
        }

        // POST: Admin/QuyenGops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maQG,maND,ngayQG,maLQG,soLuong,donVi,trangThai,hinhAnh,ghiChu")] QuyenGop quyenGop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyenGop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai", quyenGop.maLQG);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", quyenGop.maND);
            return View(quyenGop);
        }

        // GET: Admin/QuyenGops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyenGop quyenGop = db.QuyenGops.Find(id);
            if (quyenGop == null)
            {
                return HttpNotFound();
            }
            return View(quyenGop);
        }

        // POST: Admin/QuyenGops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuyenGop quyenGop = db.QuyenGops.Find(id);
            db.QuyenGops.Remove(quyenGop);
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
