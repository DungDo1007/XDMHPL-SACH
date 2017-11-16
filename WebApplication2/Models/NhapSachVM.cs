using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class NhapSachVM
    {
        public int fk_nxbID { get; set; }
        public DateTime nhapsach_ngaygiao { get; set; }
        public string nhapsach_nguoigiao { get; set; }
        public List<NhapSachDetail> NhapSachDetails { get; set; }
    }
}