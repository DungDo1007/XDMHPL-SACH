﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class THUEntities : DbContext
    {
        public THUEntities()
            : base("name=THUEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DaiLy> DaiLies { get; set; }
        public virtual DbSet<DaiLyDebt> DaiLyDebts { get; set; }
        public virtual DbSet<DaiLyDebtTien> DaiLyDebtTiens { get; set; }
        public virtual DbSet<DoanhThu> DoanhThus { get; set; }
        public virtual DbSet<NhapSachDetail> NhapSachDetails { get; set; }
        public virtual DbSet<NhapSachMaster> NhapSachMasters { get; set; }
        public virtual DbSet<NXB> NXBs { get; set; }
        public virtual DbSet<NXBDebt> NXBDebts { get; set; }
        public virtual DbSet<NXBDebtTien> NXBDebtTiens { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TonKho> TonKhoes { get; set; }
        public virtual DbSet<XuatSachDetail> XuatSachDetails { get; set; }
        public virtual DbSet<XuatSachMaster> XuatSachMasters { get; set; }
    }
}
