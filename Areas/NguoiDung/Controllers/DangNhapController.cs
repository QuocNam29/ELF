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
        ELFDatabaseEntities model;
        public DangNhapController()
        {
            model = new ELFDatabaseEntities();
        }
        // GET: NguoiDung/DangNhap
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string matKhau)
        {
            using (var context = new ELFDatabaseEntities())
            {
                var f_password = GetMD5(matKhau);
                var account = context.TaiKhoans.Where(acc => acc.email.Equals(email) && acc.matKhau.Equals(f_password)).FirstOrDefault();
                bool isValid = context.TaiKhoans.Any(x => x.email.Equals(email)
                && x.matKhau.Equals(f_password));
                if (isValid)
                {
                    Session["hoVaTen"] = account.NguoiDung.hoVaTen;
                    Session["email"] = account.email;
                    Session["maND"] = account.maND;
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("Index", "TrangChu");
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
            return RedirectToAction("Index", "DangNhap");
        }
    }
}