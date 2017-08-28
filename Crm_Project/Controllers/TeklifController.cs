using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Data.Entity;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;
using System.Web.Helpers;

using System.Data.Entity.Migrations;
using Crm_Project.Models;
using System.Text;
using Crm_Project.ViewModels.TeklifViewModels;

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
        //Tamam boyle oldugunda ,. checkboxlardan birinin adini post ediyor devam ede bilirsiniz hocam

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
           
            var customers = (from customer in db.CariKartlars
                             where customer.Unvan.StartsWith(prefix)
                             select new
                             {
                                 label = customer.Unvan,
                                 val = customer.Id
                             }).ToList();
            Session["Firma"] = prefix;
            return Json(customers);
        }
        public ActionResult Create()
        {
            var model = new CreateTeklifViewModel();

            ViewData["Ilgili"] = new SelectList( db.CariKartlars.OrderBy(p => p.IligiliKisi).ToArray(), "Id", "IligiliKisi");
            ViewData["StokAdi"] = new SelectList( db.StokKartlars.OrderBy(p => p.StokAdi).ToArray(), "Id", "StokAdi");
            ViewData["ProjeAdi"] = new SelectList( db.Projes.OrderBy(p => p.ProjeAdi).ToArray(), "Id", "ProjeAdi");
            ViewData["Firma"] = new SelectList( db.CariKartlars.OrderBy(p => p.Unvan).ToArray(), "Id", "Unvan");
            ViewData["Musteri"] = new SelectList( db.CariKartlars.OrderBy(p => p.Unvan2).ToArray(), "Id", "Unvan2");
            
            var ProductList = db.StokKartlars.ToList();
            ViewBag.ProductList = ProductList;
          
            return View();
        }

       
        [HttpPost]
        public ActionResult Create(Teklif Modell, int[] CheckTeklif,string CustomerName)
        {


            try
            {
                Modell.UserId = UserId;
                Modell.Tarih = DateTime.Now;
                Modell.UpdateUserId = UserId;
                Modell.Durum = false;
                Modell.Firma = CustomerName;
                Modell.TeklifNo = DateTime.Now.Year + "-" + UserId + DateTime.Now;
                db.Teklifs.Add(Modell);
                db.SaveChanges();
                
                var Teklifler = db.Teklifs.Where(m => m.TeklifNo == Modell.TeklifNo).SingleOrDefault();
                TeklifStok ts = new TeklifStok();
                int StokSayi = db.StokKartlars.Count();
                for (int i = 0; i < StokSayi; i++)
                {
                    if (CheckTeklif[i] != 0)
                    {
                        int idds = CheckTeklif[i];
                      var stok =  db.StokKartlars.Where(m => m.Id == idds).SingleOrDefault();
                        Modell.Kar = (stok.SatisFiyat - stok.AlisFiyat) * stok.Birim * Modell.BirimFiyat;
                        ts.TeklifId = Teklifler.Id;
                        ts.StokId = CheckTeklif[i];
                        db.TeklifStoks.Add(ts);
                        db.SaveChanges();
                    }
                    Teklifler.Kar = Modell.Kar;
                    db.Teklifs.AddOrUpdate(Teklifler);
                    db.SaveChanges();


                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Teklif");
                
            }

            

            return RedirectToAction("Index", "Teklif");
        }


        public ActionResult Delete(int Id)
        {
            var remove = db.Teklifs.Where(x => x.Id == Id).SingleOrDefault();
            var teklifstok = db.TeklifStoks.Where(m => m.TeklifId == Id).FirstOrDefault();
            
            if(remove.UserId == UserId)
            {
                var teklifstoksay = db.TeklifStoks.Where(m => m.TeklifId == Id).Count();
                if (teklifstoksay != 0)
                {
                    db.TeklifStoks.Remove(teklifstok);
                    db.SaveChanges();
                }

                var teklifnotlar = db.Notlars.Where(m => m.TeklifId == Id).FirstOrDefault();
                var teklifnotlarsay = db.Notlars.Where(m => m.TeklifId == Id).Count();
                if (teklifnotlarsay != 0)
                {
                    db.Notlars.Remove(teklifnotlar);
                    db.SaveChanges();
                }
                db.Teklifs.Remove(remove);
                db.SaveChanges();
            }
            else
            {
                ViewData["hata"] = "Size ait olmayan teklifleri silemezsiniz !!!";
            }
         
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var remove = db.Teklifs.Where(x => x.Id == Id).SingleOrDefault();


            if (remove.UserId == UserId)
            {

                ViewData["Ilgili"] = new SelectList(db.CariKartlars.OrderBy(p => p.IligiliKisi).ToArray(), "Id", "IligiliKisi");
                ViewData["StokAdi"] = new SelectList(db.StokKartlars.OrderBy(p => p.StokAdi).ToArray(), "Id", "StokAdi");
                ViewData["ProjeAdi"] = new SelectList(db.Projes.OrderBy(p => p.ProjeAdi).ToArray(), "Id", "ProjeAdi");
                ViewData["Musteri"] = new SelectList(db.CariKartlars.OrderBy(p => p.Unvan2).ToArray(), "Id", "Unvan2");

                TeklifStok tk = new TeklifStok();
                var dey = db.TeklifStoks.Where(m => m.TeklifId == Id).FirstOrDefault();
                var ProductList = db.StokKartlars.Where(m=>m.Id==dey.StokId).ToList();
                ViewBag.ProductList = ProductList;

                return View(db.Teklifs.Where(q => q.UserId == UserId).SingleOrDefault(a => a.Id == Id));
            }

            else return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Edit(Teklif Modell, int[] CheckTeklif, string CustomerName)
        {


            try
            {
                Modell.UserId = UserId;
                Modell.Tarih = DateTime.Now;
                Modell.UpdateUserId = UserId;
                Modell.Durum = false;
                Modell.Firma = CustomerName;
                Modell.TeklifNo = DateTime.Now.Year + "-" + UserId + DateTime.Now;
                db.Teklifs.AddOrUpdate(Modell);
                db.SaveChanges();

                var Teklifler = db.Teklifs.Where(m => m.TeklifNo == Modell.TeklifNo).SingleOrDefault();
                TeklifStok ts = new TeklifStok();
                int StokSayi = db.StokKartlars.Count();
                for (int i = 0; i < StokSayi; i++)
                {
                    if (CheckTeklif[i] != 0)
                    {
                        int idds = CheckTeklif[i];
                        var stok = db.StokKartlars.Where(m => m.Id == idds).SingleOrDefault();
                        Modell.Kar = (stok.SatisFiyat - stok.AlisFiyat) * stok.Birim * Modell.BirimFiyat;
                        ts.TeklifId = Teklifler.Id;
                        ts.StokId = CheckTeklif[i];
                        db.TeklifStoks.Add(ts);
                        db.SaveChanges();
                    }
                    Teklifler.Kar = Modell.Kar;
                    db.Teklifs.AddOrUpdate(Teklifler);
                    db.SaveChanges();


                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Teklif");

            }

            return RedirectToAction("Index");
        }

        public ActionResult ViewDetail(int? Id)
        {
            var data = (from t in db.Teklifs
                        where t.Id == Id
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
            Response.AddHeader("content-disposition", "attachment;filename=Teklifler.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");

        }
        public ActionResult ExportExcelAll()
        {
            GridView gv = new GridView();
            gv.DataSource = (from t in db.Teklifs
                             where t.Durum == false
                             select t).ToList();
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=Teklifler.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");

        }
        public ActionResult ExportPDF(int id)
        {

            return new Rotativa.ActionAsPdf("ViewDetail", new { id });
        }




        public ActionResult SiparisNot(int Id)
        {
            Session["id"] = Id;

            var data = (from t in db.Teklifs
                        where t.Id == Id && t.UserId == UserId
                        select t).ToList();
            return View(data);

        }
        [HttpPost]
        public ActionResult SipariseCevir(string SiparisNotu)
        {

            int idd = Convert.ToInt32(Session["id"]);
            Notlar nt = new Notlar();

            var siparis = db.Teklifs.Where(m => m.Id == idd).SingleOrDefault();
            if (siparis != null)
            {
                try
                {
                    siparis.Durum = true;
                    //          db.Teklifs.AddOrUpdate(siparis);
                    db.SaveChanges();
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

        public ActionResult TeklifYorumlar()
        {
            int idd = Convert.ToInt32(Session["id"]);
            var notlar = db.Notlars.Where(m => m.TeklifId == idd).ToList();
            return View(notlar);
        }





    }
}