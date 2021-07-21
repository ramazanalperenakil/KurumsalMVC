using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalMVC.Models.Entitiy;
using PagedList;
using PagedList.Mvc;


namespace KurumsalMVC.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        KurumsalMVCEntities db = new KurumsalMVCEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var bloglar = db.Blog.Where(i => i.durum == 1).OrderByDescending(i => i.BlogID).ToList().ToPagedList(sayfa, 1);
            return View(bloglar);
        }

        public ActionResult BlogDetay(int? id, Blog b)
        {
            var detay = db.Blog.Where(x => x.BlogID == id && x.durum == 1).ToList();
            //var datay2 = db.Blog.FirstOrDefault(y => y.BlogID == id);

            var idcek = db.Blog.FirstOrDefault(y => y.BlogID == id);
            string blogid = idcek.BlogID.ToString();
            if (Session[blogid] == null)
            {

                Session[blogid] = blogid;
                var blg = db.Blog.FirstOrDefault(y => y.BlogID == id);
                //blg.OkumaSayisi = b.OkumaSayisi++;
                b.OkumaSayisi = blg.OkumaSayisi++;
                db.SaveChanges();
                Session[blogid] = "okundu";


            }






            return View(detay);

        }

        public PartialViewResult Kategoriler()
        {
            var Kategoriler = db.ProjeKategoriler.ToList();
            return PartialView(Kategoriler);
        }

        public PartialViewResult Populer()
        {
            var Populer = db.Blog.Where(x => x.durum == 1).OrderByDescending(i => i.OkumaSayisi).Take(3).ToList();
            return PartialView(Populer);
        }
    }
}