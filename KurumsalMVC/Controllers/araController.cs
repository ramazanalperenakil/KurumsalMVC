using KurumsalMVC.Models.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalMVC.Controllers
{
    public class AraController : Controller
    {
        // GET: ara
        KurumsalMVCEntities db = new KurumsalMVCEntities();
        public ActionResult Index(string searchName)
        {

            searchName = Request.QueryString["q"].ToString();
            var projects = db.Projeler.Where(p => p.Baslik.Contains(searchName) || p.Yazi.Contains(searchName));
           

            return View(projects.ToList());

            //if (String.IsNullOrEmpty(searchName))
            //{

            //}
            //return View( );
            // Filter down if necessary
            //if (!String.IsNullOrEmpty(searchName))
            //{
            //    projects = db.Projeler.Where(p => p.Baslik.Contains(searchName));
            //}
            // Pass your list out to your view


            //var Kategorisel = db.Projeler.Where(i => i.Durum == 1).ToList();
            ////return PartialView(Kategorisel);
            //return View(Kategorisel);


        }


        public ActionResult BlogAra(string searchName)
        {
            // Current projects


            searchName = Request.QueryString["q"].ToString();

            var blogs = db.Blog.Where(p => p.Baslik.Contains(searchName) || p.Yazi.Contains(searchName));
            return View(blogs.ToList());

            //if (String.IsNullOrEmpty(searchName))
            //{

            //}
            //return View( );
            // Filter down if necessary
            //if (!String.IsNullOrEmpty(searchName))
            //{
            //    projects = db.Projeler.Where(p => p.Baslik.Contains(searchName));
            //}
            // Pass your list out to your view


            //var Kategorisel = db.Projeler.Where(i => i.Durum == 1).ToList();
            ////return PartialView(Kategorisel);
            //return View(Kategorisel);


        }

        public PartialViewResult aramabirimi()
        {
            return PartialView();
        }
    }
}