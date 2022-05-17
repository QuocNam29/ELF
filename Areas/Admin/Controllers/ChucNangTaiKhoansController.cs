using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Models;

namespace ELF.Areas.Admin.Controllers
{
    public class ChucNangTaiKhoansController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/ChucNangTaiKhoans
        public ActionResult Index()
        { 
            var chucNangTaiKhoans = db.ChucNangTaiKhoans.Include(c => c.LoaiTaiKhoan).Include(c => c.TaiKhoan);

            return View(chucNangTaiKhoans.ToList());
        }
        public ActionResult list_user(int? maLTK, string keyword)
        {
            

            var links = from l in db.ChucNangTaiKhoans.Include(c => c.LoaiTaiKhoan)
                        .Include(c => c.TaiKhoan).Where(c => c.maLoaiTK == maLTK)
                        select l;

            if (!string.IsNullOrEmpty(keyword))
            {
                links = links.Where(b => b.TaiKhoan.NguoiDung.hoVaTen.ToLower().Contains(keyword.ToLower())
                || b.TaiKhoan.email.ToLower().Contains(keyword.ToLower()));
                TempData["keyword"] = keyword;
                return PartialView("list_user", links.ToList());
            }

            return PartialView("list_user", links.ToList());
        }

        public ActionResult list_LoaiTK(int? maTK )
        {
           var chucNangTaiKhoans = db.ChucNangTaiKhoans.Include(c => c.LoaiTaiKhoan).Include(c => c.TaiKhoan).Where(c => c.ID_TaiKhoan == maTK);
            
            
            return PartialView("list_LoaiTK", chucNangTaiKhoans.ToList());
        }
        public ActionResult list_LoaiTK_checkbox(int? maTK)
        {
            var chucNangTaiKhoans = db.ChucNangTaiKhoans.Include(c => c.LoaiTaiKhoan).Include(c => c.TaiKhoan).Where(c => c.ID_TaiKhoan == maTK);


            return PartialView("list_LoaiTK_checkbox", chucNangTaiKhoans.ToList());
        }

        public ActionResult Edit_TT_TaiKhoan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            if (taiKhoan.trangThai == true)
            {
                taiKhoan.trangThai = false;
            }
            else
            {
                taiKhoan.trangThai = true;
            }
            
            db.Entry(taiKhoan).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: Admin/ChucNangTaiKhoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNangTaiKhoan chucNangTaiKhoan = db.ChucNangTaiKhoans.Find(id);

            if (chucNangTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(chucNangTaiKhoan);
        }


        public ActionResult lock_TK(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);

            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            taiKhoan.trangThai = false;
            db.Entry(taiKhoan).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }

        // GET: Admin/ChucNangTaiKhoans/Create
        public ActionResult Create(int maTK, int? maL1, int? maL2, int? maL3, int? maL4, int maChucNang)
        {
            ChucNangTaiKhoan chucNangTaiKhoan = new ChucNangTaiKhoan();
            chucNangTaiKhoan.ID_TaiKhoan = maTK;
            if (maL1 == 1)
            {
                ChucNangTaiKhoan chucNangTK = (from c in db.ChucNangTaiKhoans
                                    where c.ID_TaiKhoan == maTK && c.maLoaiTK == maL1
                                    select c).FirstOrDefault();
                if (chucNangTK == null)
                {
                     chucNangTaiKhoan.maLoaiTK = 1;
                    db.ChucNangTaiKhoans.Add(chucNangTaiKhoan);
                }

            }
            else
            {
                ChucNangTaiKhoan chucNangTK = (from c in db.ChucNangTaiKhoans
                                               where c.ID_TaiKhoan == maTK && c.maLoaiTK == 1
                                               select c).FirstOrDefault();
                if (chucNangTK != null)
                {
                    db.ChucNangTaiKhoans.Remove(chucNangTK);
                }
            }
            if (maL2 == 2)
            {
                ChucNangTaiKhoan chucNangTK = (from c in db.ChucNangTaiKhoans
                                               where c.ID_TaiKhoan == maTK && c.maLoaiTK == maL2
                                               select c).FirstOrDefault();
                if (chucNangTK == null)
                {
                    chucNangTaiKhoan.maLoaiTK = 2;
                    db.ChucNangTaiKhoans.Add(chucNangTaiKhoan);
                }

            }
            else
            {
                ChucNangTaiKhoan chucNangTK = (from c in db.ChucNangTaiKhoans
                                               where c.ID_TaiKhoan == maTK && c.maLoaiTK == 2
                                               select c).FirstOrDefault();
                if (chucNangTK != null)
                {
                    db.ChucNangTaiKhoans.Remove(chucNangTK);
                }
            }
            if (maL3 == 3)
            {
                ChucNangTaiKhoan chucNangTK = (from c in db.ChucNangTaiKhoans
                                               where c.ID_TaiKhoan == maTK && c.maLoaiTK == maL3
                                               select c).FirstOrDefault();
                if (chucNangTK == null)
                {
                    chucNangTaiKhoan.maLoaiTK = 3;
                    db.ChucNangTaiKhoans.Add(chucNangTaiKhoan);
                }

            }
            else
            {
                ChucNangTaiKhoan chucNangTK = (from c in db.ChucNangTaiKhoans
                                               where c.ID_TaiKhoan == maTK && c.maLoaiTK == 3
                                               select c).FirstOrDefault();
                if (chucNangTK != null)
                {
                    db.ChucNangTaiKhoans.Remove(chucNangTK);
                }
            }
            if (maL4 == 4)
            {
                ChucNangTaiKhoan chucNangTK = (from c in db.ChucNangTaiKhoans
                                               where c.ID_TaiKhoan == maTK && c.maLoaiTK == maL4
                                               select c).FirstOrDefault();
                if (chucNangTK == null)
                {
                    chucNangTaiKhoan.maLoaiTK = 4;
                    db.ChucNangTaiKhoans.Add(chucNangTaiKhoan);
                }

            }
            else
            {
                ChucNangTaiKhoan chucNangTK = (from c in db.ChucNangTaiKhoans
                                               where c.ID_TaiKhoan == maTK && c.maLoaiTK == 4
                                               select c).FirstOrDefault();
                if (chucNangTK != null)
                {
                    db.ChucNangTaiKhoans.Remove(chucNangTK);
                }
            }

            db.SaveChanges();

            ViewBag.maLoaiTK = new SelectList(db.LoaiTaiKhoans, "maLoaiTK", "vaiTro");
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "email");
            return RedirectToAction("Details", new { id = maChucNang});
        }

       

        // GET: Admin/ChucNangTaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNangTaiKhoan chucNangTaiKhoan = db.ChucNangTaiKhoans.Find(id);
            if (chucNangTaiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.maLoaiTK = new SelectList(db.LoaiTaiKhoans, "maLoaiTK", "vaiTro", chucNangTaiKhoan.maLoaiTK);
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "email", chucNangTaiKhoan.ID_TaiKhoan);
            return View(chucNangTaiKhoan);
        }

        // POST: Admin/ChucNangTaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_TaiKhoan,maLoaiTK")] ChucNangTaiKhoan chucNangTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chucNangTaiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maLoaiTK = new SelectList(db.LoaiTaiKhoans, "maLoaiTK", "vaiTro", chucNangTaiKhoan.maLoaiTK);
            ViewBag.ID_TaiKhoan = new SelectList(db.TaiKhoans, "ID", "email", chucNangTaiKhoan.ID_TaiKhoan);
            return View(chucNangTaiKhoan);
        }

        // GET: Admin/ChucNangTaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChucNangTaiKhoan chucNangTaiKhoan = db.ChucNangTaiKhoans.Find(id);
            if (chucNangTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(chucNangTaiKhoan);
        }

        // POST: Admin/ChucNangTaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChucNangTaiKhoan chucNangTaiKhoan = db.ChucNangTaiKhoans.Find(id);
            db.ChucNangTaiKhoans.Remove(chucNangTaiKhoan);
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
