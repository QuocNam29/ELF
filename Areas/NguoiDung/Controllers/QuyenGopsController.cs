using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.NguoiDung.Middleware;
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    [LoginVerification]
    public class QuyenGopsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/QuyenGops
        public ActionResult Index()
        {
            int mand = int.Parse(Session["maND"].ToString());
            var quyenGops = db.QuyenGops.Include(q => q.LoaiQuyenGop).Include(q => q.NguoiDung).Where(q => q.maND == mand).OrderByDescending(q => q.maQG);
            return View(quyenGops.ToList());
        }

        

        // GET: NguoiDung/QuyenGops/Create
        public ActionResult Create(int maLQG)
        {
            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai", maLQG);
            return View();
        }

        // POST: NguoiDung/QuyenGops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maQG,maND,ngayQG,maLQG,soLuong,donVi,trangThai,hinhAnh,ghiChu")] QuyenGop quyenGop, HttpPostedFileBase img)
        {
            int mand = int.Parse(Session["maND"].ToString());

            if (ModelState.IsValid)
            {
                try
                {
                    if (img != null)
                    {
                        string extension = Path.GetExtension(img.FileName);

                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                        {
                            string time = DateTime.Now.ToString("yymmssfff");
                            string fileName = Path.GetFileName(img.FileName);
                            string filePath = "~/images/hinhQuyenGops/" + time + fileName;
                            img.SaveAs(Server.MapPath(filePath));
                            db.QuyenGops.Add(new QuyenGop
                            {
                                maQG = quyenGop.maQG,
                                maND = mand,
                                ngayQG = quyenGop.ngayQG,
                                maLQG = quyenGop.maLQG,
                                soLuong = quyenGop.soLuong,
                                donVi = quyenGop.donVi,
                                trangThai = "Chờ duyệt",
                                hinhAnh = "~/images/hinhQuyenGops/" + time + img.FileName,
                                ghiChu = quyenGop.ghiChu
                            });
                            db.DiemTichLuys.Add(new DiemTichLuy
                            {
                                maND = int.Parse(Session["maND"].ToString()),
                                thoiGian = DateTime.Now,
                                maQG = quyenGop.maQG,
                                diem = 10
                            });
                            db.SaveChanges();
                            return RedirectToAction("Index", "LoaiQuyenGops");
                        } else
                        {
                            ViewBag.FileStatus = "Định dạng file không hợp lệ!";
                        }
                    } else
                    {
                        ViewBag.FileStatus = "Bạn chưa chọn hình ảnh";
                    }
                }
                catch (Exception)
                {

                }
              
            }

            ViewBag.maLQG = new SelectList(db.LoaiQuyenGops, "maLQG", "tenLoai", quyenGop.maLQG);
            return View(quyenGop);
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
