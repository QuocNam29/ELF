using System;
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
    public class KiemDuyetsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: KiemDuyets/KiemDuyets
        public ActionResult Index(string keyword)
        {
            var links = from l in db.BaiDangSanPhams.Include(b => b.LoaiSanPham).Include(b => b.NguoiDung)
                        .Include(b => b.TrangThaiBaiDang).Where(b => b.maTT == 1)
                        select l;

            if (!string.IsNullOrEmpty(keyword))
            {
                links = links.Where(b => b.tenSP.ToLower().Contains(keyword.ToLower())
                || b.noiDung.Contains(keyword) || b.NguoiDung.hoVaTen.Contains(keyword));
                TempData["keyword"] = keyword;
                return View(links);
            }
            return View(links);
            
        }
        public ActionResult Index_BDTT(string keyword)
        {
            var links = from l in db.BaiDangThongTins.Include(b => b.NguoiDung)
                        .Include(b => b.TrangThaiBaiDang).Where(b => b.maTT == 1)
                        select l;

            if (!string.IsNullOrEmpty(keyword))
            {
                links = links.Where(b => b.tieuDe.ToLower().Contains(keyword.ToLower())
                || b.noiDung.Contains(keyword) || b.NguoiDung.hoVaTen.Contains(keyword));
                TempData["keyword"] = keyword;
                return View(links);
            }
            return View(links);
           
        }

        public ActionResult Index_DonQG(string keyword, int? maLoaiQG)
        {
            var links = from l in db.QuyenGops.Include(q => q.LoaiQuyenGop)
                        .Include(q => q.NguoiDung).Where(b => b.trangThai == "Chờ duyệt")
                        select l;

            if (!string.IsNullOrEmpty(keyword))
            {
                links = links.Where(b => b.NguoiDung.hoVaTen.Contains(keyword));
                TempData["keyword"] = keyword;
                return View(links);
            }
            if (maLoaiQG != null)
            {
                links = links.Where(b => b.maLQG == maLoaiQG);
                TempData["maLoaiQG"] = maLoaiQG;
                return View(links);
            }
            return View(links);

        }
        

        // GET: KiemDuyets/KiemDuyets/Details/5
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

        public ActionResult Details_BDTT(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(id);
            if (baiDangThongTin == null)
            {
                return HttpNotFound();
            }
            return View(baiDangThongTin);
        }

        public ActionResult Details_DonQG(int? id)
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
        public ActionResult XoaBai_BDSP(int? maBDSP, string lydo)
        {
            if (maBDSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangSanPham baiDangSanPham = db.BaiDangSanPhams.Find(maBDSP);
            if (baiDangSanPham == null)
            {
                return HttpNotFound();
            }
            baiDangSanPham.maTT = 4;
            baiDangSanPham.ghiChu = lydo;
            db.Entry(baiDangSanPham).State = EntityState.Modified;

            db.ThongBaos.Add(new ThongBao
            {
                maBDSP = maBDSP,
                maND = baiDangSanPham.maND,
                tinhTrang = "Hien",
                ngayTB = DateTime.Now,
                noiDung = "Hủy duyệt"
            }) ;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyetBai_BDSP(int? maBDSP)
        {
            if (maBDSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangSanPham baiDangSanPham = db.BaiDangSanPhams.Find(maBDSP);
            if (baiDangSanPham == null)
            {
                return HttpNotFound();
            }
            baiDangSanPham.maTT = 2;
            db.Entry(baiDangSanPham).State = EntityState.Modified;

            db.DiemTichLuys.Add(new DiemTichLuy
            {
                maND = baiDangSanPham.maND,
                thoiGian = DateTime.Now,
                maBDSP = baiDangSanPham.maBDSP,
                diem = 5
            });

            db.ThongBaos.Add(new ThongBao
            {
                maBDSP = maBDSP,
                maND = baiDangSanPham.maND,
                tinhTrang = "Hien",
                ngayTB = DateTime.Now,
                noiDung = "Đã duyệt"
            });

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult XoaBai_BDTT(int? maBDTT, string lydo)
        {
            if (maBDTT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(maBDTT);
            if (baiDangThongTin == null)
            {
                return HttpNotFound();
            }
            baiDangThongTin.maTT = 4;
            baiDangThongTin.ghiChu = lydo;
            db.Entry(baiDangThongTin).State = EntityState.Modified;

            db.ThongBaos.Add(new ThongBao
            {
                maBDTT = maBDTT,
                maND = baiDangThongTin.maND,
                tinhTrang = "Hien",
                ngayTB = DateTime.Now,
                noiDung = "Hủy duyệt"
            });

            db.SaveChanges();
            return RedirectToAction("Index_BDTT");
        }
        public ActionResult DuyetBai_BDTT(int? maBDTT)
        {
            if (maBDTT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(maBDTT);
            if (baiDangThongTin == null)
            {
                return HttpNotFound();
            }
            baiDangThongTin.maTT = 2;
            db.Entry(baiDangThongTin).State = EntityState.Modified;

            db.DiemTichLuys.Add(new DiemTichLuy
            {
                maND = baiDangThongTin.maND,
                thoiGian = DateTime.Now,
                maBDTT = baiDangThongTin.maBDTT,
                diem = 10
            });

            db.ThongBaos.Add(new ThongBao
            {
                maBDTT = maBDTT,
                maND = baiDangThongTin.maND,
                tinhTrang = "Hien",
                ngayTB = DateTime.Now,
                noiDung = "Đã duyệt"
            });

            db.SaveChanges();
            return RedirectToAction("Index_BDTT");
        }

        public ActionResult XoaBai_DonQG(int? maQG, string lydo)
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
            return RedirectToAction("Index_DonQG");
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
            return RedirectToAction("Index_DonQG");
        }




        // GET: KiemDuyets/KiemDuyets/Create
        public ActionResult Create()
        {
            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai");
            return View();
        }

        // POST: KiemDuyets/KiemDuyets/Create
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

        // GET: KiemDuyets/KiemDuyets/Edit/5
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

        // POST: KiemDuyets/KiemDuyets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBDSP,maND,maLSP,tenSP,noiDung,hinhAnh,video,maTT,gia,soLuong,ngayDang,ngayThayDoi,ghiChu")] BaiDangSanPham baiDangSanPham)
        {
            if (ModelState.IsValid)
            {
                baiDangSanPham.maTT = 4;
             
                db.Entry(baiDangSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP", baiDangSanPham.maLSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangSanPham.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangSanPham.maTT);
            return View(baiDangSanPham);
        }

        // GET: KiemDuyets/KiemDuyets/Delete/5
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

        // POST: KiemDuyets/KiemDuyets/Delete/5
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
