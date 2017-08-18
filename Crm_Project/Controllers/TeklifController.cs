using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using iTextSharp.text.html.simpleparser;
using System.Data.Entity;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;
using System.Web.Helpers;
using ExporterObjects;

namespace Crm_Project.Controllers
{
    public class TeklifController : Controller
    {
        private BytekCRMEntitiesMain db = new BytekCRMEntitiesMain();
        private int UserId = WebSecurity.CurrentUserId;
        // GET: Teklif
        public ActionResult Index()
        {
            return View(db.Teklifs.ToList());
        }

        public ActionResult Create()
        {

            ViewData["Ilgili"] = new SelectList(db.CariKartlars.OrderBy(p => p.IligiliKisi).ToArray(), "Id", "IligiliKisi");
            ViewData["StokAdi"] = new SelectList(db.StokKartlars.OrderBy(p => p.StokAdi).ToArray(), "Id", "StokAdi");
            ViewData["ProjeAdi"] = new SelectList(db.Projes.OrderBy(p => p.ProjeAdi).ToArray(), "Id", "ProjeAdi");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Teklif Modell)

        {

            Modell.UserId = UserId;
            Modell.Tarih = DateTime.Now;
            Modell.UpdateUserId = UserId;
            int tutar = (int)Modell.Miktar * (int)Modell.Birim;
            double kar = (tutar * (int)Modell.KarOrani) / 100.0;



            Modell.Tutar = tutar;
            Modell.Kar = (int)kar;

            db.Teklifs.Add(Modell);
            db.SaveChanges();
            return RedirectToAction("Index", "Teklif");
        }


        public ActionResult Delete(int Id)
        {
            var remove = db.Teklifs.Where(x => x.Id == Id).SingleOrDefault();
            db.Teklifs.Remove(remove);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            ViewData["Ilgili"] = new SelectList(db.CariKartlars.OrderBy(p => p.IligiliKisi).ToArray(), "Id", "IligiliKisi");
            ViewData["StokAdi"] = new SelectList(db.StokKartlars.OrderBy(p => p.StokAdi).ToArray(), "Id", "StokAdi");
            ViewData["ProjeAdi"] = new SelectList(db.Projes.OrderBy(p => p.ProjeAdi).ToArray(), "Id", "ProjeAdi");
            return View(db.Teklifs.Where(q => q.UserId == UserId).SingleOrDefault(a => a.Id == Id));
        }

        [HttpPost]
        public ActionResult Edit(Teklif Modell)
        {


            Modell.UpdateUserId = UserId;
            int tutar = (int)Modell.Miktar * (int)Modell.Birim;
            double kar = (tutar * (int)Modell.KarOrani) / 100.0;



            Modell.Tutar = tutar;
            Modell.Kar = (int)kar;


            //  stok = Model;
            db.Entry(Modell).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewDetail(int? Id)
        {
            var data = (from t in db.Teklifs
                        where t.Id == Id && t.UserId == UserId
                        select t).ToList();
            return View(data);
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
            Response.AddHeader("content-disposition", "attachment;filename=Stations.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");

        }
        public ActionResult ExportPDF(int Id)
        {
            return View();
        }

    }
}