using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Doanc_sharp.src.Helpers;
using Quanlythuquanweb.Models;

namespace Quanlythuquanweb.Controllers
{
    public class HomeController : Controller
    {
        private DbConnection db = new DbConnection();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UserProfile()
        {
            if (Session["Mathanhvien"] != null)
            {
                int mathanhvien = Convert.ToInt32(Session["Mathanhvien"]);
                MySqlConnection conn = db.GetConnection();
                conn.Open();
                string queryLSHD = @"SELECT * FROM lichsuhoatdong WHERE Mathanhvien = @mathanhvien";
                MySqlCommand cmdLSHD = new MySqlCommand(queryLSHD, conn);
                cmdLSHD.Parameters.AddWithValue("@mathanhvien", mathanhvien);

                List<LSHoatDong> lshd = new List<LSHoatDong>();
                using (MySqlDataReader reader = cmdLSHD.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LSHoatDong log = new LSHoatDong
                        {
                            mathanhvien = Convert.ToInt32(reader["Mathanhvien"]),
                            mahoatdong = Convert.ToInt32(reader["Mahoatdong"]),
                            loai = reader["Loai"].ToString(),
                            thoigian = Convert.ToDateTime(reader["Thoigian"]),
                            chitiet = reader["Chitiet"].ToString(),
                            is_delete = Convert.ToInt32(reader["is_delete"])
                        };
                        lshd.Add(log);
                    }
                }
                conn.Close();
                ViewBag.lshd = lshd;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }
    }
}