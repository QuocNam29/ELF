using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.KiemDuyets.Middleware;
using ELF.Models;

namespace ELF.Areas.KiemDuyets.Controllers
{
    [LoginVerification]
    public class KD_BaiDangsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: KiemDuyets/BaiDangThongTins
        public ActionResult Index()
        {
            var baiDangThongTins = db.BaiDangThongTins.Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maTT == 2).OrderByDescending(B => B.maBDTT);
            return View(baiDangThongTins.ToList());
        }

        // GET: KiemDuyets/BaiDangThongTins/Details/5
        public ActionResult Details(int? id)
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

        // GET: KiemDuyets/BaiDangThongTins/Create
        public ActionResult Create()
        {
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai");
            return View();
        }

        // POST: KiemDuyets/BaiDangThongTins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maBDTT,maND,tieuDe,noiDung,hinhAnh,video,maTT,ngayDang,ngayThayDoi,ghiChu")] BaiDangThongTin baiDangThongTin, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filePath = "";
                    string time = DateTime.Now.ToString().Replace("/","-").Replace(":","") ;
                    if (img != null)
                    {
                        string fileName = System.IO.Path.GetFileName(img.FileName);
                        filePath = "~/images/" + time + fileName ;
                        Console.WriteLine(filePath);
                        img.SaveAs(Server.MapPath(filePath));
                        /*string path = System.IO.Path.Combine(
                                 Server.MapPath("~/images/"), fileName);*/

                        /* img.SaveAs(path);*/
                        baiDangThongTin.maND = int.Parse(Session["maND"].ToString());
                        baiDangThongTin.maTT = 2;
                        baiDangThongTin.ngayDang = DateTime.Now;
                        baiDangThongTin.hinhAnh = filePath;
                        baiDangThongTin.ghiChu = "ELF";
                        db.BaiDangThongTins.Add(baiDangThongTin);

                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        db.BaiDangThongTins.Add(new BaiDangThongTin
                        {
                            maND = int.Parse(Session["maND"].ToString()),
                            tieuDe = baiDangThongTin.tieuDe,
                            noiDung = baiDangThongTin.noiDung,
                            maTT = 2,
                            ghiChu = "ELF",
                            ngayDang = DateTime.Now,
                        });

                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                   
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }


                db.BaiDangThongTins.Add(baiDangThongTin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangThongTin.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangThongTin.maTT);
            return View(baiDangThongTin);
        }

        // GET: KiemDuyets/BaiDangThongTins/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangThongTin.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangThongTin.maTT);
            return View(baiDangThongTin);
        }

        // POST: KiemDuyets/BaiDangThongTins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBDTT,maND,tieuDe,noiDung,hinhAnh,video,maTT,ngayDang,ngayThayDoi,ghiChu")] BaiDangThongTin baiDangThongTin, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                string oldfilePath = baiDangThongTin.hinhAnh;
                string time = DateTime.Now.ToString().Replace("/", "-").Replace(":", "");
                if (img != null && img.ContentLength > 0)
                {
                    var fileName = time +  System.IO.Path.GetFileName(img.FileName);
                    
                    string path = System.IO.Path.Combine(
                    Server.MapPath("~/images/"), fileName);
                    img.SaveAs(path);
                    baiDangThongTin.hinhAnh = "~/images/" + time + img.FileName;
                    string fullPath = Request.MapPath(oldfilePath);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                baiDangThongTin.ngayThayDoi = DateTime.Now;
                db.Entry(baiDangThongTin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangThongTin.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangThongTin.maTT);
            return View(baiDangThongTin);
        }

        // GET: KiemDuyets/BaiDangThongTins/Delete/5
        public ActionResult Delete(int? id)
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


        // POST: KiemDuyets/BaiDangThongTins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(id);
            db.BaiDangThongTins.Remove(baiDangThongTin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index_BDTT_one(int maND, int maBDTT)
        {
            var baiDangThongTins = db.BaiDangThongTins.Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maND == maND).Where(b => b.maBDTT == maBDTT);


            return View(baiDangThongTins.ToList());
        }
        public ActionResult listComment_oneBDTT(int maBDTT)
        {
            var binhLuans = db.BinhLuans.Include(b => b.BaiDangSanPham).Include(b => b.BaiDangThongTin).Include(b => b.NguoiDung).Where(bl => bl.maBDTT == maBDTT).OrderByDescending(bl => bl.maBL);
            return PartialView("listComment_oneBDTT", binhLuans.ToList());
        }

        [HttpPost]
        public ActionResult InsertBinhLuansBDTT(BinhLuan binhLuanBDTT)
        {
            using (ELFVanLang2021Entities entities = new ELFVanLang2021Entities())
            {

                entities.BinhLuans.Add(binhLuanBDTT);
                entities.SaveChanges();
            }
            return Json(binhLuanBDTT);
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
