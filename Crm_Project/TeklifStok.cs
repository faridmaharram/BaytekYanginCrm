namespace Crm_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TeklifStok")]
    public partial class TeklifStok
    {
        public int Id { get; set; }

        public int? TeklifId { get; set; }

        public int? StokId { get; set; }

        public virtual StokKartlar StokKartlar { get; set; }

        public virtual Teklif Teklif { get; set; }
    }
}
