using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Crm_Project.Controllers
{
    [Authorize]
    public class SectorController : Controller
    {
        BytekCRMEntitiesMain db = new BytekCRMEntitiesMain();
        // GET: Sector
        public async Task<ActionResult> Index()
        {
            return View(await db.Sectors.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Sector Model)
        {
            if (ModelState.IsValid)
            {
                db.Sectors.Add(Model);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? Id)
        {
            db.Sectors.Remove(await db.Sectors.FindAsync(Id));
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}