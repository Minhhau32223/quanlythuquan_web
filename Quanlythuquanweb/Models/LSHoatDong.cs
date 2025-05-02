using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlythuquanweb.Models
{
    public class LSHoatDong
    {
        public int mathanhvien {  get; set; }
        public int mahoatdong { get; set; }
        public DateTime thoigian { get; set; }
        public string loai { get; set; }
        public string chitiet { get; set; }
        public int is_delete { get; set; }
    }
}