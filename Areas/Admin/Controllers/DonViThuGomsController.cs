using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.Admin.Middleware;
using ELF.Models;

namespace ELF.Areas.Admin.Controllers
{
    [LoginVerification]
    public class DonViThuGomsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/DonViThuGoms
        public ActionResult Index()
        {
            return View(db.DonViThuGoms.ToList().OrderByDescending(d => d.maDVTG));
        }

        // GET: Admin/DonViThuGoms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.DonViThuGom donViThuGom = db.DonViThuGoms.Find(id);
            if (donViThuGom == null)
            {
                return HttpNotFound();
            }
            return View(donViThuGom);
        }

        // GET: Admin/DonViThuGoms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DonViThuGoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string tenDVTG, string sdt, string diaChi, string LoaiTG, string hinhThucTG, HttpPostedFileBase img)
        {

            Models.DonViThuGom donViThuGom = new Models.DonViThuGom();

            string filePath = "";
            string time = DateTime.Now.ToString().Replace("/", "-").Replace(":", "");
            if (img != null)
            {
                string fileName = System.IO.Path.GetFileName(img.FileName);
                filePath = time + fileName;
                Console.WriteLine(filePath);
                img.SaveAs(Server.MapPath("~/images/resources/" + filePath));
                /*string path = System.IO.Path.Combine(
                         Server.MapPath("~/images/"), fileName);*/

                /* img.SaveAs(path);*/

                donViThuGom.tenDVTG = tenDVTG;
                donViThuGom.soDienThoai = sdt;
                donViThuGom.loaiThuGom = LoaiTG;
                donViThuGom.hinhThucThuGom = hinhThucTG;
                donViThuGom.logo = filePath;
                donViThuGom.diaChi = diaChi;
                donViThuGom.trangThai = "Đang hoạt động";
                
                db.DonViThuGoms.Add(donViThuGom);
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

        // GET: Admin/DonViThuGoms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.DonViThuGom donViThuGom = db.DonViThuGoms.Find(id);
            if (donViThuGom == null)
            {
                return HttpNotFound();
            }
            return View(donViThuGom);
        }

        // POST: Admin/DonViThuGoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDVTG,tenDVTG,loaiThuGom,diaChi,soDienThoai,hinhThucThuGom,ghiChu")] Models.DonViThuGom donViThuGom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donViThuGom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donViThuGom);
        }

        // GET: Admin/DonViThuGoms/Delete/5
        public ActionResult Delete(int? id)
        {
            Models.DonViThuGom donViThuGom = db.DonViThuGoms.Find(id);
            donViThuGom.trangThai = "Đã xóa";
            db.Entry(donViThuGom).State = EntityState.Modified;
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
