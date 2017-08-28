namespace Crm_Project
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BytekCRMEntitiesMain : DbContext
    {
        public BytekCRMEntitiesMain()
            : base("name=BytekCRMEntitiesMain")
        {
        }

        public virtual DbSet<CariKartlar> CariKartlars { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Notlar> Notlars { get; set; }
        public virtual DbSet<Proje> Projes { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<Sipari> Siparis { get; set; }
        public virtual DbSet<StokKartlar> StokKartlars { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Teklif> Teklifs { get; set; }
        public virtual DbSet<TeklifStok> TeklifStoks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual DbSet<webpages_Roles> webpages_Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CariKartlar>()
                .HasMany(e => e.Siparis)
                .WithOptional(e => e.CariKartlar)
                .HasForeignKey(e => e.CariId);

            modelBuilder.Entity<CariKartlar>()
                .HasMany(e => e.Teklifs)
                .WithOptional(e => e.CariKartlar)
                .HasForeignKey(e => e.CariId);

            modelBuilder.Entity<City>()
                .HasMany(e => e.CariKartlars)
                .WithOptional(e => e.City)
                .HasForeignKey(e => e.Il);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.CariKartlars)
                .WithRequired(e => e.Group)
                .HasForeignKey(e => e.GrupKodu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sector>()
                .HasMany(e => e.CariKartlars)
                .WithRequired(e => e.Sector)
                .HasForeignKey(e => e.SektorKodu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StokKartlar>()
                .HasMany(e => e.Siparis)
                .WithOptional(e => e.StokKartlar)
                .HasForeignKey(e => e.StokId);

            modelBuilder.Entity<StokKartlar>()
                .HasMany(e => e.TeklifStoks)
                .WithOptional(e => e.StokKartlar)
                .HasForeignKey(e => e.StokId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CariKartlars)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UsersId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Notlars)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UseriId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.StokKartlars)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UsersId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Teklifs)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Teklifs1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.UpdateUserId);

            modelBuilder.Entity<webpages_Roles>()
                .HasMany(e => e.Users)
                .WithMany(e => e.webpages_Roles)
                .Map(m => m.ToTable("webpages_UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));
        }
    }
}
