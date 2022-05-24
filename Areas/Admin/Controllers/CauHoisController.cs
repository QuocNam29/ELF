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
    public class CauHoisController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: Admin/CauHois
        public ActionResult Index(int? maCD)
        {
            var cauHois = db.CauHois.Include(c => c.ChuDeBaiQuiz).Where(b => b.maChuDe == maCD);
            ChuDeBaiQuiz chuDeBaiQuiz = db.ChuDeBaiQuizs.Find(maCD);
            TempData["TenChuDe"] = chuDeBaiQuiz.tenChuDe;
            return View(cauHois.ToList());
        }

        public ActionResult Index_DA(int? maCH)
        {
            var dapAns = db.DapAns.Include(d => d.CauHoi).Where(b => b.maCauHoi == maCH);
            CauHoi cauHoi = db.CauHois.Find(maCH);
            TempData["NoiDungCauHoi"] = cauHoi.noiDungCauHoi;
            return View(dapAns.ToList());
        }

        // GET: Admin/CauHois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            return View(cauHoi);
        }

        // GET: Admin/CauHois/Create
        public ActionResult Create(int maCD, string cauHoi, string DA1, string DA2, string DA3, string DA4, string DA_true)
        {

            db.CauHois.Add(new CauHoi
            {
                noiDungCauHoi = cauHoi,
                ngayTao = System.DateTime.Now,
                maChuDe = maCD
            });
            db.SaveChanges();
            int maCH = db.CauHois.Max(item => item.maCauHoi);
            bool flat = false;
            if (DA1 == DA_true)
            {
                flat = true;
            }
            db.DapAns.Add(new DapAn
            {
                maCauHoi = maCH,
                NoiDungDapAn = DA1,
                dapAn1 = flat

            });
            if (DA2 == DA_true)
            {
                flat = true;
            }
            else
            {
                flat = false;
            }
            db.DapAns.Add(new DapAn
            {
                maCauHoi = maCH,
                NoiDungDapAn = DA1,
                dapAn1 = flat

            });
            if (DA3 == DA_true)
            {
                flat = true;
            }
            else
            {
                flat = false;
            }
            db.DapAns.Add(new DapAn
            {
                maCauHoi = maCH,
                NoiDungDapAn = DA1,
                dapAn1 = flat

            });
            if (DA4 == DA_true)
            {
                flat = true;
            }
            else
            {
                flat = false;
            }
            db.DapAns.Add(new DapAn
            {
                maCauHoi = maCH,
                NoiDungDapAn = DA1,
                dapAn1 = flat

            });
            db.SaveChanges();
            return RedirectToAction("Index", new { maCD = maCD });
        }

     

        // GET: Admin/CauHois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauHoi cauHoi = db.CauHois.Find(id);
            if (cauHoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.maChuDe = new SelectList(db.ChuDeBaiQuizs, "maChuDe", "tenChuDe", cauHoi.maChuDe);
            return View(cauHoi);
        }

        // POST: Admin/CauHois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maCauHoi,noiDungCauHoi,ngayTao,ngayThayDoi,maChuDe")] CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauHoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maChuDe = new SelectList(db.ChuDeBaiQuizs, "maChuDe", "tenChuDe", cauHoi.maChuDe);
            return View(cauHoi);
        }

        // GET: Admin/CauHois/Delete/5
        public ActionResult Delete(int? id, int? maCD)
        {
            CauHoi cauHoi = db.CauHois.Find(id);
            /* cauHoi.trangThai = "Đã xóa";
             db.Entry(cauHoi).State = EntityState.Modified;*/
            db.CauHois.Remove(cauHoi);
            db.SaveChanges();
            return RedirectToAction("Index", new {maCD = maCD });
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
