using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crm_Project.Models
{
    public class StokTeklifListe
    {
        public List<StokKartlar> stok { get; set; }
        public Teklif teklifs { get; set; }
    }
}