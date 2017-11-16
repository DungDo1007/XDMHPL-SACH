using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class BookViewModel
    {
        public int sachID { get; set; }
        public string sach_ten { get; set; }
        public string sach_tacgia { get; set; }
        public Nullable<double> sach_gianhap { get; set; }
        public Nullable<double> sach_giaxuat { get; set; }
        public Nullable<int> fk_nxbID { get; set; }
        public Nullable<int> sach_soluong { get; set; }
        public string NXB_NAME { get; set; }
    }
}