using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.DonViThuGom.Middleware;
using ELF.Models;

namespace ELF.Areas.DonViThuGom.Controllers
{
    [LoginVerification]
    public class DonThuGomsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        public ActionResult Index(string keyword, int? maDVTG)
        {
            if (maDVTG == null)
            {
                maDVTG = int.Parse(Session["maDVTG"].ToString());
            }
            var links = from l in db.DonThuGoms.Include(q => q.DonViThuGom)
                       .Where(q => q.maDVTG == maDVTG)
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
                links = links.Where(b => b.thanhPhan.Contains(keyword) ||
                                b.viTriTG.Contains(keyword) ||
                                b.ghiChu.Contains(keyword));
                TempData["keyword"] = keyword;

                return PartialView("list_DTG", links.ToList());
            }


            return PartialView("list_DTG", links.ToList());
        }

        // GET: DonViThuGom/DonThuGoms/Xoa_DTG
        public ActionResult Xoa_DTG(int? id, int? maDVTG, string lydo)
        {
            DonThuGom donThuGom = db.DonThuGoms.Find(id);
            donThuGom.trangThai = "Đã hủy";
            donThuGom.ghiChu = "Đơn vị thu gom hủy yêu cầu với lý do: " + lydo;
            db.Entry(donThuGom).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { maDVTG = maDVTG });
        }
        public ActionResult XacNhan_DTG(int? id, int? maDVTG)
        {
            DonThuGom donThuGom = db.DonThuGoms.Find(id);
            if (donThuGom.trangThai == "Gửi yêu cầu")
            {
               donThuGom.trangThai = "Đã xác nhận";
               donThuGom.ngayXacNhan = DateTime.Now;
            }
            else if (donThuGom.trangThai == "Đã xác nhận")
            {
                donThuGom.trangThai = "Hoàn tất";
                donThuGom.ngayHoanTat = DateTime.Now;
            }
         
            db.Entry(donThuGom).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { maDVTG = maDVTG });
        }

        public ActionResult GuiYeuCau(int? id, int? maDVTG)
        {
            DonThuGom donThuGom = db.DonThuGoms.Find(id);            
                donThuGom.trangThai = "Gửi yêu cầu";                      
            db.Entry(donThuGom).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { maDVTG = maDVTG });
        }

        // GET: DonViThuGom/DonThuGoms/Details/5
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

        // GET: DonViThuGom/DonThuGoms/Create
        public ActionResult Create()
        {
            ViewBag.maDVTG = new SelectList(db.DonViThuGoms, "maDVTG", "tenDVTG");
            return View();
        }

        // POST: DonViThuGom/DonThuGoms/Create
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

        // GET: DonViThuGom/DonThuGoms/Edit/5
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

        // POST: DonViThuGom/DonThuGoms/Edit/5
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

        // GET: DonViThuGom/DonThuGoms/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: DonViThuGom/DonThuGoms/Delete/5
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
