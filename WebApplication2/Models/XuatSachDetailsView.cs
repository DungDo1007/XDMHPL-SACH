using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class XuatSachDetailsView
    {
        public int ctxuatsachID { get; set; }
        public Nullable<int> xuatsachID { get; set; }
        public Nullable<int> fk_sachID { get; set; }
        public Nullable<int> soluong { get; set; }
        public Nullable<int> soluongton { get; set; }
        public Nullable<double> SACH_GIA { get; set; }
        public string SACH_NAME { get; set; }
    }
}