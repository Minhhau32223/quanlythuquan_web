using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlythuquanweb.Models
{
    public class Thietbi
    { 
        public int Mathietbi { get; set; }
        public string Tenthietbi { get; set; }
        public string Madanhmuc { get; set; }
        public int Giathue { get; set; }
        public string Trangthai { get; set; }
        
        public Thietbi() { }
        public Thietbi(int mathietbi, string tenthietbi, string madanhmuc, int giathue, string trangthai)
        {
            Mathietbi = mathietbi;
            Tenthietbi = tenthietbi;
            Madanhmuc = madanhmuc;
            Giathue = giathue;
            Trangthai = trangthai;
        }
    }
}