using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class XuatSachMasterView
    {
        public int xuatsachID { get; set; }
        public Nullable<int> fk_dailyID { get; set; }
        public Nullable<System.DateTime> xuatsach_ngayorder { get; set; }
        public Nullable<System.DateTime> xuatsach_ngayupdate { get; set; }
        public string xuatsach_nguoinhan { get; set; }
        public string xuatsach_trangthai { get; set; }
        public Nullable<double> xuatsach_tongtien { get; set; }
        public string DAILY_NAME { get; set; }
    }
}