using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    public class TraoDoisController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();
        
        // GET: NguoiDung/TraoDois
        public ActionResult Index()
        {
           
            var traoDois = db.TraoDois.Include(t => t.BaiDangSanPham).Include(t => t.NguoiDung).Include(t => t.NguoiDung1).OrderBy(td => td.maBDSP);
            return View(traoDois.ToList());
        }

        // GET: NguoiDung/TraoDois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraoDoi traoDoi = db.TraoDois.Find(id);
            if (traoDoi == null)
            {
                return HttpNotFound();
            }
            return View(traoDoi);
        }


        // GET: NguoiDung/TraoDois/Create
        public ActionResult Create()
        {
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            return View();
        }

        // POST: NguoiDung/TraoDois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maTD,maND,maNDKhac,maBDSP,ngayDangKyTD,trangThai")] TraoDoi traoDoi)
        {
            if (ModelState.IsValid)
            {
                traoDoi.ngayDangKyTD = DateTime.Now;
                traoDoi.trangThai = "Đăng ký";
                db.TraoDois.Add(traoDoi);
                db.SaveChanges();
                return RedirectToAction("Index", "BaiDangSanPhams");
            }

            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", traoDoi.maBDSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maND);
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maNDKhac);
            return View(traoDoi);
        }

        // GET: NguoiDung/TraoDois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraoDoi traoDoi = db.TraoDois.Find(id);
            if (traoDoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", traoDoi.maBDSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maND);
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maNDKhac);
            return View(traoDoi);
        }

        // POST: NguoiDung/TraoDois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maTD,maND,maNDKhac,maBDSP,ngayDangKyTD,trangThai")] TraoDoi traoDoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traoDoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", traoDoi.maBDSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maND);
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maNDKhac);
            return View(traoDoi);
        }

        // GET: NguoiDung/TraoDois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraoDoi traoDoi = db.TraoDois.Find(id);
            if (traoDoi == null)
            {
                return HttpNotFound();
            }
           
            db.TraoDois.Remove(traoDoi);
            db.SaveChanges();
            return RedirectToAction("Index", "BaiDangSanPhams");
        }

        /* // POST: NguoiDung/TraoDois/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public ActionResult DeleteConfirmed(int id)
         {
             TraoDoi traoDoi = db.TraoDois.Find(id);
             db.TraoDois.Remove(traoDoi);
             db.SaveChanges();
             return RedirectToAction("Index", "BaiDangSanPhams");
         }*/

        public ActionResult EditTranngThai(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraoDoi traoDoi = db.TraoDois.Find(id);
            if (traoDoi == null)
            {
                return HttpNotFound();
            }
            traoDoi.trangThai = "Xác nhận";
            db.Entry(traoDoi).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", traoDoi.maBDSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maND);
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maNDKhac);
            return RedirectToAction("Index", "TraoDois");
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
