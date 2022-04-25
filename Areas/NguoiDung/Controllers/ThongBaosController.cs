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
    public class ThongBaosController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/ThongBaos
        public ActionResult Index(int maND)
        {
            var thongBaos = db.ThongBaos.Include(t => t.BaiDangSanPham).Include(t => t.BaiDangThongTin).Include(t => t.BinhLuan).Include(t => t.DonQua).Include(t => t.NguoiDung).Include(t => t.NguoiDung1).Include(t => t.QuyenGop).Include(t => t.TraoDoi).Where(t => t.maND == maND).OrderByDescending(t => t.maTB);
            return View(thongBaos.ToList());
        }
        public ActionResult ThongBao_header()
        {
            int maND = int.Parse(Session["maND"].ToString());
            var thongBaos = db.ThongBaos.Include(t => t.BaiDangSanPham).Include(t => t.BaiDangThongTin).Include(t => t.BinhLuan).Include(t => t.DonQua).Include(t => t.NguoiDung).Include(t => t.NguoiDung1).Include(t => t.QuyenGop).Include(t => t.TraoDoi).Where(t => t.maND == maND).OrderByDescending(t => t.maTB);
            return View(thongBaos.ToList());
        }

        // GET: NguoiDung/ThongBaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongBao thongBao = db.ThongBaos.Find(id);
            if (thongBao == null)
            {
                return HttpNotFound();
            }
            return View(thongBao);
        }

        // GET: NguoiDung/ThongBaos/Create
        public ActionResult Create()
        {
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP");
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe");
            ViewBag.maBL = new SelectList(db.BinhLuans, "maBL", "noiDung");
            ViewBag.maDQ = new SelectList(db.DonQuas, "MaDQ", "TrangThai");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi");
            ViewBag.maTraoDoi = new SelectList(db.TraoDois, "maTD", "trangThai");
            return View();
        }

        // POST: NguoiDung/ThongBaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maTB,maBDSP,maBDTT,maBL,maTraoDoi,maQG,maDQ,noiDung,ngayTB,tinhTrang,maND,maNDKhac")] ThongBao thongBao)
        {
            if (ModelState.IsValid)
            {
                db.ThongBaos.Add(thongBao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", thongBao.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", thongBao.maBDTT);
            ViewBag.maBL = new SelectList(db.BinhLuans, "maBL", "noiDung", thongBao.maBL);
            ViewBag.maDQ = new SelectList(db.DonQuas, "MaDQ", "TrangThai", thongBao.maDQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", thongBao.maND);
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen", thongBao.maNDKhac);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", thongBao.maQG);
            ViewBag.maTraoDoi = new SelectList(db.TraoDois, "maTD", "trangThai", thongBao.maTraoDoi);
            return View(thongBao);
        }

        // GET: NguoiDung/ThongBaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongBao thongBao = db.ThongBaos.Find(id);
            if (thongBao == null)
            {
                return HttpNotFound();
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", thongBao.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", thongBao.maBDTT);
            ViewBag.maBL = new SelectList(db.BinhLuans, "maBL", "noiDung", thongBao.maBL);
            ViewBag.maDQ = new SelectList(db.DonQuas, "MaDQ", "TrangThai", thongBao.maDQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", thongBao.maND);
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen", thongBao.maNDKhac);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", thongBao.maQG);
            ViewBag.maTraoDoi = new SelectList(db.TraoDois, "maTD", "trangThai", thongBao.maTraoDoi);
            return View(thongBao);
        }

        // POST: NguoiDung/ThongBaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maTB,maBDSP,maBDTT,maBL,maTraoDoi,maQG,maDQ,noiDung,ngayTB,tinhTrang,maND,maNDKhac")] ThongBao thongBao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongBao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", thongBao.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", thongBao.maBDTT);
            ViewBag.maBL = new SelectList(db.BinhLuans, "maBL", "noiDung", thongBao.maBL);
            ViewBag.maDQ = new SelectList(db.DonQuas, "MaDQ", "TrangThai", thongBao.maDQ);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", thongBao.maND);
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen", thongBao.maNDKhac);
            ViewBag.maQG = new SelectList(db.QuyenGops, "maQG", "donVi", thongBao.maQG);
            ViewBag.maTraoDoi = new SelectList(db.TraoDois, "maTD", "trangThai", thongBao.maTraoDoi);
            return View(thongBao);
        }

        // GET: NguoiDung/ThongBaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongBao thongBao = db.ThongBaos.Find(id);
            if (thongBao == null)
            {
                return HttpNotFound();
            }
            return View(thongBao);
        }

        // POST: NguoiDung/ThongBaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThongBao thongBao = db.ThongBaos.Find(id);
            db.ThongBaos.Remove(thongBao);
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
