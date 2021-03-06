using ELF.Areas.NguoiDung.Middleware;
using ELF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ELF.Models.viewModels;

namespace ELF.Areas.NguoiDung.Controllers
{
    [LoginVerification]
    public class QuizzController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();

        [HttpGet]
        public ActionResult SelectQuizz()
        {
            int mand = int.Parse(Session["maND"].ToString());

            var dateCriteria = DateTime.Now.Date.AddDays(-2);
            var query = db.KetQuas.Where(m => m.ngayLamQuiz >= dateCriteria && m.maND == mand);
            if(query.Count() >= 3)
            {
                TempData["ErrorAttempts"] = "Bạn đã hết giới hạn làm bài quiz của mình 😥. Xin hãy đợi 3 ngày kể từ ngày làm bài quiz để được làm bài tiếp.";
            }
            QuizVM quiz = new viewModels.QuizVM();
            quiz.ListOfQuizz = db.ChuDeBaiQuizs.Where(q => q.trangThai != "Đã xóa").Select(q => new SelectListItem
            {
                Text = q.tenChuDe,
                Value = q.maChuDe.ToString()

            }).ToList();

            return View(quiz);
        }

        [HttpPost]
        public ActionResult SelectQuizz(QuizVM quiz)
        {
            QuizVM quizSelected = db.ChuDeBaiQuizs.Where(q => q.maChuDe == quiz.QuizID && q.trangThai != "Đã xóa").Select(q => new QuizVM
            {
                QuizID = q.maChuDe,
                QuizName = q.tenChuDe,


            }).FirstOrDefault();

            if (quizSelected != null)
            {
                Session["SelectedQuiz"] = quizSelected;

                return RedirectToAction("QuizTest");
            }

            return View();
        }

        [HttpGet]
        public ActionResult QuizTest()
        {
            int mand = int.Parse(Session["maND"].ToString());

            var dateCriteria = DateTime.Now.Date.AddDays(-2);
            var query = db.KetQuas.Where(m => m.ngayLamQuiz >= dateCriteria && m.maND == mand);
            if (query.Count() >= 3)
            {
                TempData["ErrorAttempts"] = "Bạn đã hết giới hạn làm bài quiz của mình 😥. Xin hãy đợi 3 ngày kể từ ngày làm bài quiz để được làm bài tiếp.";
                return RedirectToAction("SelectQuizz", "Quizz");
            }


            QuizVM quizSelected = Session["SelectedQuiz"] as QuizVM;
            TempData["maChuDe"] = quizSelected.QuizID;
            TempData["tenChuDe"] = quizSelected.QuizName;        

            IQueryable<QuestionVM> questions = null;

            if (quizSelected != null)
            {
                questions = db.CauHois.Where(q => q.maChuDe == quizSelected.QuizID)
                   .Select(q => new QuestionVM
                   {
                       QuestionID = q.maCauHoi,
                       QuestionText = q.noiDungCauHoi,
                       Choices = q.DapAns.Select(c => new ChoiceVM
                       {
                           ChoiceID = c.maDapAn,
                           ChoiceText = c.NoiDungDapAn
                       }).ToList()

                   }).OrderBy(q => Guid.NewGuid()).Take(10).AsQueryable();

            }

            return View(questions);
        }

        [HttpPost]
        public ActionResult QuizTest(List<QuizAnswersVM> resultQuiz)
        {
            List<QuizAnswersVM> finalResultQuiz = new List<viewModels.QuizAnswersVM>();

            foreach (QuizAnswersVM answser in resultQuiz)
            {
                QuizAnswersVM result = db.DapAns.Where(a => a.maCauHoi == answser.QuestionID && a.dapAn1 == true).Select(a => new QuizAnswersVM
                {
                    QuestionID = a.maCauHoi,
                    AnswerQ = a.NoiDungDapAn,
                    isCorrect = (answser.AnswerQ.ToLower().Equals(a.NoiDungDapAn.ToLower()))

                }).FirstOrDefault();

                finalResultQuiz.Add(result);
            }

            return Json(new { result = finalResultQuiz }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertKetQuaBaiQuiz(KetQua ketQua)
        {
            using (ELFVanLang2021Entities entities = new ELFVanLang2021Entities())
            {
                entities.KetQuas.Add(ketQua);
                entities.SaveChanges();

                int maKQ = ketQua.maKQ;
                TempData["maKQ"] = maKQ;
            }

            return Json(ketQua);
        }

        [HttpPost]
        public ActionResult InsertDiemTichLuys(DiemTichLuy diemTichLuy)
        {
            using (ELFVanLang2021Entities entities = new ELFVanLang2021Entities())
            {
                entities.DiemTichLuys.Add(diemTichLuy);
                entities.SaveChanges();
            }

            return Json(diemTichLuy);
        }
        
    }
}