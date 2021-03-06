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
    public class QuaTangsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/QuaTangs
        public ActionResult Index()
        {
            return View(db.QuaTangs.ToList());
        }

        

        

        // POST: NguoiDung/QuaTangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
