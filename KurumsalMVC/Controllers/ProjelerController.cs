using KurumsalMVC.Models;
using KurumsalMVC.Models.Entitiy;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalMVC.Controllers
{
    public class ProjelerController : Controller
    {
        // GET: Projeler
        KurumsalMVCEntities db = new KurumsalMVCEntities();

        HomePageViewModel by = new HomePageViewModel();
        public ActionResult Index()
        {
            by.kat = db.ProjeKategoriler.ToList();
            by.proje = db.Projeler.Where(x => x.Durum == 1).ToList();


            return View(by);
        }

        public ActionResult ProjeDetay(int? id)
        {
            var detay = db.Projeler.Where(x => x.ProjeID == id && x.Durum == 1).ToList();
            return View(detay);
        }

        public ActionResult Kategorisel(  int id,int sayfa = 1 )
        {
            //var Kategorisel = db.Projeler.Where(i => i.ProjeKategoriler.ProjeKatID==id && i.Durum == 1 && i.ProjeKategoriler.KategoriURL==katurl).OrderByDescending(i => i.ProjeID).ToList().ToPagedList(sayfa, 1);
            //return PartialView(Kategorisel);

            var Kategorisel = db.Projeler.Where(i => i.ProjeKategoriler.ProjeKatID==id && i.Durum == 1 ).ToList().ToPagedList(sayfa, 1);
            return PartialView(Kategorisel);
        }
    }
}