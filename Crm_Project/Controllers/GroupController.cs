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
    public class GroupController : Controller
    {
        BytekCRMEntitiesMain db = new BytekCRMEntitiesMain();
        // GET: Group
        public async Task<ActionResult> Index()
        {
            using (db)
            {
                return View(await db.Groups.ToListAsync());
            }

        }
        public async Task<ActionResult> Create(string GroupKodu)
        {
            using (db)
            {
                if (ModelState.IsValid)
                {
                    Group Model = new Group();
                    Model.GroupKodu = GroupKodu;
                    db.Groups.Add(Model);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index", "Group");
            }
        }

        public async Task<ActionResult> Delete(int? Id)
        {
            using (db)
            {
                db.Entry(await db.Groups.FindAsync(Id)).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Group");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

    }
}