using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Crm_Project.Controllers
{
    [Authorize]
    public class CariController : Controller
    {

        private BytekCRMEntitiesMain db = new BytekCRMEntitiesMain();
        private int UserId = WebSecurity.CurrentUserId;
        // GET: Cari

        [HttpPost]
        public JsonResult FillSearch(string urunName)
        {
            var data = from t in db.CariKartlars
                where t.Unvan.StartsWith(urunName) || t.Unvan2.StartsWith(urunName)
                select new { t.Unvan, t.Unvan2 , t.Kodu };
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewDetail(int? Id)
        {
            var data = (from t in db.CariKartlars
                where t.Id == Id && t.UsersId == UserId
                select t).ToListAsync();
            return View(await data);
        }


        public async Task<ActionResult> Index()
        {
           return View(await db.CariKartlars.Where(q => q.UsersId ==UserId).ToListAsync());
        }

        [HttpPost]
        public JsonResult CreateCard(string IligiliKisi)
        {
            var data = (from t in db.Users
                       where t.Name.StartsWith(IligiliKisi) 
                       select new { t.Name});
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Create()
        {
            ViewData["Cities"] = new SelectList(await db.Cities.OrderBy(p => p.Name).ToArrayAsync(), "Id", "Name");
            ViewData["Groups"] = new SelectList(await db.Groups.OrderBy(p => p.GroupKodu).ToArrayAsync(), "Id", "GroupKodu");
            ViewData["Sectors"] = new SelectList(await db.Sectors.OrderBy(p => p.Sektor).ToArrayAsync(), "Id", "Sektor");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CariKartlar Model)
        {
            db.CariKartlars.Add(Model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Cari");
        }

        [Authorize(Roles ="Adminstrator")] 
        public async Task<ActionResult> Delete(int? Id)
        {
            var remove = await db.CariKartlars.Where(x => x.Id == Id).SingleOrDefaultAsync();
            db.CariKartlars.Remove(remove);
            //burda idsi girilen userin idsine esitdirse silinebiler dedik
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Adminstrator")] // deneme 1 2
        public async Task<ActionResult> Edit(int? Id)
        {
            return View(await db.CariKartlars.Where(q => q.UsersId == UserId).SingleOrDefaultAsync(a => a.Id == Id));
        }

        [HttpPost]
        public ActionResult Edit(CariKartlar Model)
        {
            if (ModelState.IsValid)
            {
                return View(Model);
            }
            db.Entry(Model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> SearchResult(string urunName)
        {
            var data = from d in db.CariKartlars
                       where d.Unvan.StartsWith(urunName)
                       select d;
            return View(await data.ToListAsync());
        }

        public async Task<ActionResult> AllCari()
        {
            return View(await db.CariKartlars.ToListAsync());
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //        db.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}