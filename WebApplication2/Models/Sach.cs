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
    
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            this.DaiLyDebts = new HashSet<DaiLyDebt>();
            this.NhapSachDetails = new HashSet<NhapSachDetail>();
            this.NXBDebts = new HashSet<NXBDebt>();
            this.TonKhoes = new HashSet<TonKho>();
            this.XuatSachDetails = new HashSet<XuatSachDetail>();
        }
    
        public int sachID { get; set; }
        public string sach_ten { get; set; }
        public string sach_tacgia { get; set; }
        public Nullable<int> fk_nxbID { get; set; }
        public Nullable<double> sach_gianhap { get; set; }
        public Nullable<double> sach_giaxuat { get; set; }
        public Nullable<int> sach_soluong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DaiLyDebt> DaiLyDebts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhapSachDetail> NhapSachDetails { get; set; }
        public virtual NXB NXB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NXBDebt> NXBDebts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TonKho> TonKhoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XuatSachDetail> XuatSachDetails { get; set; }
    }
}
