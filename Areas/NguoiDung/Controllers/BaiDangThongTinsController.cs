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
using ELF.Areas.NguoiDung.Middleware;
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    [LoginVerification]
    public class BaiDangThongTinsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/BaiDangThongTins
        public ActionResult Index()
        {
            var baiDangThongTins = db.BaiDangThongTins.Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maTT == 2).OrderByDescending(B => B.maBDTT);
            return View(baiDangThongTins.ToList());
        }

        public ActionResult Index_TrangCaNhan(int maND)
        {
            var baiDangThongTins = db.BaiDangThongTins.Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maND == maND).Where(b => b.maTT == 2 || b.maTT == 3).OrderByDescending(B => B.maBDTT);
            return View(baiDangThongTins.ToList());
        }
        public ActionResult Index_BDTT_one(int maND, int maBDTT)
        {
            var baiDangThongTins = db.BaiDangThongTins.Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maND == maND).Where(b => b.maBDTT == maBDTT);


            return View(baiDangThongTins.ToList());
        }

        public ActionResult Index_TrangCaNhan_NDK(int maND_K, string hoVaTen_NDK, string avartar_NDK)
        {
            var baiDangThongTins = db.BaiDangThongTins.Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maND == maND_K).OrderByDescending(B => B.maBDTT);
            Session["maND_K"] = maND_K;
            Session["hoVaTen_NguoiDungKhac"] = hoVaTen_NDK;
            Session["avatar_NguoiDungKhac"] = avartar_NDK;

            return View(baiDangThongTins.ToList());
        }



        // GET: NguoiDung/BaiDangThongTins/Details/5
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

        // GET: NguoiDung/BaiDangThongTins/Create
        public ActionResult Create()
        {
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai");
            return View();
        }

        // POST: NguoiDung/BaiDangThongTins/Create
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
                    if (img != null)
                    {
                        string fileName = System.IO.Path.GetFileName(img.FileName);
                        filePath = "~/images/" + fileName + DateTime.Now;
                        Console.WriteLine(filePath);
                        img.SaveAs(Server.MapPath(filePath));
                        /*string path = System.IO.Path.Combine(
                                 Server.MapPath("~/images/"), fileName);*/

                        /* img.SaveAs(path);*/
                        baiDangThongTin.maND = int.Parse(Session["maND"].ToString());
                        baiDangThongTin.maTT = 1;
                        baiDangThongTin.ngayDang = DateTime.Now;
                        baiDangThongTin.hinhAnh = filePath;
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
                            maTT = 1,
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

        // GET: NguoiDung/BaiDangThongTins/Edit/5
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

        // POST: NguoiDung/BaiDangThongTins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBDTT,maND,tieuDe,noiDung,hinhAnh,video,maTT,ngayDang,ngayThayDoi,ghiChu")] BaiDangThongTin baiDangThongTin, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                string oldfilePath = baiDangThongTin.hinhAnh;
                if (img != null && img.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(img.FileName);
                    string path = System.IO.Path.Combine(
                    Server.MapPath("~/images/"), fileName);
                    img.SaveAs(path);
                    baiDangThongTin.hinhAnh = "~/images/" + img.FileName;
                    string fullPath = Request.MapPath(oldfilePath);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                
                baiDangThongTin.ngayThayDoi = DateTime.Now;
                db.Entry(baiDangThongTin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_TrangCaNhan", "BaiDangThongTins", new { maND = int.Parse(Session["maND"].ToString()) });
            }
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangThongTin.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangThongTin.maTT);
            return View(baiDangThongTin);
        }

        // GET: NguoiDung/BaiDangThongTins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(id);
            baiDangThongTin.maTT = 6;
            db.Entry(baiDangThongTin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_TrangCaNhan", "BaiDangThongTins", new { maND = int.Parse(Session["maND"].ToString()) });
        }

       /*
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiDangThongTin baiDangThongTin = db.BaiDangThongTins.Find(id);
            db.BaiDangThongTins.Remove(baiDangThongTin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
*/
        public ActionResult Hidden(int? id)
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
            baiDangThongTin.maTT = 3;
            db.Entry(baiDangThongTin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_TrangCaNhan", "BaiDangThongTins", new { maND = int.Parse(Session["maND"].ToString()) });

        }

        public ActionResult Appear(int? id)
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
            baiDangThongTin.maTT = 2;
            db.Entry(baiDangThongTin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_TrangCaNhan", "BaiDangThongTins", new { maND = int.Parse(Session["maND"].ToString()) });

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
