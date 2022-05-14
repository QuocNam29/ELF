using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ELF.Areas.NguoiDung.Middleware;
using ELF.Models;
namespace ELF.Areas.NguoiDung.Controllers
{
    public class NguoiDungsController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        // GET: NguoiDung/NguoiDungs
        [LoginVerification]
        public ActionResult Index()
        {
            var nguoiDungs = db.NguoiDungs.Include(n => n.PhuongThiTran);
            return View(nguoiDungs.ToList());
        }

        // GET: NguoiDung/NguoiDungs/Details/5
        [LoginVerification]
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

        public ActionResult GetQuanHuyenList (int? maTinh_TP)
        {
            List<QuanHuyen> quanHuyens = db.QuanHuyens.Where(x => x.maTP == maTinh_TP).ToList();
            ViewBag.maQuan = new SelectList(quanHuyens, "maQuan", "tenQuan");
            return PartialView("DisplayQuanHuyen");
        }
        public ActionResult GetPhuongThiTranList(int? maQuan)
        {
            List<PhuongThiTran> phuongThiTrans = db.PhuongThiTrans.Where(x => x.maQuan == maQuan).ToList();
            ViewBag.maP = new SelectList(phuongThiTrans, "maPhuong", "tenPhuong");
            return PartialView("DisplayPhuongThiTran");
        }

        public JsonResult GetQHList(int maTinh_TP)
        {            
            return Json(db.QuanHuyens.Where(q => q.maTP == 
            maTinh_TP).Select(q => new
            {
                maQuan = q.maQuan,
                tenQuan = q.tenQuan
            }).ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetPList(int maQuan)
        {
            return Json(db.PhuongThiTrans.Where(p => p.maQuan ==
            maQuan).Select(p => new
            {
                maPhuong = p.maPhuong,
                tenPhuong = p.tenPhuong
            }).ToList(), JsonRequestBehavior.AllowGet);

        }

        // POST: NguoiDung/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maND,hoVaTen,gioiTinh,dienThoai,maTinh_TP,maQuan,maP,diaChi,avatar,ngaySinh,ghiChu")] Models.NguoiDung nguoiDung, string email, string matKhau, string xacNhanMatKhau, string gioiTinh)
        {
            if (string.IsNullOrEmpty(gioiTinh))
            {
                ModelState.AddModelError("loigioitinh", "Bạn chưa chọn giới tính");
            }
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("loiemail", "Bạn chưa nhập email");
            }
            else
            {
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(email))
                {
                    ModelState.AddModelError("loiemail", "Email chưa đúng định dạng, vui lòng kiểm tra lại");
                }
            }
            if (string.IsNullOrEmpty(matKhau))
            {
                ModelState.AddModelError("loimatkhau", "Bạn chưa nhập mật khẩu");
            }
            if (string.IsNullOrEmpty(xacNhanMatKhau))
            {
                ModelState.AddModelError("loixacnhanmatkhau", "Bạn chưa xác nhận mật khẩu");
            }
            if (ModelState.IsValid)
            {               
                var check = db.TaiKhoans.FirstOrDefault(s => s.email == email);
                if (check == null)
                {
                    if (GetMD5(matKhau) == GetMD5(xacNhanMatKhau))
                    {
                        nguoiDung.gioiTinh = int.Parse(gioiTinh);
                        db.NguoiDungs.Add(nguoiDung);
                        db.SaveChanges();

                        int maND = nguoiDung.maND;
                        TaiKhoan taiKhoan = new TaiKhoan();
                        taiKhoan.email = email;
                        taiKhoan.matKhau = GetMD5(matKhau);
                        taiKhoan.xacNhanMatKhau = GetMD5(xacNhanMatKhau);
                        taiKhoan.trangThai = true;
                        taiKhoan.maND = maND;
                        taiKhoan.ngayTao = DateTime.Now;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.TaiKhoans.Add(taiKhoan);
                        db.SaveChanges();

                        int id_taiKhoan = taiKhoan.ID;
                        ChucNangTaiKhoan chucNangTaiKhoan = new ChucNangTaiKhoan();
                        chucNangTaiKhoan.ID_TaiKhoan = id_taiKhoan;
                        chucNangTaiKhoan.maLoaiTK = 2;
                        db.ChucNangTaiKhoans.Add(chucNangTaiKhoan);
                        db.SaveChanges();

                        return RedirectToAction("DangNhap", "DangNhap");
                    }
                    else
                    {
                        ModelState.AddModelError("loixacnhanmatkhau", "Xác nhận mật khẩu thất bại, vui lòng kiểm tra lại");
                        ViewBag.maTinh_TP = new SelectList(GetTinh_ThanhPhosList(), "maTinh_TP", "tenTinh_TP", nguoiDung.maTinh_TP);
                        return View(nguoiDung);
                    }
                }
                else
                {
                    ModelState.AddModelError("loiemail", "Địa chỉ email này đã được đăng ký rồi");
                    ViewBag.maTinh_TP = new SelectList(GetTinh_ThanhPhosList(), "maTinh_TP", "tenTinh_TP", nguoiDung.maTinh_TP);
                    return View(nguoiDung);
                }
            }
                      
            ViewBag.maTinh_TP = new SelectList(GetTinh_ThanhPhosList(), "maTinh_TP", "tenTinh_TP", nguoiDung.maTinh_TP);
            return View(nguoiDung);
        }

        // GET: NguoiDung/NguoiDungs/Edit/5
        [LoginVerification]
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

            ViewBag.maTinh_TP = new SelectList(GetTinh_ThanhPhosList(), "maTinh_TP", "tenTinh_TP", nguoiDung.maTinh_TP);
            TempData["maQuan"] = nguoiDung.maQuan;
            ViewBag.maP = new SelectList(db.PhuongThiTrans, "maPhuong", "tenPhuong", nguoiDung.maP);
            return View(nguoiDung);
        }

        // POST: NguoiDung/NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maND,hoVaTen,gioiTinh,dienThoai,maTinh_TP,maQuan,maP,diaChi,avatar,ngaySinh,ghiChu")] Models.NguoiDung nguoiDung, HttpPostedFileBase avt, int gioiTinh, FormCollection formcollection)
        {
            int maND_ss = int.Parse(Session["maND"].ToString());
            if (ModelState.IsValid)
            {
                string oldfilePath = nguoiDung.avatar;
                if (avt != null && avt.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(avt.FileName);
                    string path = System.IO.Path.Combine(
                    Server.MapPath("~/images/"), fileName);
                    avt.SaveAs(path);
                    nguoiDung.avatar = "~/images/" + avt.FileName;
                    string fullPath = Request.MapPath(oldfilePath);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                else
                {           
                    if (Session["avatar"] != null)
                    {
                        nguoiDung.avatar = Session["avatar"].ToString();
                    } else
                    {
                        nguoiDung.avatar = null;
                    }
                }
                nguoiDung.maND = maND_ss;
                nguoiDung.gioiTinh = gioiTinh;
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();

                if (nguoiDung.maP != null && nguoiDung.maQuan != null &&
                    nguoiDung.maTinh_TP != null && nguoiDung.diaChi != null)
                {

                    string phuongThiTran = formcollection["tenPhuong"].Trim();
                    string quanHuyen = formcollection["tenQuan"].Trim();
                    string tinhTP = formcollection["tenTinh_TP"].Trim();
                    string diaChi = nguoiDung.diaChi.Trim();

                    Session["diaChiTong"] = diaChi + ", " + phuongThiTran + ", " + quanHuyen + ", " + tinhTP;
                }
                else
                {
                    Session["diaChiTong"] = "";
                }

                Session["hoVaTen"] = nguoiDung.hoVaTen;
                Session["avatar"] = nguoiDung.avatar;
                Session["gioiTinh"] = nguoiDung.gioiTinh;
                if (nguoiDung.gioiTinh == 1)
                {
                    Session["loaiGioiTinh"] = "Nam";
                }
                else if (nguoiDung.gioiTinh == 0)
                {
                    Session["loaiGioiTinh"] = "Nữ";
                }
                else
                {
                    Session["loaiGioiTinh"] = "Khác";
                }
                return RedirectToAction("Details", "NguoiDungs", new { id = maND_ss });
            }
            ViewBag.maP = new SelectList(db.PhuongThiTrans, "maPhuong", "tenPhuong", nguoiDung.maP);
            ViewBag.maQuan = new SelectList(db.QuanHuyens, "maQuan", "tenQuan", nguoiDung.maQuan);
            ViewBag.maTinh_TP = new SelectList(db.Tinh_ThanhPho, "maTinh_TP", "tenTinh_TP", nguoiDung.maTinh_TP);
            return View(nguoiDung);
        }

        // GET: NguoiDung/NguoiDungs/Delete/5
        [LoginVerification]
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

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [LoginVerification]
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
