using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Quanlythuquanweb.Models;
using Doanc_sharp.src.Helpers;
using Quanlythuquanweb.Models.ViewModels;

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

        //public ActionResult DatCho(int mathietbi)
        //{
        //    Thietbi thietBi = null;

        //    try
        //    {
        //        using (MySqlConnection conn = db.GetConnection())
        //        {
        //            conn.Open();

        //            string query = "SELECT Mathietbi, Tenthietbi, Madanhmuc, Giathue, Trangthai FROM thietbi WHERE Mathietbi = @mathietbi";
        //            using (MySqlCommand cmd = new MySqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@mathietbi", mathietbi);

        //                using (MySqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        thietBi = new Thietbi
        //                        {
        //                            Mathietbi = Convert.ToInt32(reader["Mathietbi"]),
        //                            Tenthietbi = reader["Tenthietbi"].ToString(),
        //                            Madanhmuc = reader["Madanhmuc"].ToString(),
        //                            Giathue = Convert.ToInt32(reader["Giathue"]),
        //                            Trangthai = reader["Trangthai"].ToString()
        //                        };
        //                    }
        //                }
        //            }
        //            if (Session["Mathanhvien"] != null)
        //            {
        //                int mathanhvien = Convert.ToInt32(Session["Mathanhvien"]);
        //                ThanhVien thanhVien = LayThongTinThanhVien(mathanhvien);
        //                ViewBag.profile = thanhVien;
        //                return View(thietBi);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Lỗi khi lấy thiết bị: " + ex.Message;
        //        return RedirectToAction("Danhsach");
        //    }

        //    if (thietBi == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(thietBi); 
        //}
        //private ThanhVien LayThongTinThanhVien(int mathanhvien)
        //{
        //    ThanhVien thanhVien = null;

        //    using (MySqlConnection conn = db.GetConnection())
        //    {
        //        conn.Open();

        //        string query = @"SELECT * FROM thanhvien WHERE Mathanhvien = @mathanhvien";
        //        using (MySqlCommand cmd = new MySqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@mathanhvien", mathanhvien);

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    thanhVien = new ThanhVien
        //                    {
        //                        Mathanhvien = Convert.ToInt32(reader["Mathanhvien"]),
        //                        HoTen = reader["Hoten"].ToString(),
        //                        DiaChi = reader["Diachi"].ToString(),
        //                        Sdt = Convert.ToInt32(reader["Sdt"]),
        //                        Email = reader["Email"].ToString(),
        //                        Ngaydangky = Convert.ToDateTime(reader["Ngaydangky"])
        //                    };
        //                }
        //            }
        //        }
        //    }

        //    return thanhVien;
        //}
        public ActionResult DatCho(int mathietbi)
        {
            Thietbi thietBi = null;
            ThanhVien thanhVien = null;

            try
            {
                using (MySqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT Mathietbi, Tenthietbi, Madanhmuc, Giathue, Trangthai FROM thietbi WHERE Mathietbi = @mathietbi";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@mathietbi", mathietbi);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                thietBi = new Thietbi
                                {
                                    Mathietbi = Convert.ToInt32(reader["Mathietbi"]),
                                    Tenthietbi = reader["Tenthietbi"].ToString(),
                                    Madanhmuc = reader["Madanhmuc"].ToString(),
                                    Giathue = Convert.ToInt32(reader["Giathue"]),
                                    Trangthai = reader["Trangthai"].ToString()
                                };
                            }
                        }
                    }

                    if (Session["Mathanhvien"] != null)
                    {
                        int mathanhvien = Convert.ToInt32(Session["Mathanhvien"]);
                        string queryProfile = @"SELECT * FROM thanhvien WHERE Mathanhvien = @mathanhvien";
                        MySqlCommand cmdProfile = new MySqlCommand(queryProfile, conn);
                        cmdProfile.Parameters.AddWithValue("@mathanhvien", mathanhvien);
                        using (MySqlDataReader reader = cmdProfile.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                thanhVien = new ThanhVien
                                {
                                    Mathanhvien = Convert.ToInt32(reader["Mathanhvien"]),
                                    HoTen = Convert.ToString(reader["Hoten"]),
                                    DiaChi = Convert.ToString(reader["Diachi"]),
                                    Sdt = Convert.ToInt32(reader["Sdt"]),
                                    Email = Convert.ToString(reader["Email"]),
                                    Ngaydangky = Convert.ToDateTime(reader["Ngaydangky"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi lấy thiết bị: " + ex.Message;
                return RedirectToAction("Danhsach");
            }

            if (thietBi == null)
            {
                return HttpNotFound();
            }

            // Tạo ViewModel và truyền vào View
            var viewModel = new DatChoViewModel
            {
                Thietbi = thietBi,
                ThanhVien = thanhVien
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult XacNhanDatCho(int mathietbi, int soluong, DateTime ngaymuon) 
        {
            int mathanhvien = Convert.ToInt32(Session["Mathanhvien"]);
            Thietbi thietBi = null;
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string queryInsertDatcho = "INSERT INTO datcho(Mathanhvien, Thoigiandat, Trangthai, is_delete) VALUES (@mathanhvien, @thoigiandat, @trangthai, @isdelete); " +
                    "SELECT LAST_INSERT_ID()";
                using (MySqlCommand cmdInsert = new MySqlCommand(queryInsertDatcho, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@mathanhvien", mathanhvien);
                    cmdInsert.Parameters.AddWithValue("@thoigiandat", ngaymuon);
                    cmdInsert.Parameters.AddWithValue("@trangthai", "Đang xác nhận");
                    cmdInsert.Parameters.AddWithValue("@isdelete", false);
                    int madatcho = Convert.ToInt32(cmdInsert.ExecuteScalar());

                    string queryInsertChitietdatcho = "INSERT INTO chitietdatcho(Madatcho, Mathietbi, Soluong) VALUES (@madatcho, @mathietbi, @soluong); ";
                   
                    using (MySqlCommand cmdInsertdetail = new MySqlCommand(queryInsertChitietdatcho, conn))
                    {
                        cmdInsertdetail.Parameters.AddWithValue("@madatcho", madatcho);
                        cmdInsertdetail.Parameters.AddWithValue("@mathietbi", mathietbi);
                        cmdInsertdetail.Parameters.AddWithValue("@soluong", soluong);
                        cmdInsertdetail.ExecuteNonQuery();
                    }
                    string queryUpdateThietBi = @"UPDATE thietbi 
                                          SET Trangthai = 'Đang thuê' 
                                          WHERE Mathietbi = @mathietbi";

                    using (MySqlCommand cmdUpdateThietBi = new MySqlCommand(queryUpdateThietBi, conn))
                    {
                        cmdUpdateThietBi.Parameters.AddWithValue("@mathietbi", mathietbi);
                        cmdUpdateThietBi.ExecuteNonQuery();
                    }
                }
            }
            return View("ThanhCong");
        }
        public ActionResult ThanhCong()
        {
            return View();
        }

    }
}
    

