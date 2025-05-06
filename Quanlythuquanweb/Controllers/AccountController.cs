using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using Doanc_sharp.src.Helpers;
using MySql.Data.MySqlClient;
using Quanlythuquanweb.Models;
namespace Quanlythuquanweb.Controllers
{
    public class AccountController : Controller
    {
        private DbConnection db = new DbConnection();
        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                string taikhoan = form["Taikhoan"];
                string matkhau = form["Matkhau"];

                MySqlConnection conn = db.GetConnection();
                conn.Open();
                //string queryTV = "SELECT COUNT(*) FROM thanhvien WHERE Taikhoan=@taikhoan AND Matkhau=@Matkhau";
                string queryTV = "SELECT Mathanhvien FROM thanhvien WHERE Taikhoan=@taikhoan AND Matkhau=@Matkhau";
                string queryNV = "SELECT COUNT(*) FROM nhanvien WHERE Taikhoan=@taikhoan AND Matkhau=@Matkhau";

                MySqlCommand cmdTV = new MySqlCommand(queryTV, conn);
                cmdTV.Parameters.AddWithValue("@Taikhoan", taikhoan);
                cmdTV.Parameters.AddWithValue("@matkhau", matkhau);
                int countTV = Convert.ToInt32(cmdTV.ExecuteScalar());
                object resultTV = cmdTV.ExecuteScalar();


                if (resultTV != null)
                {
                    int mathanhvien = countTV;
                    Session["Mathanhvien"] = mathanhvien;
                    ViewBag.Message = "Đăng nhập thành công!";
                    return RedirectToAction("Danhsach", "Thietbi");
                }
                else
                {
                    MySqlCommand cmdNV = new MySqlCommand(queryNV, conn);
                    cmdNV.Parameters.AddWithValue("@Taikhoan", taikhoan);
                    cmdNV.Parameters.AddWithValue("@matkhau", matkhau);

                    int countNV = Convert.ToInt32(cmdNV.ExecuteScalar());

                    if (countNV > 0)
                    {
                        ViewBag.Message = "Đăng nhập thành công!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Lỗi: " + ex.Message;
            }
            return View();
        }
        //public ActionResult Login(FormCollection form)
        //{
        //    MySqlConnection conn = null;

        //    try
        //    {
        //        string taikhoan = form["Taikhoan"];
        //        string matkhau = form["Matkhau"];

        //        conn = db.GetConnection();
        //        conn.Open();

        //        // Kiểm tra đăng nhập thành viên
        //        string queryTV = "SELECT Mathanhvien FROM thanhvien WHERE Taikhoan=@taikhoan AND Matkhau=@matkhau";
        //        MySqlCommand cmdTV = new MySqlCommand(queryTV, conn);
        //        cmdTV.Parameters.AddWithValue("@taikhoan", taikhoan);
        //        cmdTV.Parameters.AddWithValue("@matkhau", matkhau);

        //        object resultTV = cmdTV.ExecuteScalar();

        //        if (resultTV != null)
        //        {
        //            int mathanhvien = Convert.ToInt32(resultTV);

        //            // Truy vấn lịch sử hoạt động
        //            string queryLSHD = @"SELECT Mathanhvien, MaHoatdong, Ngaydangky, Loai, Chitiet, is_delete
        //                         FROM lshoatdong
        //                         WHERE Mathanhvien = @mathanhvien AND is_delete = 0
        //                         ORDER BY Ngaydangky DESC";
        //            MySqlCommand cmdLSHD = new MySqlCommand(queryLSHD, conn);
        //            cmdLSHD.Parameters.AddWithValue("@mathanhvien", mathanhvien);

        //            List<LSHoatDong> lsHoatDongs = new List<LSHoatDong>();
        //            using (MySqlDataReader reader = cmdLSHD.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    LSHoatDong log = new LSHoatDong
        //                    {
        //                        mathanhvien = Convert.ToInt32(reader["Mathanhvien"]),
        //                        mahoatdong = Convert.ToInt32(reader["MaHoatdong"]),
        //                        ngayDangky = Convert.ToDateTime(reader["Ngaydangky"]),
        //                        loai = reader["Loai"].ToString(),
        //                        chitiet = reader["Chitiet"].ToString(),
        //                        is_delete = Convert.ToInt32(reader["is_delete"])
        //                    };
        //                    lsHoatDongs.Add(log);
        //                }
        //            }

        //            TempData["LichSuHoatDong"] = lsHoatDongs; // Dùng TempData để truyền qua Redirect
        //            TempData["Message"] = "Đăng nhập thành công!";
        //            return RedirectToAction("UserProfile", "Home");
        //        }
        //        else
        //        {
        //            // Kiểm tra đăng nhập nhân viên
        //            string queryNV = "SELECT COUNT(*) FROM nhanvien WHERE Taikhoan=@taikhoan AND Matkhau=@matkhau";
        //            MySqlCommand cmdNV = new MySqlCommand(queryNV, conn);
        //            cmdNV.Parameters.AddWithValue("@taikhoan", taikhoan);
        //            cmdNV.Parameters.AddWithValue("@matkhau", matkhau);
        //            int countNV = Convert.ToInt32(cmdNV.ExecuteScalar());

        //            if (countNV > 0)
        //            {
        //                TempData["Message"] = "Đăng nhập thành công!";
        //                return RedirectToAction("UserProfile", "Home");
        //            }
        //            else
        //            {
        //                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Lỗi: " + ex.Message;
        //    }
        //    finally
        //    {
        //        if (conn != null && conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }
        //    return View(); // Trả về lại trang Login nếu có lỗi
        //}


        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection form)
        {
            try
            {

                string taikhoan = form["Taikhoan"];
                string matkhau = form["Matkhau"];
                string hoten = form["Hoten"];
                string sdt = form["Sdt"];
                string email = form["Email"];
                string diachi = form["Diachi"];
                string trangthai = "Bình thường";
                string ngaydangky = DateTime.Now.ToString("yyyy-MM-dd");

                MySqlConnection conn = db.GetConnection();
                conn.Open();

                string query = $@"INSERT INTO thanhvien(Mathanhvien, Taikhoan, Matkhau, Hoten, Sdt, Email, Diachi, Trangthai, Ngaydangky) VALUES
                (1, @taikhoan, @matkhau, @hoten, @sdt, @email, @diachi, @trangthai, @ngaydangky)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                cmd.Parameters.AddWithValue("@matkhau", matkhau);
                cmd.Parameters.AddWithValue("@hoten", hoten);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@diachi", diachi);
                cmd.Parameters.AddWithValue("@trangthai", trangthai);
                cmd.Parameters.AddWithValue("@ngaydangky", ngaydangky);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    ViewBag.Message = "Đăng ký thành công!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Đăng ký thất bại!";
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Lỗi: "+ ex.Message;
            }
            return View();
        }

    }
}