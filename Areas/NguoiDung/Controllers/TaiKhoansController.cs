using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.NguoiDung.Middleware;
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    [LoginVerification]
    public class TaiKhoansController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

       

        

        // GET: NguoiDung/TaiKhoans/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", taiKhoan.maND);
            return View(taiKhoan);
        }

        // POST: NguoiDung/TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,email,maND,matKhau,xacNhanMatKhau,ngayTao,trangThai")] TaiKhoan taiKhoan, string matKhauHienTai, string matKhauMoi, string xacNhanMatKhau)
        {
            Session["Message_DoiMK"] = null;
            if (ModelState.IsValid)
            {
               
                if (GetMD5(matKhauHienTai) == Session["matKhau"].ToString())
                {
                    if (matKhauMoi == xacNhanMatKhau)
                    {
                        taiKhoan.matKhau = GetMD5(matKhauMoi);
                        taiKhoan.xacNhanMatKhau= GetMD5(xacNhanMatKhau);
                        
                        taiKhoan.maND = int.Parse(Session["maND"].ToString());
                        taiKhoan.trangThai = true;
                        taiKhoan.email = Session["email"].ToString();
                        taiKhoan.ngayTao = DateTime.Parse(Session["ngayTao"].ToString());
                        db.Entry(taiKhoan).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index_TrangCaNhan", "BaiDangSanPhams", new { maND = int.Parse(Session["maND"].ToString()) });
                    }
                    else
                    {
                        ViewBag.error = "Xác nhận mật khẩu không chính xác";
                        Session["Message_DoiMK"] = "Xác nhận mật khẩu không chính xác";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "Mật khẩu hiện tại không chính xác";
                    Session["Message_DoiMK"] = "Mật khẩu hiện tại không chính xác";
                    return View();
                }

            }
            ViewBag.maND = new SelectList(db.NguoiDungs, "maND", "hoVaTen", taiKhoan.maND);
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
