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
using ELF.Areas.Admin.Middleware;
using ELF.Models;

namespace ELF.Areas.Admin.Controllers
{
    [LoginVerification]
    public class QuaTangsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/QuaTangs
        public ActionResult Index()
        {
            return View(db.QuaTangs.ToList());
        }

        // GET: Admin/QuaTangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuaTang quaTang = db.QuaTangs.Find(id);
            if (quaTang == null)
            {
                return HttpNotFound();
            }
            return View(quaTang);
        }

        // GET: Admin/QuaTangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuaTangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string tenQuaTang, int diemDoi, string ghichu, HttpPostedFileBase img)
        {
            QuaTang quaTang = new QuaTang();
           
                string filePath = "";
                string time = DateTime.Now.ToString().Replace("/", "-").Replace(":", "");
                if (img != null)
                {
                    string fileName = System.IO.Path.GetFileName(img.FileName);
                    filePath =  time + fileName;
                    Console.WriteLine(  filePath);
                    img.SaveAs(Server.MapPath("~/images/resources/" + filePath));
                    /*string path = System.IO.Path.Combine(
                             Server.MapPath("~/images/"), fileName);*/

                    /* img.SaveAs(path);*/
                   

                   
                    quaTang.tenQuaTang = tenQuaTang;
                    quaTang.ghiChu = ghichu;
                    quaTang.diemDoi = diemDoi;
                    quaTang.ngayTao = DateTime.Now;
                    quaTang.hinhAnh = filePath;
                quaTang.trangThai = "Còn";
                    db.QuaTangs.Add(quaTang);

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.FileStatus = "Bạn chưa chọn hình ảnh";

                }
                ViewBag.FileStatus = "File uploaded successfully.";
            
           
          
                
               
                return RedirectToAction("Index");
            

           
        }

        // GET: Admin/QuaTangs/Edit/5
        public ActionResult Edit(int? id, string tenQuaTang, int diemDoi, string ghichu, HttpPostedFileBase img)
        {
            QuaTang quaTang = db.QuaTangs.Find(id);
            string oldfilePath = quaTang.hinhAnh ;
            string time = DateTime.Now.ToString().Replace("/", "-").Replace(":", "");
            if (img != null && img.ContentLength > 0)
            {
                var fileName = time + System.IO.Path.GetFileName(img.FileName);
                string path = System.IO.Path.Combine(
                Server.MapPath("~/images/resources/"), fileName);
                img.SaveAs(path);
                quaTang.hinhAnh = time + img.FileName;
                string fullPath = Request.MapPath(oldfilePath);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            
            
            quaTang.tenQuaTang = tenQuaTang;
            quaTang.diemDoi = diemDoi;
            quaTang.ghiChu = ghichu;
            quaTang.ngayThayDoi = DateTime.Now;
            db.Entry(quaTang).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "QuaTangs", new { id = id });
        }

       

        // GET: Admin/QuaTangs/Delete/5
        public ActionResult Delete(int? id)
        {
            QuaTang quaTang = db.QuaTangs.Find(id);
            db.QuaTangs.Remove(quaTang);
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
