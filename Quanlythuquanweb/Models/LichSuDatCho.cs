using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlythuquanweb.Models
{
    public class LichSuDatCho
    {
        public int madatcho { get; set; }
        public int mathanhvien { get; set; }
        public DateTime thoigianbatdau { get; set; }
        public DateTime thoigianketthuc { get; set; }
        public int mathietbi { get; set; }
        public string tenthietbi { get; set; }
        public int giathue { get; set; }
        public int soluong { get; set; }
        public string trangthai { get; set; }
        public LichSuDatCho() { }
        public LichSuDatCho(int madatcho, int mathanhvien, DateTime thoigianbatdau, DateTime thoigianketthuc, string trangthai, int mathietbi, string tenthietbi, int giathue, int soluong)
        {
            this.madatcho = madatcho;
            this.mathanhvien = mathanhvien;
            this.thoigianbatdau = thoigianbatdau;
            this.thoigianketthuc = thoigianketthuc;
            this.trangthai = trangthai;
            this.mathietbi = mathietbi;
            this.tenthietbi = tenthietbi;
            this.giathue = giathue;
            this.soluong = soluong;
        }
    }
}