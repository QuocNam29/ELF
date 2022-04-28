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
    public class DonQuasController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/DonQuas
        public ActionResult Index()
        {
            var donQuas = db.DonQuas.Include(d => d.NguoiDung).Include(d => d.QuaTang);
            return View(donQuas.ToList());
        }

        // GET: NguoiDung/DonQuas/Details/5
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

        // GET: NguoiDung/DonQuas/Create
        public ActionResult Create(string maQT, string path, string tenQua, string diemDoi)
        {            
            ViewBag.maQT = maQT;
            ViewBag.hinhAnh = path;
            ViewBag.tenQua = tenQua;
            ViewBag.diemDoi = diemDoi;
            return View();
        }

        // POST: NguoiDung/DonQuas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDQ,MaND,MaQT,NgayTao,TrangThai,TongDiem,DiaChi,GhiChu")] DonQua donQua, int soLuong, string path, string tenQua, string diemDoi)
        {
            int mand = int.Parse(Session["maND"].ToString());

            if ((int)Session["DiemTichLuys"] < (int.Parse(diemDoi) * soLuong))
            {
                TempData["ErrorMessage"] = "Bạn hiện không đủ điểm để đổi quà tặng này 😥";
                return RedirectToAction("Create", "DonQuas", new {maQT = donQua.MaQT, path, tenQua, diemDoi });
            }

            if (ModelState.IsValid)
            {
                db.DonQuas.Add(new DonQua
                {
                    MaDQ = donQua.MaDQ,
                    MaND = mand,
                    MaQT = donQua.MaQT,
                    NgayTao = DateTime.Now,
                    TrangThai = "Chờ duyệt",
                    TongDiem = int.Parse(diemDoi)*soLuong,
                    DiaChi = donQua.DiaChi,
                    GhiChu = donQua.GhiChu
                });

                db.ThongBaos.Add(new ThongBao
                {
                    maDQ = donQua.MaDQ,
                    maND = mand,
                    tinhTrang = "Hien",
                    ngayTB = DateTime.Now,
                    noiDung = "Quà của bạn đã được đổi thành công, bạn vui lòng đợi vài ngày để chúng mình liên lạc và giao hàng đến cho bạn"
                });

                db.DiemTichLuys.Add(new DiemTichLuy
                {
                    maND = mand,
                    thoiGian = DateTime.Now,
                    maDQ = donQua.MaDQ,
                    diem = -(int.Parse(diemDoi) * soLuong)
                });;

                db.SaveChanges();
                return RedirectToAction("Index", "QuaTangs");
            }

            return View(donQua);
        }

        // GET: NguoiDung/DonQuas/Edit/5
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

        // POST: NguoiDung/DonQuas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDQ,MaND,MaQT,NgayTao,TrangThai,TongDiem,DiaChi,GhiChu")] DonQua donQua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donQua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", donQua.MaND);
            ViewBag.MaQT = new SelectList(db.QuaTangs, "maQuaTang", "tenQuaTang", donQua.MaQT);
            return View(donQua);
        }

        // GET: NguoiDung/DonQuas/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: NguoiDung/DonQuas/Delete/5
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
