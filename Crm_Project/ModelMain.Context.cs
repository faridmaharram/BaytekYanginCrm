﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crm_Project
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BytekCRMEntitiesMain : DbContext
    {
        public BytekCRMEntitiesMain()
            : base("name=BytekCRMEntitiesMain")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<CariKartlar> CariKartlars { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<StokKartlar> StokKartlars { get; set; }
        public virtual DbSet<Proje> Projes { get; set; }
        public virtual DbSet<Teklif> Teklifs { get; set; }
    }
}
