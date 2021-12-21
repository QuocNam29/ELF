﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ELF.Models;
namespace ELF.Areas.NguoiDung.Controllers
{
    public class NguoiDungsController : Controller
    {
        private ELFDatabaseEntities db = new ELFDatabaseEntities();

        // GET: NguoiDung/NguoiDungs
        public ActionResult Index()
        {
            var nguoiDungs = db.NguoiDungs.Include(n => n.PhuongThiTran);
            return View(nguoiDungs.ToList());
        }

        // GET: NguoiDung/NguoiDungs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: NguoiDung/NguoiDungs/Create
        public ActionResult Create()
        {
            
            ViewBag.maTinh_TP = new SelectList(GetTinh_ThanhPhosList(), "maTinh_TP", "tenTinh_TP");
            return View();
        }
        public List<Tinh_ThanhPho> GetTinh_ThanhPhosList()
        {
            List<Tinh_ThanhPho> tinh_ThanhPhos = db.Tinh_ThanhPho.ToList();
            return tinh_ThanhPhos;
        }
        public ActionResult GetQuanHuyenList (int maTinh_TP)
        {
            List<QuanHuyen> quanHuyens = db.QuanHuyens.Where(x => x.maTP == maTinh_TP).ToList();
            ViewBag.maQuan = new SelectList(quanHuyens, "maQuan", "tenQuan");
            return PartialView("HienThiQuan");
        }
        public ActionResult GetPhuongThiTranList(int maQuan)
        {
            List<PhuongThiTran> phuongThiTrans = db.PhuongThiTrans.Where(x => x.maQuan == maQuan).ToList();
            ViewBag.maP = new SelectList(phuongThiTrans, "maPhuong", "tenPhuong");
            return PartialView("HienThiPhuong");
        }



        // POST: NguoiDung/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maND,hoVaTen,gioiTinh,dienThoai,maTinh_TP,maQuan,maP,diaChi,avatar,ngaySinh,ghiChu")] Models.NguoiDung nguoiDung, string email, string matKhau, string xacNhanMatKhau, string gioiTinh)
        {
            if (ModelState.IsValid)
            {
                var check = db.TaiKhoans.FirstOrDefault(s => s.email == email);
                if (check == null)
                {
                    
                    nguoiDung.gioiTinh = int.Parse(gioiTinh);
                    db.NguoiDungs.Add(nguoiDung);
                    db.SaveChanges();

                    int maND = nguoiDung.maND;
                    TaiKhoan taiKhoan = new TaiKhoan();
                    taiKhoan.email = email;
                    taiKhoan.matKhau = GetMD5(matKhau);
                    taiKhoan.xacNhanMatKhau = xacNhanMatKhau;
                    taiKhoan.trangThai = true;
                    taiKhoan.maND = maND;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();

                    ChucNangTaiKhoan chucNangTaiKhoan = new ChucNangTaiKhoan();
                    chucNangTaiKhoan.Email = email;
                    chucNangTaiKhoan.maLoaiTK = 2;
                    db.ChucNangTaiKhoans.Add(chucNangTaiKhoan);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.error = "Địa chỉ email này đã được đăng ký rồi";
                    return View();
                }
                return RedirectToAction("Index");
            }
            ViewBag.maTinh_TP = new SelectList(db.Tinh_ThanhPho, "maTinh_TP", "tenTinh_TP");
            ViewBag.maQuan = new SelectList(db.QuanHuyens, "maQuan", "tenQuan");
            ViewBag.maP = new SelectList(db.PhuongThiTrans, "maPhuong", "tenPhuong", nguoiDung.maP);
            return View(nguoiDung);
        }

        // GET: NguoiDung/NguoiDungs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }

            ViewBag.maP = new SelectList(db.PhuongThiTrans, "maPhuong", "tenPhuong", nguoiDung.maP);
            ViewBag.maQuan = new SelectList(db.QuanHuyens, "maQuan", "tenQuan", nguoiDung.maQuan);
            ViewBag.maTinh_TP = new SelectList(db.Tinh_ThanhPho, "maTinh_TP", "tenTinh_TP", nguoiDung.maTinh_TP);
            return View(nguoiDung);
        }

        // POST: NguoiDung/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maND,hoVaTen,gioiTinh,dienThoai,maTinh_TP,maQuan,maP,diaChi,avatar,ngaySinh,ghiChu")] Models.NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maP = new SelectList(db.PhuongThiTrans, "maPhuong", "tenPhuong", nguoiDung.maP);
            ViewBag.maQuan = new SelectList(db.QuanHuyens, "maQuan", "tenQuan", nguoiDung.maQuan);
            ViewBag.maTinh_TP = new SelectList(db.Tinh_ThanhPho, "maTinh_TP", "tenTinh_TP", nguoiDung.maTinh_TP);
            return View(nguoiDung);
        }

        // GET: NguoiDung/NguoiDungs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDung/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            db.NguoiDungs.Remove(nguoiDung);
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
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}
