namespace Crm_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notlar")]
    public partial class Notlar
    {
        public int Id { get; set; }

        public int? UseriId { get; set; }

        public int? TeklifId { get; set; }

        public string UserNot { get; set; }

        public DateTime? Tarih { get; set; }

        public virtual Teklif Teklif { get; set; }

        public virtual User User { get; set; }
    }
}
