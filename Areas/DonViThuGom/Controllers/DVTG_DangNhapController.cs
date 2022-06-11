using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ELF.Models;

namespace ELF.Areas.DonViThuGom.Controllers
{
    public class DVTG_DangNhapController : Controller
    {
        ELFVanLang2021Entities model;
        // GET: DVTG/DVTG_DangNhap
        public DVTG_DangNhapController()
        {
            model = new ELFVanLang2021Entities();
        }
        public ActionResult DVTG_DangNhap()
        {
            Session["Message"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult DVTG_DangNhap(string mail, string matKhau)
        {
            using (var context = new ELFVanLang2021Entities())
            {
                var f_password = GetMD5(matKhau);
                var account = context.TaiKhoans.Where(acc => acc.email.Equals(mail) && acc.matKhau.Equals(f_password)).FirstOrDefault();
                bool isValid = context.TaiKhoans.Any(x => x.email.Equals(mail)
                && x.matKhau.Equals(f_password));


                if (isValid)
                {
                    var check_loaiTK = context.ChucNangTaiKhoans.Where(check => check.ID_TaiKhoan.Equals(account.ID) && check.maLoaiTK == 4).FirstOrDefault();
                    if (check_loaiTK != null)
                    {
                        Session["hoVaTen"] = account.NguoiDung.hoVaTen;
                        Session["ID"] = account.ID;
                        Session["maND"] = account.maND;
                        Session["matKhau"] = account.matKhau;
                        Session["avatar"] = account.NguoiDung.avatar;
                        Session["email"] = account.email;
                        Session["ngayTao"] = account.ngayTao;
                        Session["gioiTinh"] = account.NguoiDung.gioiTinh;
                        Session["maDVTG"] = account.NguoiDung.maDVTG;

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
                        FormsAuthentication.SetAuthCookie(mail, false);
                        return RedirectToAction("Index", "DonThuGoms");
                    }
                    ModelState.AddModelError("", "Invalid kiemDuyet!!");
                    Session["Message_DVTG"] = "* Đây không phải tài khoản Đơn Vị Thu Gom!!";
                    return View();
                }

                ModelState.AddModelError("", "Invalid email and password!!");
                Session["Message_DVTG"] = "* Sai email hoặc mật khẩu!!";
                return View();
            }
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

        public ActionResult DVTG_LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("DVTG_DangNhap", "DVTG_DangNhap");
        }
    }
}