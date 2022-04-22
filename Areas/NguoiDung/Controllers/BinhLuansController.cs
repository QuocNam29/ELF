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
    [Route("api/ImportExport")]
    public class BinhLuansController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/BinhLuans
        public ActionResult Index(int maBDSP, string tenNguoiDang,
            DateTime ngayDang, string tenSP, 
            string noiDung, string hinhAnh, 
            string giaBan, string avt_BD,
             int flat, int maTD)
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDSP==maBDSP).OrderByDescending(bl=> bl.maBL);
           

            Session["tenNguoiDang"] = tenNguoiDang;
            Session["ngayDang"] = ngayDang;
            Session["tenSP"] = tenSP;
            Session["noiDung"] = noiDung;
            Session["hinhAnh"] = hinhAnh;
            Session["giaBan"] = giaBan;
            Session["avt_BD"] = avt_BD;

            Session["flat"] = flat;
            Session["maTD"] = maTD;
            Session["maBDSP"] = maBDSP;



            return View(binhLuans.ToList());
        }

        public ActionResult Index_BLBDSP_TrangCaNhan(int maBDSP, string tenNguoiDang,
            DateTime ngayDang, string tenSP,
            string noiDung, string hinhAnh,
            string giaBan, string avt_BD,
             int flat, int maTD)
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDSP == maBDSP).OrderByDescending(bl => bl.maBL);


            Session["tenNguoiDang"] = tenNguoiDang;
            Session["ngayDang"] = ngayDang;
            Session["tenSP"] = tenSP;
            Session["noiDung"] = noiDung;
            Session["hinhAnh"] = hinhAnh;
            Session["giaBan"] = giaBan;
            Session["avt_BD"] = avt_BD;

            Session["flat"] = flat;
            Session["maTD"] = maTD;
            Session["maBDSP"] = maBDSP;



            return View(binhLuans.ToList());
        }


        public ActionResult Index_BDTT(int maBDTT, string tenNguoiDang,
           DateTime ngayDang,string noiDung, string tieuDe,
           string hinhAnh, string avt_BD)
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDTT == maBDTT).OrderByDescending(bl => bl.maBL);


            Session["tenNguoiDang"] = tenNguoiDang;
            Session["ngayDang"] = ngayDang;
            Session["noiDung"] = noiDung;
            Session["hinhAnh"] = hinhAnh;
            Session["tieuDe"] = tieuDe;
            Session["avt_BD"] = avt_BD;
            Session["maBDTT"] = maBDTT;



            return View(binhLuans.ToList());
        }

        public ActionResult Index_BLBDTT_TrangCaNhan(int maBDTT, string tenNguoiDang,
           DateTime ngayDang, string noiDung, string tieuDe,
           string hinhAnh, string avt_BD)
            {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDTT == maBDTT).OrderByDescending(bl => bl.maBL);


            Session["tenNguoiDang"] = tenNguoiDang;
            Session["ngayDang"] = ngayDang;
            Session["noiDung"] = noiDung;
            Session["hinhAnh"] = hinhAnh;
            Session["tieuDe"] = tieuDe;
            Session["avt_BD"] = avt_BD;
            Session["maBDTT"] = maBDTT;


            return View(binhLuans.ToList());
        }



        // GET: NguoiDung/BinhLuans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // GET: NguoiDung/BinhLuans/Create
        [Route("GeneratePDF")]
        [HttpPost]
       
        public ActionResult CreateBLBDSP(string coment, int maND, int maBDSP)
        {
            BinhLuan binhLuan = new BinhLuan();
            binhLuan.noiDung = coment;
            binhLuan.maND = maND;
            binhLuan.maBDSP = maBDSP;
            binhLuan.ngayBL = DateTime.Now;
            binhLuan.trangThai = "Hien";
            db.BinhLuans.Add(binhLuan);

            /*db.ThongBaos.Add(new ThongBao
            {
                maBL = binhLuan.maBL,
                maND = binhLuan.BaiDangSanPham.maND,
                tinhTrang = "Hien",
                ngayTB = DateTime.Now
            });*/

            db.SaveChanges();

            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();
            string tenSP = Session["tenSP"].ToString();
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string giaBan = Session["giaBan"].ToString();
            string avt_BD = Session["avt_BD"].ToString();

            int flat = int.Parse(Session["flat"].ToString());
            string maTD = Session["maTD"].ToString();

            return RedirectToAction("Index", new {
                maBDSP = maBDSP,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                tenSP = tenSP,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                giaBan = giaBan,
                avt_BD = avt_BD,
                flat = flat,
                maTD = maTD
            });

        }

        public ActionResult CreateBLBDSP_TrangCaNhan(string coment, int maND, int maBDSP)
        {
            BinhLuan binhLuan = new BinhLuan();
            binhLuan.noiDung = coment;
            binhLuan.maND = maND;
            binhLuan.maBDSP = maBDSP;
            binhLuan.ngayBL = DateTime.Now;
            binhLuan.trangThai = "Hien";
            db.BinhLuans.Add(binhLuan);
            db.SaveChanges();

            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();
            string tenSP = Session["tenSP"].ToString();
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string giaBan = Session["giaBan"].ToString();
            string avt_BD = Session["avt_BD"].ToString();

            int flat = int.Parse(Session["flat"].ToString());
            string maTD = Session["maTD"].ToString();

            return RedirectToAction("Index_BLBDSP_TrangCaNhan", new
            {
                maBDSP = maBDSP,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                tenSP = tenSP,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                giaBan = giaBan,
                avt_BD = avt_BD,
                flat = flat,
                maTD = maTD
            });

        }


        public ActionResult CreateBLBDTT(string coment, int maND, int maBDTT)
        {
            BinhLuan binhLuan = new BinhLuan();
            binhLuan.noiDung = coment;
            binhLuan.maND = maND;
            binhLuan.maBDTT = maBDTT;
            binhLuan.ngayBL = DateTime.Now;
            binhLuan.trangThai = "Hien";
            db.BinhLuans.Add(binhLuan);
            db.SaveChanges();

            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();        
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();       
            string avt_BD = Session["avt_BD"].ToString();
            return RedirectToAction("Index_BDTT", new
            {
                maBDTT = maBDTT,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,               
                noiDung = noiDung,
                hinhAnh = hinhAnh,          
                avt_BD = avt_BD,
            });

        }

        public ActionResult CreateBLBDTT_TrangCaNhan(string coment, int maND, int maBDTT)
        {
            BinhLuan binhLuan = new BinhLuan();
            binhLuan.noiDung = coment;
            binhLuan.maND = maND;
            binhLuan.maBDTT = maBDTT;
            binhLuan.ngayBL = DateTime.Now;
            binhLuan.trangThai = "Hien";
            db.BinhLuans.Add(binhLuan);
            db.SaveChanges();

            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string avt_BD = Session["avt_BD"].ToString();
            return RedirectToAction("Index_BLBDTT_TrangCaNhan", new
            {
                maBDTT = maBDTT,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                avt_BD = avt_BD,
            });

        }

        
        // GET: NguoiDung/BinhLuans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", binhLuan.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", binhLuan.maBDTT);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", binhLuan.maND);
            return View(binhLuan);
        }

        // POST: NguoiDung/BinhLuans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBL,maND,maBDSP,maBDTT,noiDung,trangThai,ngayBL")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", binhLuan.maBDSP);
            ViewBag.maBDTT = new SelectList(db.BaiDangThongTins, "maBDTT", "tieuDe", binhLuan.maBDTT);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", binhLuan.maND);
            return View(binhLuan);
        }

        // GET: NguoiDung/BinhLuans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            db.BinhLuans.Remove(binhLuan);
            db.SaveChanges();
            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();
            string tenSP = Session["tenSP"].ToString();
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string giaBan = Session["giaBan"].ToString();
            string avt_BD = Session["avt_BD"].ToString();

            int flat = int.Parse(Session["flat"].ToString());
            string maTD = Session["maTD"].ToString();
            int maBDSP = int.Parse(Session["maBDSP"].ToString());

            return RedirectToAction("Index", new
            {
                maBDSP = maBDSP,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                tenSP = tenSP,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                giaBan = giaBan,
                avt_BD = avt_BD,
                flat = flat,
                maTD = maTD
            });
        }

       

        public ActionResult Hidden(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            binhLuan.trangThai = "An";
            db.Entry(binhLuan).State = EntityState.Modified;
            db.SaveChanges();
            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();
            string tenSP = Session["tenSP"].ToString();
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string giaBan = Session["giaBan"].ToString();
            string avt_BD = Session["avt_BD"].ToString();

            int flat = int.Parse(Session["flat"].ToString());
            string maTD = Session["maTD"].ToString();
            int maBDSP = int.Parse(Session["maBDSP"].ToString());
            return RedirectToAction("Index", new
            {
                maBDSP = maBDSP,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                tenSP = tenSP,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                giaBan = giaBan,
                avt_BD = avt_BD,
                flat = flat,
                maTD = maTD
            });

        }

        public ActionResult Appear(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            binhLuan.trangThai = "Hien";
            db.Entry(binhLuan).State = EntityState.Modified;
            db.SaveChanges();
            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();
            string tenSP = Session["tenSP"].ToString();
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string giaBan = Session["giaBan"].ToString();
            string avt_BD = Session["avt_BD"].ToString();

            int flat = int.Parse(Session["flat"].ToString());
            string maTD = Session["maTD"].ToString();
            int maBDSP = int.Parse(Session["maBDSP"].ToString());
            return RedirectToAction("Index", new
            {
                maBDSP = maBDSP,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                tenSP = tenSP,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                giaBan = giaBan,
                avt_BD = avt_BD,
                flat = flat,
                maTD = maTD
            });
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
