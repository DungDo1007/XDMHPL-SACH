//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TonKho
    {
        public int tonkhoID { get; set; }
        public Nullable<int> fk_sachID { get; set; }
        public Nullable<int> tonkho_soluong { get; set; }
        public Nullable<System.DateTime> tonkho_ngaycapnhat { get; set; }
    
        public virtual Sach Sach { get; set; }
    }
}