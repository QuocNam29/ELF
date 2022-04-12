using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ELF.Models;

namespace ELF.Areas.NguoiDung.Controllers
{
    public class DangNhapController : Controller
    {
        ELFVanLang2021Entities model;
        
        public DangNhapController()
        {
            model = new ELFVanLang2021Entities();
        }
        // GET: NguoiDung/DangNhap
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string email, string matKhau)
        {
            using (var context = new ELFVanLang2021Entities())
            {
                var f_password = GetMD5(matKhau);
                var account = context.TaiKhoans.Where(acc => acc.email.Equals(email) && acc.matKhau.Equals(f_password)).FirstOrDefault();
                bool isValid = context.TaiKhoans.Any(x => x.email.Equals(email)
                && x.matKhau.Equals(f_password));
                if (isValid)
                {
                    Session["hoVaTen"] = account.NguoiDung.hoVaTen;
                    Session["ID"] = account.ID;
                    Session["maND"] = account.maND;
                    Session["matKhau"] = account.matKhau;
                    Session["avatar"] = account.NguoiDung.avatar;
                    Session["email"] = account.email;
                    Session["ngayTao"] = account.ngayTao;
                    Session["gioiTinh"] = account.NguoiDung.gioiTinh;

                    if (account.NguoiDung.gioiTinh == 1)
                    {
                        Session["loaiGioiTinh"] = "Nam";
                    }
                    else if (account.NguoiDung.gioiTinh == 0)
                    {
                        Session["loaiGioiTinh"] = "Nữ";
                    }
                    else
                    {
                        Session["loaiGioiTinh"] = "Khác";
                    }
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("Index", "BaiDangSanPhams");
                }
            }
            ModelState.AddModelError("", "Invalid email and password!!");
            Session["Message"] = "Sai Email hoặc mật khẩu!!";
            return View();
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

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("DangNhap", "DangNhap");
        }
    }
}