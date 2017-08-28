using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crm_Project.ViewModels.TeklifViewModels
{
    public class CreateTeklifViewModel
    {
        public List<SelectListItem> Ilgili { get; set; }
        public List<SelectListItem> StokAdi { get; set; }
        public List<SelectListItem> ProjeAdi { get; set; }
        public List<SelectListItem> Firma { get; set; }
        public List<SelectListItem> Musteri { get; set; }
        public string MusteriId { get; set; }
        public decimal BirimFiyat { get; set; }
        public string CariId { get; set; }
        public string ProjeId { get; set; }
        public string KarOrani { get; set; }
        public List<StokKartlar> StokKartlars { get; set; }
        public List<string> Ids { get; set; }  
        // Check box kimi ne poturmek isteyirsen Id? he
    }
}