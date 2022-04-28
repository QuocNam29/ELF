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
             int flat, int maTD, int maND_BDSP)
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
            Session["maND_BDSP"] = maND_BDSP;



            return View(binhLuans.ToList());
        }

        public ActionResult Index_BLBDSP_TrangCaNhan(int maBDSP, string tenNguoiDang,
            DateTime ngayDang, string tenSP,
            string noiDung, string hinhAnh,
            string giaBan, string avt_BD,
             int flat, int maTD, int maND_BDSP)
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
            Session["maND_BDSP"] = maND_BDSP;


            return View(binhLuans.ToList());
        }


        public ActionResult Index_BDTT(int maBDTT, string tenNguoiDang,
           DateTime ngayDang,string noiDung, string tieuDe,
           string hinhAnh, string avt_BD, int maND_BDTT)
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDTT == maBDTT).OrderByDescending(bl => bl.maBL);


            Session["tenNguoiDang"] = tenNguoiDang;
            Session["ngayDang"] = ngayDang;
            Session["noiDung"] = noiDung;
            
            Session["tieuDe"] = tieuDe;
            Session["avt_BD"] = avt_BD;
            Session["maBDTT"] = maBDTT;
            Session["maND_BDTT"] = maND_BDTT;
            if (hinhAnh != null)
            {
                Session["hinhAnh"] = hinhAnh;
            }
            else
            {
                Session["hinhAnh"] = "false";
            }



            return View(binhLuans.ToList());
        }

        public ActionResult Index_BLBDTT_TrangCaNhan(int maBDTT, string tenNguoiDang,
           DateTime ngayDang, string noiDung, string tieuDe,
           string hinhAnh, string avt_BD, int maND_BDTT)
            {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDTT == maBDTT).OrderByDescending(bl => bl.maBL);


            Session["tenNguoiDang"] = tenNguoiDang;
            Session["ngayDang"] = ngayDang;
            Session["noiDung"] = noiDung;
            
            Session["tieuDe"] = tieuDe;
            Session["avt_BD"] = avt_BD;
            Session["maBDTT"] = maBDTT;
            Session["maND_BDTT"] = maND_BDTT;
            if (hinhAnh != null)
            {
                Session["hinhAnh"] = hinhAnh;
            }
            else
            {
                Session["hinhAnh"] = "false";
            }


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

           
            string maND_BDSP = Session["maND_BDSP"].ToString();
            int maND_BDSP_TB = int.Parse(maND_BDSP);
            if (maND_BDSP_TB != maND)
            {
                db.ThongBaos.Add(new ThongBao
                {
                    maBL = binhLuan.maBL,
                    maND = maND_BDSP_TB,
                    tinhTrang = "Hien",
                    ngayTB = DateTime.Now,
                    noiDung = coment

                });
            }

            

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
                ngayDang = DateTime.Parse(ngayDang),
                tenSP = tenSP,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                giaBan = giaBan,
                avt_BD = avt_BD,
                flat = flat,
                maTD = maTD,
                maND_BDSP = maND_BDSP
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
            string maND_BDSP = Session["maND_BDSP"].ToString();
            int maND_BDSP_TB = int.Parse(maND_BDSP);
            if (maND_BDSP_TB != maND)
            {
                db.ThongBaos.Add(new ThongBao
                {
                    maBL = binhLuan.maBL,
                    maND = maND_BDSP_TB,
                    tinhTrang = "Hien",
                    ngayTB = DateTime.Now,
                    noiDung = coment

                });
            }


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
                ngayDang = DateTime.Parse(ngayDang),
                tenSP = tenSP,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                giaBan = giaBan,
                avt_BD = avt_BD,
                flat = flat,
                maTD = maTD,
                maND_BDSP = maND_BDSP
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

            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(maBDTT);
            if (baiDangThongTin.maND != maND)
            {
                db.ThongBaos.Add(new ThongBao
                {
                    maBL = binhLuan.maBL,
                    maND = baiDangThongTin.maND,
                    tinhTrang = "Hien",
                    ngayTB = DateTime.Now,
                    noiDung = coment

                });
            }

            db.SaveChanges();

            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();        
            string noiDung = Session["noiDung"].ToString();
           string hinhAnh = Session["hinhAnh"].ToString();   
            string avt_BD = Session["avt_BD"].ToString();
            string tieuDe = Session["tieuDe"].ToString();
            return RedirectToAction("Index_BDTT", new
            {
                maBDTT = maBDTT,
                tenNguoiDang = tenNguoiDang,
                ngayDang = DateTime.Parse(ngayDang),               
                noiDung = noiDung,
                hinhAnh = hinhAnh,          
                avt_BD = avt_BD,
                tieuDe = tieuDe
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

            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(maBDTT);
            if (baiDangThongTin.maND != maND)
            {
                db.ThongBaos.Add(new ThongBao
                {
                    maBL = binhLuan.maBL,
                    maND = baiDangThongTin.maND,
                    tinhTrang = "Hien",
                    ngayTB = DateTime.Now,
                    noiDung = coment

                });
            }

            db.SaveChanges();

            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string avt_BD = Session["avt_BD"].ToString();
            string tieuDe = Session["tieuDe"].ToString();
            return RedirectToAction("Index_BLBDTT_TrangCaNhan", new
            {
                maBDTT = maBDTT,
                tenNguoiDang = tenNguoiDang,
                ngayDang = DateTime.Parse(ngayDang),
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                avt_BD = avt_BD,
                tieuDe = tieuDe
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
        public ActionResult Delete_BL_BDTT(int? id)
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
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string avt_BD = Session["avt_BD"].ToString();
            string tieuDe = Session["tieuDe"].ToString();
            int maBDTT = int.Parse(Session["maBDTT"].ToString());
            return RedirectToAction("Index_BDTT", new
            {
                maBDTT = maBDTT,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                avt_BD = avt_BD,
                tieuDe = tieuDe
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

        public ActionResult Hidden_BL_BDTT (int? id)
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
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string avt_BD = Session["avt_BD"].ToString();
            string tieuDe = Session["tieuDe"].ToString();
            int maBDTT = int.Parse(Session["maBDTT"].ToString());
            return RedirectToAction("Index_BDTT", new
            {
                maBDTT = maBDTT,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                avt_BD = avt_BD,
                tieuDe = tieuDe
            });

        }

        public ActionResult Appear_BL_BDTT(int? id)
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
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string avt_BD = Session["avt_BD"].ToString();
            string tieuDe = Session["tieuDe"].ToString();
            int maBDTT = int.Parse(Session["maBDTT"].ToString());
            return RedirectToAction("Index_BDTT", new
            {
                maBDTT = maBDTT,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                avt_BD = avt_BD,
                tieuDe = tieuDe
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
