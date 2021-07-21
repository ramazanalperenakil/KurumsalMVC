using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalMVC.Models.Entitiy;

namespace KurumsalMVC.Controllers
{
    public class HakkimizdaController : Controller
    {
        // GET: Hakkimizda
        KurumsalMVCEntities db = new KurumsalMVCEntities();
        public ActionResult Index()
        {
            var Hakkimizda = db.Hakkimizda.ToList();
            return View(Hakkimizda);

            
        }
    }
}