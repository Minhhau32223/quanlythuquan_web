﻿using System;
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



                string queryProfile = @"SELECT * FROM thanhvien WHERE Mathanhvien = @mathanhvien";
                MySqlCommand cmdProfile = new MySqlCommand(queryProfile, conn);
                cmdProfile.Parameters.AddWithValue("@mathanhvien", mathanhvien);
                using (MySqlDataReader reader = cmdProfile.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ThanhVien tv = new ThanhVien
                        {
                            Mathanhvien = Convert.ToInt32(reader["Mathanhvien"]),
                            HoTen = Convert.ToString(reader["Hoten"]),
                            DiaChi = Convert.ToString(reader["Diachi"]),
                            Sdt = Convert.ToInt32(reader["Sdt"]),
                            Email = Convert.ToString(reader["Email"]),
                            Ngaydangky = Convert.ToDateTime(reader["Ngaydangky"])
                        };
                        ViewBag.profile = tv;
                    }
                    else ViewBag.profile = null;
                }


                string queryLSDC = @"
                    SELECT 
                        dc.Madatcho,
                        dc.Thoigiandat,
                        ct.Mathietbi,
                        tb.Tenthietbi,
                        tb.Giathue,
                        ct.soluong,
                        dc.Trangthai
                    FROM datcho AS dc
                    JOIN chitietdatcho AS ct ON dc.Madatcho = ct.Madatcho
                    JOIN thietbi AS tb ON ct.Mathietbi = tb.Mathietbi
                    WHERE dc.Mathanhvien = @mathanhvien
                      AND dc.is_delete = 0
                      AND tb.is_delete = 0
                    ORDER BY dc.Thoigiandat DESC;
                    ";

                MySqlCommand cmdLSDC = new MySqlCommand(queryLSDC, conn);
                cmdLSDC.Parameters.AddWithValue("@mathanhvien", mathanhvien);

                List<LichSuDatCho> lsdc = new List<LichSuDatCho>();
                using (MySqlDataReader reader = cmdLSDC.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LichSuDatCho item = new LichSuDatCho
                        {
                            madatcho = Convert.ToInt32(reader["Madatcho"]),
                            thoigianbatdau = Convert.ToDateTime(reader["Thoigiandat"]),
                            mathietbi = Convert.ToInt32(reader["Mathietbi"]),
                            tenthietbi = reader["Tenthietbi"].ToString(),
                            giathue = Convert.ToInt32(reader["Giathue"]),
                            soluong = Convert.ToInt32(reader["soluong"]),
                            trangthai = reader["Trangthai"].ToString()
                        };
                        lsdc.Add(item);
                    }
                }

                conn.Close();
                ViewBag.lshd = lshd;
                ViewBag.Device = lsdc;
                return View();
            }
            else
            {
                //return RedirectToAction("Login", "Home");
                return View();
            }
            
        }

        public ActionResult CapNhatThongTin(ThanhVien model)
        {
            if (Session["Mathanhvien"] != null)
            {
                int mathanhvien = Convert.ToInt32(Session["Mathanhvien"]);
                MySqlConnection conn = db.GetConnection();
                conn.Open();
                string queryProfile = @"UPDATE thanhvien SET Hoten=@hoten, Email=@email, Sdt=@sdt, Diachi=@diachi WHERE Mathanhvien = @mathanhvien";
                MySqlCommand cmdProfile = new MySqlCommand(queryProfile, conn);
                cmdProfile.Parameters.AddWithValue("@hoten", model.HoTen);
                cmdProfile.Parameters.AddWithValue("@email", model.Email);
                cmdProfile.Parameters.AddWithValue("@sdt", model.Sdt);
                cmdProfile.Parameters.AddWithValue("@diachi", model.DiaChi);
                cmdProfile.Parameters.AddWithValue("@mathanhvien", mathanhvien);
                Boolean success = cmdProfile.ExecuteNonQuery() > 0;
                conn.Close();
                ViewBag.success = success;
                return RedirectToAction("UserProfile");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}