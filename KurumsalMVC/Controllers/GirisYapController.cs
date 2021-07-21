using KurumsalMVC.Models;
using KurumsalMVC.Models.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KurumsalMVC.Controllers
{
    public class GirisYapController : Controller
    {
        KurumsalMVCEntities db = new KurumsalMVCEntities();
        // GET: GirisYap
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanicilar ad)
        {
            var bilgiler = db.Kullanicilar.FirstOrDefault(x => x.Eposta == ad.Eposta);
            if (bilgiler != null)
            {
                if (bilgiler.sifre == seo1.MD5(ad.sifre))
                {
                    FormsAuthentication.SetAuthCookie(bilgiler.sifre, false);
                    Session["KullaniciID"] = bilgiler.KullaniciID;
                    TempData["KullaniciID"] = bilgiler.KullaniciID;

                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    ViewBag.Error= "Hata"; 
                    return View();
                }
               
            }
            else
            {
                ViewBag.Error = "Hata";
                return View();
            }

        }

        public ActionResult Cik()
        {
            FormsAuthentication.SignOut();
            TempData["KullaniciID"] = null;
            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult Reset()
        {
            return View();
        }


        public string sifreYap(int uzunluk)
        {
            string karakterler = "#@!+-,;*abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random karaktersec = new Random();
            char[] karakter = new char[uzunluk];

            for (int i = 0; i < uzunluk; i++)
            {
           
                karakter[i] = karakterler[Convert.ToInt32((karakterler.Length - 1) * karaktersec.NextDouble())];
            }
            return new string(karakter);
        }
        [HttpPost]
        public ActionResult Reset(Contact model,Kullanicilar b)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var kullanicikod = db.Kullanicilar.Where(i => i.Eposta == model.Email).FirstOrDefault();
                    var body = new StringBuilder();
                    body.AppendLine("E-Mail Adresi: " + model.Email);

                    string YeniSifre = sifreYap(8);
                    string YeniSifreMD5 = seo1.MD5(YeniSifre);
                    kullanicikod.sifre = YeniSifreMD5;
                    db.SaveChanges();
                    body.AppendLine("Yeni Şifreniz: " + YeniSifre );
                    Mail.MailSender(body.ToString(),model.Email);
                     
                    return RedirectToAction("Login", "GirisYap");

                }
                else
                {
                    ViewBag.Error = true;
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