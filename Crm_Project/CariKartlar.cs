namespace Crm_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CariKartlar")]
    public partial class CariKartlar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CariKartlar()
        {
            Siparis = new HashSet<Sipari>();
            Teklifs = new HashSet<Teklif>();
        }

        public int Id { get; set; }

        [StringLength(200)]
        public string Kodu { get; set; }

        public string Unvan { get; set; }

        public string Unvan2 { get; set; }

        [Required]
        [StringLength(150)]
        public string TemsilciKodu { get; set; }

        public int GrupKodu { get; set; }

        public int SektorKodu { get; set; }

        [StringLength(200)]
        public string IligiliKisi { get; set; }

        [StringLength(50)]
        public string TelefonNo { get; set; }

        [StringLength(150)]
        public string Eposta { get; set; }

        public string Adres { get; set; }

        public int? Il { get; set; }

        [StringLength(100)]
        public string Ilce { get; set; }

        public int UsersId { get; set; }

        [StringLength(150)]
        public string VergiDairesi { get; set; }

        [StringLength(150)]
        public string VergiKodu { get; set; }

        public virtual City City { get; set; }

        public virtual Group Group { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sipari> Siparis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Teklif> Teklifs { get; set; }
    }
}
