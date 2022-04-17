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
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    public class BaiDangSanPhamsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/BaiDangSanPhams
        public ActionResult Index(string keyword)
        {           
            if (keyword == null)
            {
                var baiDangSanPhams = db.BaiDangSanPhams.Include(b => b.LoaiSanPham).Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maTT != 3).OrderByDescending(B => B.maBDSP);
                return View(baiDangSanPhams.ToList());
            }
            var searchSP = db.BaiDangSanPhams.Where(x => x.tenSP.ToLower().Contains(keyword.ToLower())
           || keyword == null).ToList();
            return View(searchSP.ToList());

        }
       

        public ActionResult Index_TrangCaNhan(int maND)
        {
            var baiDangSanPhams = db.BaiDangSanPhams.Include(b => b.LoaiSanPham).Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maND == maND).OrderByDescending(B => B.maBDSP);

            
            return View(baiDangSanPhams.ToList());
        }

        public ActionResult Index_BDSP_one(int maND, int maBDSP)
        {
            var baiDangSanPhams = db.BaiDangSanPhams.Include(b => b.LoaiSanPham).Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maND == maND).Where(b => b.maBDSP == maBDSP);


            return View(baiDangSanPhams.ToList());
        }

        public ActionResult Index_TrangCaNhan_NDK(int maND_K, string hoVaTen_NDK, string avartar_NDK)
        {
            var baiDangSanPhams = db.BaiDangSanPhams.Include(b => b.LoaiSanPham).Include(b => b.NguoiDung).Include(b => b.TrangThaiBaiDang).Where(b => b.maND == maND_K).OrderByDescending(B => B.maBDSP);
            Session["maND_K"] = maND_K;
            Session["hoVaTen_NguoiDungKhac"] = hoVaTen_NDK;
            Session["avatar_NguoiDungKhac"] = avartar_NDK;
            
            return View(baiDangSanPhams.ToList());
        }

        // GET: NguoiDung/BaiDangSanPhams/Details/5
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

        // GET: NguoiDung/BaiDangSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP");
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen");
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai");
            return View();
        }

        // POST: NguoiDung/BaiDangSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maBDSP,maND,maLSP,tenSP,noiDung,hinhAnh,video,maTT,gia,soLuong,ngayDang,ngayThayDoi,ghiChu")] BaiDangSanPham baiDangSanPham, int maND, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filePath = "";
                    if (img != null)
                    {
                        string fileName = System.IO.Path.GetFileName(img.FileName);
                        filePath = "~/images/" + fileName;
                        Console.WriteLine(filePath);
                        img.SaveAs(Server.MapPath(filePath));
                        /*string path = System.IO.Path.Combine(
                                 Server.MapPath("~/images/"), fileName);*/
                        
                        /* img.SaveAs(path);*/
                        baiDangSanPham.maND = maND;
                        baiDangSanPham.maTT = 1;
                        baiDangSanPham.ngayDang = DateTime.Now;
                        baiDangSanPham.hinhAnh = filePath;
                        db.BaiDangSanPhams.Add(baiDangSanPham);

                        db.DiemTichLuys.Add(new DiemTichLuy
                        {
                            maND = maND,
                            thoiGian = DateTime.Now,
                            maBDSP = baiDangSanPham.maBDSP,
                            diem = 5
                        }) ;
                       


                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ViewBag.FileStatus = "Bạn chưa chọn hình ảnh";

                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
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
               
            }

            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP", baiDangSanPham.maLSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangSanPham.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangSanPham.maTT);
            return View(baiDangSanPham);
        }

        // GET: NguoiDung/BaiDangSanPhams/Edit/5
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

        // POST: NguoiDung/BaiDangSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maBDSP,maND,maLSP,tenSP,noiDung,hinhAnh,video,maTT,gia,soLuong,ngayDang,ngayThayDoi,ghiChu")] BaiDangSanPham baiDangSanPham, HttpPostedFileBase img, string hinhAnh)
        {
            if (ModelState.IsValid)
            {
                string oldfilePath = baiDangSanPham.hinhAnh;
                if (img != null && img.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(img.FileName);
                    string path = System.IO.Path.Combine(
                    Server.MapPath("~/images/"), fileName);
                    img.SaveAs(path);
                    baiDangSanPham.hinhAnh = "~/images/" + img.FileName;
                    string fullPath = Request.MapPath(oldfilePath);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                else
                {
                    baiDangSanPham.hinhAnh = hinhAnh;
                }
               baiDangSanPham.ngayThayDoi = DateTime.Now;
                db.Entry(baiDangSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_TrangCaNhan", "BaiDangSanPhams", new { maND = baiDangSanPham.maND });
            }
            ViewBag.maLSP = new SelectList(db.LoaiSanPhams, "maLSP", "tenLSP", baiDangSanPham.maLSP);
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", baiDangSanPham.maND);
            ViewBag.maTT = new SelectList(db.TrangThaiBaiDangs, "maTT", "trangThai", baiDangSanPham.maTT);
            return View(baiDangSanPham);
        }

        // GET: NguoiDung/BaiDangSanPhams/Delete/5
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
           
            db.BaiDangSanPhams.Remove(baiDangSanPham);
            db.SaveChanges();
            return RedirectToAction("Index_TrangCaNhan", "BaiDangSanPhams", new { maND = int.Parse(Session["maND"].ToString()) });

        }

     

        public ActionResult Hidden(int? id)
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
            baiDangSanPham.maTT = 3;
            db.Entry(baiDangSanPham).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_TrangCaNhan", "BaiDangSanPhams", new { maND = int.Parse(Session["maND"].ToString()) });

        }

        public ActionResult Appear(int? id)
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
            baiDangSanPham.maTT = 2;
            db.Entry(baiDangSanPham).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_TrangCaNhan", "BaiDangSanPhams", new { maND = int.Parse(Session["maND"].ToString()) });

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
