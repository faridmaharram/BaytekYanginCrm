namespace Crm_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sipari
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Firma { get; set; }

        public int? TeklifNo { get; set; }

        public int? StokId { get; set; }

        public int? Mikyar { get; set; }

        public int? Birim { get; set; }

        public int? Tutar { get; set; }

        public int? CariId { get; set; }

        public int? ProjeId { get; set; }

        public int? KarOrani { get; set; }

        public int? Kar { get; set; }

        public int? UserId { get; set; }

        public DateTime? Tarih { get; set; }

        public string Notlar { get; set; }

        public virtual CariKartlar CariKartlar { get; set; }

        public virtual Proje Proje { get; set; }

        public virtual StokKartlar StokKartlar { get; set; }

        public virtual User User { get; set; }
    }
}
