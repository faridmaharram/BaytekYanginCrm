//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CariKartlar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CariKartlar()
        {
            this.Teklifs = new HashSet<Teklif>();
        }
    
        public int Id { get; set; }
        public string Kodu { get; set; }
        public string Unvan { get; set; }
        public string Unvan2 { get; set; }
        public string TemsilciKodu { get; set; }
        public int GrupKodu { get; set; }
        public int SektorKodu { get; set; }
        public string IligiliKisi { get; set; }
        public string TelefonNo { get; set; }
        public string Eposta { get; set; }
        public string Adres { get; set; }
        public Nullable<int> Il { get; set; }
        public string Ilce { get; set; }
        public int UsersId { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiKodu { get; set; }
    
        public virtual City City { get; set; }
        public virtual Group Group { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Teklif> Teklifs { get; set; }
    }
}