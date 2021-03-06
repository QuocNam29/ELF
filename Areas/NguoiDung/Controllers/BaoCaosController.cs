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
    public class BaoCaosController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/BaoCaos
        public ActionResult Index()
        {
            var baoCaos = db.BaoCaos.Include(b => b.NguoiDung).Include(b => b.NguoiDung1);

            return PartialView("Index", baoCaos.ToList());
            
        }

       
        // GET: NguoiDung/BaoCaos/Create
        public ActionResult Create(int maND, int maND_K, string lydo)
        {
            BaoCao baoCao = new BaoCao();
            baoCao.maND = maND;
            baoCao.maNDKhac = maND_K;
            baoCao.lyDo = lydo;
            baoCao.ngayBaoCao = DateTime.Now;
            db.BaoCaos.Add(baoCao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
       

        // GET: NguoiDung/BaoCaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoCao baoCao = db.BaoCaos.Find(id);
            if (baoCao == null)
            {
                return HttpNotFound();
            }
            return View(baoCao);
        }

        // POST: NguoiDung/BaoCaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaoCao baoCao = db.BaoCaos.Find(id);
            db.BaoCaos.Remove(baoCao);
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
