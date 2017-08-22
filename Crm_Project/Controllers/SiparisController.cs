using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            return View(db.Teklifs.Where(m=>m.Durum==true).ToList());
        }

        public ActionResult ViewDetail(int? Id)
        {
            var data = (from t in db.Teklifs
                        where t.Id == Id && t.Durum==true
                        select t).ToList();
            return View(data);
        }

        public ActionResult Delete(int Id)
        {
            var remove = db.Teklifs.Where(x => x.Id == Id).SingleOrDefault();
            db.Teklifs.Remove(remove);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExportExcel(int Id)
        {
            GridView gv = new GridView();
            gv.DataSource = (from t in db.Teklifs
                             where t.Id == Id 
                             select t).ToList();
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=Siparisler.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");

        }

        public ActionResult SiparisNot ( int Id)
        {
            Session["id"] = Id;

            var data = (from t in db.Teklifs
                        where t.Id == Id && t.UserId == UserId
                        select t).ToList();
            return View(data);
           
        }

        [HttpPost]
        public ActionResult NotEkle(string SiparisNotu)
        {

            int idd = Convert.ToInt32(Session["id"]);
            Notlar nt = new Notlar();

            var siparis = db.Teklifs.Where(m => m.Id == idd).SingleOrDefault();
            if (siparis != null)
            {
                try
                {
                 
                    nt.TeklifId = siparis.Id;
                    nt.UseriId = UserId;
                    nt.UserNot = SiparisNotu;
                    nt.Tarih = DateTime.Now;
                    db.Notlars.Add(nt);
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                    throw;
                }
            }

            // siparis.Notlar = SiparisNotu;

            return RedirectToAction("Index");
        }

        public ActionResult SiparisYorumlar()
        {
            int idd = Convert.ToInt32(Session["id"]);
            var notlar = db.Notlars.Where(m => m.TeklifId == idd).ToList();
            return View(notlar);
        }


    }
}