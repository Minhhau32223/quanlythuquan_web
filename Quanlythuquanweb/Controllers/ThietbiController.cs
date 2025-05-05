using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Quanlythuquanweb.Models;
using Doanc_sharp.src.Helpers;

namespace Quanlythuquanweb.Controllers
{
    public class ThietbiController : Controller
    {
        // GET: Thietbi
        private DbConnection db = new DbConnection();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Danhsach()
        {
            List<Thietbi> danhSachThietbi = new List<Thietbi>();

            try
            {
                MySqlConnection conn = db.GetConnection();
                conn.Open();

                string query = "SELECT Mathietbi, Tenthietbi, Madanhmuc, Giathue, Trangthai FROM thietbi";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Thietbi tb = new Thietbi
                        {
                            Mathietbi = Convert.ToInt32(reader["Mathietbi"]),
                            Tenthietbi = reader["Tenthietbi"].ToString(),
                            Madanhmuc = reader["Madanhmuc"].ToString(),
                            Giathue = Convert.ToInt32(reader["Giathue"]),
                            Trangthai = reader["Trangthai"].ToString()
                        };
                        danhSachThietbi.Add(tb);
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi lấy dữ liệu thiết bị: " + ex.Message;
            }

            return View(danhSachThietbi);
        }

        public ActionResult DatCho(int mathietbi)
        {
            // xử lý đặt chỗ ở đây
            ViewBag.Message = "Thiết bị " + mathietbi + " đã được đặt chỗ thành công.";
            return RedirectToAction("Danhsach");
        }

    }
}
    

