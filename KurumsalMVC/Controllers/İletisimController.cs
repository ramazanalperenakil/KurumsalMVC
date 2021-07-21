using KurumsalMVC.Models;
using KurumsalMVC.Models.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KurumsalMVC.Controllers
{
    public class İletisimController : Controller
    {
        // GET: İletisim

        [HttpGet]
        public ActionResult Index()
        {
             
            return View();
        }


        [HttpPost]
        public ActionResult Index(Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var body = new StringBuilder();
                    body.AppendLine("Ad & Soyad: " + model.Name);
                    body.AppendLine("E-Mail Adresi: " + model.Email);
                    body.AppendLine("Konu: " + model.Subject);
                    body.AppendLine("Mesaj: " + model.Message);
                    body.AppendLine("IP: " + seo1.GetClientIp().ToString());
                    Mail.MailSender(body.ToString(),"ramazanalperenakil@gmail.com");
                }



                ViewBag.Success = true;
            }
            catch  
            {
                ViewBag.Error = true;
            }
           
            return View();
        }
    }
}