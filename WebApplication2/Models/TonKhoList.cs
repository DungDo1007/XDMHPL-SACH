using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class TonKhoList
    {
        public Nullable<int> fk_sachID { get; set; }
        public string SACH_NAME { get; set; }
        public Nullable<int> tonkho_soluong { get; set; }
    }
}