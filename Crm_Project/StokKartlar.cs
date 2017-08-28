namespace Crm_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StokKartlar")]
    public partial class StokKartlar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StokKartlar()
        {
            Siparis = new HashSet<Sipari>();
            TeklifStoks = new HashSet<TeklifStok>();
        }

        public int Id { get; set; }

        [StringLength(150)]
        public string StokAdi { get; set; }

        [StringLength(150)]
        public string StokKodu { get; set; }

        public int? Birim { get; set; }

        public int? AlisFiyat { get; set; }

        public int? SatisFiyat { get; set; }

        public int? KDV { get; set; }

        [StringLength(200)]
        public string Marka { get; set; }

        [StringLength(200)]
        public string Model { get; set; }

        [Required]
        [StringLength(100)]
        public string StokGrupParent { get; set; }

        [Required]
        [StringLength(100)]
        public string StokGrup { get; set; }

        public byte[] Resim { get; set; }

        public int UsersId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sipari> Siparis { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeklifStok> TeklifStoks { get; set; }
    }
}
