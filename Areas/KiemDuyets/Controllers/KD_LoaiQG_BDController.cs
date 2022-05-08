using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Models;

namespace ELF.Areas.KiemDuyets.Controllers
{
    public class KD_LoaiQG_BDController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: KiemDuyets/KD_LoaiQG_BD
        public ActionResult LoaiQG()
        {
            return PartialView("LoaiQG", db.LoaiQuyenGops.ToList());
            
        }
        public ActionResult LoaiQuaTang()
        {
            return PartialView("LoaiQuaTang", db.QuaTangs.ToList());

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
