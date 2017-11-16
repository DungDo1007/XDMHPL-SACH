using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class DaiLyDebtView
    {
        public int dailydebtID { get; set; }
        public Nullable<int> fk_dailyID { get; set; }
        public Nullable<int> fk_sachID { get; set; }
        public Nullable<double> SACH_GIA { get; set; }
        public string SACH_NAME { get; set; }
        public Nullable<int> dailydebt_soluong { get; set; }
    }
}