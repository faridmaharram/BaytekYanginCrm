namespace Crm_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teklif")]
    public partial class Teklif
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teklif()
        {
            Notlars = new HashSet<Notlar>();
            TeklifStoks = new HashSet<TeklifStok>();
        }

        public int Id { get; set; }

        [StringLength(150)]
        public string Firma { get; set; }

        [StringLength(150)]
        public string TeklifNo { get; set; }

        public int? BirimFiyat { get; set; }

        public int? Tutar { get; set; }

        [StringLength(150)]
        public string IlgiliKisi { get; set; }

        [StringLength(150)]
        public string Musteri { get; set; }

        [StringLength(150)]
        public string Eposta { get; set; }

        [StringLength(50)]
        public string TelefonNo { get; set; }

        public int? CariId { get; set; }

        public int? ProjeId { get; set; }

        public int? KarOrani { get; set; }

        public int? Kar { get; set; }

        public int? UserId { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? Tarih { get; set; }

        public bool? Durum { get; set; }

        public virtual CariKartlar CariKartlar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notlar> Notlars { get; set; }

        public virtual Proje Proje { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeklifStok> TeklifStoks { get; set; }
    }
}
