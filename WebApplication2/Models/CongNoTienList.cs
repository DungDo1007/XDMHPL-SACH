using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CongNoTienList
    {
        public Nullable<int> fk_dailyID { get; set; }
        public string DAILY_NAME { get; set; }
        public Nullable<double> congnotien { get; set; }
        public Nullable<int> congnosach { get; set; }
    }
}