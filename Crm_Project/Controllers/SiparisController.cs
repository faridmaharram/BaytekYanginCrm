using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Crm_Project.Controllers
{
    public class SiparisController : Controller
    {
        private BytekCRMEntitiesMain db = new BytekCRMEntitiesMain();
        private int UserId = WebSecurity.CurrentUserId;
        // GET: TSiparis
        public ActionResult Index()
        {
            return View(db.Teklifs.ToList());
        }

    }
}