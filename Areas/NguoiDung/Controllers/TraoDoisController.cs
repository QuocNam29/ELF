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

        public ActionResult Delete_DS(int? id)
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
            return RedirectToAction("Index");
        }

        public ActionResult Delete_BL(int? id)
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

            string tenNguoiDang = Session["tenNguoiDang"].ToString();
            string ngayDang = Session["ngayDang"].ToString();
            string tenSP = Session["tenSP"].ToString();
            string noiDung = Session["noiDung"].ToString();
            string hinhAnh = Session["hinhAnh"].ToString();
            string giaBan = Session["giaBan"].ToString();
            string avt_BD = Session["avt_BD"].ToString();
            string maND_BDSP = Session["maND_BDSP"].ToString();
            int maBDSP = int.Parse(Session["maBDSP"].ToString());
            string maTD = Session["maTD"].ToString();
            Session["flat"] = 2;

            return RedirectToAction("Index", "BinhLuans", new
            {
                maBDSP = maBDSP,
                tenNguoiDang = tenNguoiDang,
                ngayDang = ngayDang,
                tenSP = tenSP,
                noiDung = noiDung,
                hinhAnh = hinhAnh,
                giaBan = giaBan,
                avt_BD = avt_BD,
                flat = 2,
                maTD = maTD,
                maND_BDSP = maND_BDSP
            });
        }

        public ActionResult Create_BL()
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
        public ActionResult Create_BL([Bind(Include = "maTD,maND,maNDKhac,maBDSP,ngayDangKyTD,trangThai")] TraoDoi traoDoi)
        {
            if (ModelState.IsValid)
            {
                traoDoi.ngayDangKyTD = DateTime.Now;
                traoDoi.trangThai = "Đăng ký";
                db.TraoDois.Add(traoDoi);
                db.SaveChanges();
                string tenNguoiDang = Session["tenNguoiDang"].ToString();
                string ngayDang = Session["ngayDang"].ToString();
                string tenSP = Session["tenSP"].ToString();
                string noiDung = Session["noiDung"].ToString();
                string hinhAnh = Session["hinhAnh"].ToString();
                string giaBan = Session["giaBan"].ToString();
                string avt_BD = Session["avt_BD"].ToString();
                string maND_BDSP = Session["maND_BDSP"].ToString();
                int maBDSP = int.Parse(Session["maBDSP"].ToString());
               
                string maTD = Session["maTD"].ToString();
                Session["flat"] = 1;

                return RedirectToAction("Index", "BinhLuans", new
                {
                    maBDSP = maBDSP,
                    tenNguoiDang = tenNguoiDang,
                    ngayDang = ngayDang,
                    tenSP = tenSP,
                    noiDung = noiDung,
                    hinhAnh = hinhAnh,
                    giaBan = giaBan,
                    avt_BD = avt_BD,
                    flat = 1,
                    maTD = maTD,
                    maND_BDSP = maND_BDSP
                });
            }

            ViewBag.maBDSP = new SelectList(db.BaiDangSanPhams, "maBDSP", "tenSP", traoDoi.maBDSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maND);
            ViewBag.maNDKhac = new SelectList(db.NguoiDungs, "maND", "hoVaTen", traoDoi.maNDKhac);
            return View(traoDoi);
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

        public ActionResult InsertTraoDoi(TraoDoi traoDoi)
        {
            using (ELFVanLang2021Entities entities = new ELFVanLang2021Entities())
            {
                entities.TraoDois.Add(traoDoi);
                entities.SaveChanges();
            }
            return Json(traoDoi);
        }
        public ActionResult DeleteTraoDoi(TraoDoi traoDoi)
        {
            using (ELFVanLang2021Entities entities = new ELFVanLang2021Entities())
            {
                TraoDoi traoDoi1 = (from c in entities.TraoDois
                                     where c.maBDSP == traoDoi.maBDSP && c.maND == traoDoi.maND
                                     select c).FirstOrDefault();

                if (traoDoi1 != null)
                {
                    entities.TraoDois.Remove(traoDoi1);
                    entities.SaveChanges();
                   return Json(traoDoi1);
                }
                entities.SaveChanges();
            }
             return new EmptyResult();
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
