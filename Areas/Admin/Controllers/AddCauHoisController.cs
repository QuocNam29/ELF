using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using ELF.Models;
using System;
using System.Linq;

namespace ELF.Areas.Admin.Controllers
{
    public class AddCauHoisController : Controller
    {
        private ELFVanLang2021Entities db = new ELFVanLang2021Entities();
        // GET: Admin/AddCauHois
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();                                                                                            
                          
                        }
                    }
                }
                //Insert records to database table.
                foreach (DataRow row in dt.Rows)
                {
                    db.CauHois.Add(new CauHoi
                    {
                        noiDungCauHoi = row["noiDungCauHoi"].ToString(),
                        ngayTao = System.DateTime.Now,
                        ngayThayDoi = System.DateTime.Now,
                        maChuDe = int.Parse(row["maChuDe"].ToString())
                    });
                    db.SaveChanges();
                    int lastProductId = db.CauHois.Max(item => item.maCauHoi);

                    db.DapAns.Add(new DapAn
                    {
                        maCauHoi = lastProductId,
                        NoiDungDapAn = row["dapAnA"].ToString(),
                        dapAn1 = bool.Parse(row["ATrue"].ToString())

                    });
                    db.DapAns.Add(new DapAn
                    {
                        maCauHoi = lastProductId,
                        NoiDungDapAn = row["dapAnB"].ToString(),
                        dapAn1 = bool.Parse(row["BTrue"].ToString())

                    });
                    db.DapAns.Add(new DapAn
                    {
                        maCauHoi = lastProductId,
                        NoiDungDapAn = row["dapAnC"].ToString(),
                        dapAn1 = bool.Parse(row["CTrue"].ToString())

                    });
                    db.DapAns.Add(new DapAn
                    {
                        maCauHoi = lastProductId,
                        NoiDungDapAn = row["dapAnD"].ToString(),
                        dapAn1 = bool.Parse(row["DTrue"].ToString())

                    });
                }
                db.SaveChanges();

            }

            return View();
        }
    }
}