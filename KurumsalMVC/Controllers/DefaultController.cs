using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalMVC.Models;
using KurumsalMVC.Models.Entitiy;
namespace KurumsalMVC.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        KurumsalMVCEntities db = new KurumsalMVCEntities();
        public ActionResult Index()
        {
            var slider = db.Slider.Where(i => i.Durum == 1).OrderByDescending(i => i.SliderID).ToList();
            return View(slider);
        }

        public PartialViewResult SliderALt()
        {
            var SliderALt = db.Bilgi.ToList();
            return PartialView(SliderALt);
        }

        public PartialViewResult AnasayfaHakkimizda()
        {
            var AnasayfaHakkimizda = db.Hakkimizda.ToList();
            return PartialView(AnasayfaHakkimizda);
        }

        public PartialViewResult AnasayfaHizmet()
        {
            var AnasayfaHizmet = db.Hizmetler.Where(i => i.durum == 1).OrderByDescending(i => i.HizmetID).Take(3).ToList();
            return PartialView(AnasayfaHizmet);
        }
        HomePageViewModel by = new HomePageViewModel();
        public PartialViewResult AnasayfaProje()
        {
            by.kat = db.ProjeKategoriler.ToList();
            by.proje = db.Projeler.Where(x => x.Durum == 1).Take(6).ToList();


            return PartialView(by);
            //var AnasayfaProje = db.Projeler.Where(i => i.Durum == 1).OrderByDescending(i => i.ProjeID).Take(6).ToList();
            //return PartialView(AnasayfaProje);
        }

        public PartialViewResult AnasayfaProjeKAt()
        {
            var AnasayfaProjeKAt = db.ProjeKategoriler.ToList();
            return PartialView(AnasayfaProjeKAt);
        }

        public PartialViewResult AnasayfaSonBlog()
        {
            var AnasayfaSonBlog = db.Blog.Where(i => i.durum == 1).OrderByDescending(i => i.BlogID).Take(3).ToList();
            return PartialView(AnasayfaSonBlog);
        }

        public PartialViewResult SiteBilgi()
        {
            var SiteBilgi = db.Bilgi.ToList();
            return PartialView(SiteBilgi);
        }




    }
}