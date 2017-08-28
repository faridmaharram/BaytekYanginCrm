using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Crm_Project.Controllers
{
    public class StokController : Controller
    {
        // GET: Stok
        private BytekCRMEntitiesMain db = new BytekCRMEntitiesMain();
        private int UserId = WebSecurity.CurrentUserId;


        [HttpPost]
        public JsonResult FillSearch(string urunName)
        {
            var data = from t in db.StokKartlars
                       where t.StokAdi.StartsWith(urunName) || t.StokGrup.StartsWith(urunName)
                       select new { t.StokAdi, t.StokGrup, t.StokKodu };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewDetail(int? Id)
        {
            var data = (from t in db.StokKartlars
                        where t.Id == Id && t.UsersId == UserId
                        select t).ToListAsync();
            return View(await data);
        }

        public async Task<ActionResult> Index()
        {
            
            
            
            return View(await db.StokKartlars.ToListAsync());
        }




        public async Task<ActionResult> Create()
        {
            ViewData["Cities"] = new SelectList(await db.Cities.OrderBy(p => p.Name).ToArrayAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(StokKartlar Modell, HttpPostedFileBase Foto1)

        {
            if (Foto1 != null)
            {
                Modell.Resim = new byte[Foto1.ContentLength];
                Foto1.InputStream.Read(Modell.Resim, 0, Foto1.ContentLength);

                // nerde

            }

            Modell.UsersId = UserId;
            db.StokKartlars.Add(Modell);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Stok");
        }


        public async Task<ActionResult> Delete(int? Id)
        {
            var remove = await db.StokKartlars.Where(x => x.Id == Id).SingleOrDefaultAsync();
          

            var teklifstok = await db.TeklifStoks.Where(m => m.StokId == Id).SingleOrDefaultAsync();

            var teklifstoksay = db.TeklifStoks.Where(m => m.StokId == Id).Count();
            if (teklifstoksay != 0)
            {
                db.TeklifStoks.Remove(teklifstok);
              await  db.SaveChangesAsync();
            }
          
            
           

            db.StokKartlars.Remove(remove);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int? Id)
        {
            return View(await db.StokKartlars.Where(q => q.UsersId == UserId).SingleOrDefaultAsync(a => a.Id == Id));
        }

        [HttpPost]
        public ActionResult Edit(StokKartlar Modell, HttpPostedFileBase Foto1)
        {
            
         //   StokKartlar stok = new StokKartlar();
            if (Foto1 != null)
            {
                Modell.Resim = new byte[Foto1.ContentLength];
                Foto1.InputStream.Read(Modell.Resim, 0, Foto1.ContentLength);
            }
            Modell.UsersId = UserId;

            //  stok = Model;
            db.Entry(Modell).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> SearchResult(string urunName)
        {
            var data = from d in db.StokKartlars
                       where d.StokAdi.StartsWith(urunName)
                       select d;
            return View(await data.ToListAsync());
        }

        public async Task<ActionResult> AllStok()
        {
            return View(await db.StokKartlars.ToListAsync());
        }
    }
}