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
    public class DiemTichLuysController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/DiemTichLuys
        public ActionResult Index(int? id)
        {
            var diemTichLuys = db.DiemTichLuys.Include(d => d.BaiDangSanPham).Include(d => d.BaiDangThongTin).Include(d => d.DonQua).Include(d => d.KetQua).Include(d => d.NguoiDung).Include(d => d.QuyenGop).Where(t => t.maND == id).OrderByDescending(B => B.maDTL);
            return View(diemTichLuys.ToList());
        }

        public ActionResult List_DTL( string keyword)
        {
            var links = from l in db.ChucNangTaiKhoans.Include(c => c.LoaiTaiKhoan)
                         .Include(c => c.TaiKhoan).Where(c => c.maLoaiTK == 2)
                        select l;

            if (!string.IsNullOrEmpty(keyword))
            {
                links = links.Where(b => b.TaiKhoan.NguoiDung.hoVaTen.ToLower().Contains(keyword.ToLower())
                || b.TaiKhoan.email.ToLower().Contains(keyword.ToLower()));
                TempData["keyword"] = keyword;
                return PartialView("List_DTL", links.ToList());
            }

            return PartialView("List_DTL", links.ToList());
        }



        // GET: Admin/DiemTichLuys/Details/5
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

        // GET: Admin/DiemTichLuys/Create
        public ActionResult Update_DTL(int maND, int diemTL, string lydo)
        {
            db.DiemTichLuys.Add(new DiemTichLuy
            {
                maND = maND,
                thoiGian = DateTime.Now,
                diem = diemTL
            });
            if (diemTL > 0)
            {
             db.ThongBaos.Add(new ThongBao
                        { 
                            maND = maND,
                            tinhTrang = "Cộng điểm",
                            ngayTB = DateTime.Now,
                            noiDung = lydo
                        });
            }
            else
            {
                db.ThongBaos.Add(new ThongBao
                {
                    maND = maND,
                    tinhTrang = "Trừ điểm",
                    ngayTB = DateTime.Now,
                    noiDung = lydo
                });
            }
           
            db.SaveChanges();
            return RedirectToAction("Index", new {id = maND });
            
        }

        // POST: Admin/DiemTichLuys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDTL,maND,diem,thoiGian,maBDTT,maBDSP,maQG,maDQ,maKQBQ")] DiemTichLuy diemTichLuy)
        {
            if (ModelState.IsValid)
            {
                db.DiemTichLuys.Add(diemTichLuy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", diemTichLuy.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", diemTichLuy.maBDTT);
            ViewBag.maDQ = new SelectList(db.DonQuas, "MaDQ", "TrangThai", diemTichLuy.maDQ);
            ViewBag.maKQBQ = new SelectList(db.KetQuas, "maKQ", "maKQ", diemTichLuy.maKQBQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", diemTichLuy.maND);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", diemTichLuy.maQG);
            return View(diemTichLuy);
        }

        // GET: Admin/DiemTichLuys/Edit/5
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
            ViewBag.maDQ = new SelectList(db.DonQuas, "MaDQ", "TrangThai", diemTichLuy.maDQ);
            ViewBag.maKQBQ = new SelectList(db.KetQuas, "maKQ", "maKQ", diemTichLuy.maKQBQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", diemTichLuy.maND);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", diemTichLuy.maQG);
            return View(diemTichLuy);
        }

        // POST: Admin/DiemTichLuys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDTL,maND,diem,thoiGian,maBDTT,maBDSP,maQG,maDQ,maKQBQ")] DiemTichLuy diemTichLuy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemTichLuy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", diemTichLuy.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", diemTichLuy.maBDTT);
            ViewBag.maDQ = new SelectList(db.DonQuas, "MaDQ", "TrangThai", diemTichLuy.maDQ);
            ViewBag.maKQBQ = new SelectList(db.KetQuas, "maKQ", "maKQ", diemTichLuy.maKQBQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", diemTichLuy.maND);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", diemTichLuy.maQG);
            return View(diemTichLuy);
        }

        // GET: Admin/DiemTichLuys/Delete/5
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

        // POST: Admin/DiemTichLuys/Delete/5
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
