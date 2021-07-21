using KurumsalMVC.Models.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalMVC.Controllers
{
    public class HizmetlerController : Controller
    {
        // GET: Hizmetler
        KurumsalMVCEntities db = new KurumsalMVCEntities();
        public ActionResult Index()
        {
            var Hizmetler = db.Hizmetler.Where(x => x.durum == 1).ToList();
            return View(Hizmetler);
        }

        public ActionResult HizmetDetay(int? id)
        {
            var detay = db.Hizmetler.Where(x => x.HizmetID == id && x.durum == 1).ToList();
            return View(detay);
        }

        public PartialViewResult KategoriHizmet()
        {
            var KategoriHizmet = db.Hizmetler.Where(x =>  x.durum == 1).ToList();
            return PartialView(KategoriHizmet);
        }

    }
}