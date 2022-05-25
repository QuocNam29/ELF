using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ELF.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.VanLang;

namespace ELF.Areas.NguoiDung.Controllers
{
    public class DangNhapController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();
        ELFVanLang2021Entities model;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private UserManager<IdentityUser> _userManager2;

        public DangNhapController()
        {
        }

        public DangNhapController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            model = new ELFVanLang2021Entities();

            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(model);
            _userManager2 = new UserManager<IdentityUser>(userStore);

            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: NguoiDung/DangNhap
        public ActionResult DangNhap()
        {
            Session["Message"] = null;
            
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string mail, string matKhau)
        {
            using (var context = new ELFVanLang2021Entities())
            {
                var f_password = GetMD5(matKhau);
                var account = context.TaiKhoans.Where(acc => acc.email.Equals(mail) && acc.matKhau.Equals(f_password)).FirstOrDefault();
                bool isValid = context.TaiKhoans.Any(x => x.email.Equals(mail)
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
                    


                    if (account.NguoiDung.maP != null && account.NguoiDung.maQuan != null && 
                        account.NguoiDung.maTinh_TP != null && account.NguoiDung.diaChi != null)  {

                        string phuongThiTran = account.NguoiDung.PhuongThiTran.tenPhuong.Trim();
                        string quanHuyen = account.NguoiDung.QuanHuyen.tenQuan.Trim();
                        string tinhTP = account.NguoiDung.Tinh_ThanhPho.tenTinh_TP.Trim();
                        string diaChi = account.NguoiDung.diaChi.Trim();

                        Session["diaChiTong"] = diaChi + ", " + phuongThiTran + ", " + quanHuyen + ", " + tinhTP;
                    } else
                    {
                        Session["diaChiTong"] = "";
                    }


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
                    return RedirectToAction("Index", "BaiDangSanPhams");
                }
            }
            ModelState.AddModelError("", "Invalid email and password!!");
            Session["Message"] = "Sai email hoặc mật khẩu!!";
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


        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "DangNhap", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync2();
            if (loginInfo == null)
            {
                return RedirectToAction("DangNhap");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync2(loginInfo, UserManager);
            switch (result)
            {
                case SignInStatus.Success:
                    var check = db.TaiKhoans.FirstOrDefault(s => s.email == loginInfo.Email);
                    if (check == null)
                    {
                        return RedirectToAction("VLU_Create", "NguoiDungs", new {email = loginInfo.Email});
                    } else
                    {
                        var account = db.TaiKhoans.Where(acc => acc.email.Equals(loginInfo.Email)).FirstOrDefault();

                        Session["hoVaTen"] = account.NguoiDung.hoVaTen;
                        Session["ID"] = account.ID;
                        Session["maND"] = account.maND;
                        Session["matKhau"] = account.matKhau;
                        Session["avatar"] = account.NguoiDung.avatar;
                        Session["email"] = account.email;
                        Session["ngayTao"] = account.ngayTao;
                        Session["gioiTinh"] = account.NguoiDung.gioiTinh;



                        if (account.NguoiDung.maP != null && account.NguoiDung.maQuan != null &&
                            account.NguoiDung.maTinh_TP != null && account.NguoiDung.diaChi != null)
                        {

                            string phuongThiTran = account.NguoiDung.PhuongThiTran.tenPhuong.Trim();
                            string quanHuyen = account.NguoiDung.QuanHuyen.tenQuan.Trim();
                            string tinhTP = account.NguoiDung.Tinh_ThanhPho.tenTinh_TP.Trim();
                            string diaChi = account.NguoiDung.diaChi.Trim();

                            Session["diaChiTong"] = diaChi + ", " + phuongThiTran + ", " + quanHuyen + ", " + tinhTP;
                        }
                        else
                        {
                            Session["diaChiTong"] = "";
                        }


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
                        FormsAuthentication.SetAuthCookie(loginInfo.Email, false);
                        return RedirectToAction("Index", "BaiDangSanPhams");
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "BaiDangSanPhams");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("DangNhap", "DangNhap");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "BaiDangSanPhams");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}