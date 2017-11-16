using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class NXBDebtView
    {
        public int nxbdebtID { get; set; }
        public Nullable<int> fk_nxbID { get; set; }
        public Nullable<int> fk_sachID { get; set; }
        public Nullable<double> SACH_GIA { get; set; }
        public string SACH_NAME { get; set; }
        public Nullable<int> nxbdebt_soluong { get; set; }
        public Nullable<int> nxbdebt_banduoc { get; set; }
    }
}