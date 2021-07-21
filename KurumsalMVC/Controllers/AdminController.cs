using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KurumsalMVC.Models;
using KurumsalMVC.Models.Entitiy;
using PagedList;

namespace KurumsalMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        KurumsalMVCEntities db = new KurumsalMVCEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BlogSil(int id)
        {
            var b = db.Blog.Find(id);
            db.Blog.Remove(b);
            db.SaveChanges();
            TempData["Başarılı"] = "SilT";
            return RedirectToAction("TumMakaleler");
        }
        public ActionResult TumMakaleler()
        {
            string kulid = TempData["KullaniciID"].ToString();
            int intid = Convert.ToInt32(kulid);
            var bloglar = db.Blog.Where(i => i.Yazar==intid ).OrderByDescending(i => i.BlogID).ToList();
            //var bloglar = db.Blog.OrderByDescending(i => i.BlogID).ToList();
            TempData["KullaniciID"] = kulid;
            return View(bloglar);
        }


        public ActionResult BlogGetir(int id)
        {
            var b1 = db.Blog.Find(id);
            return View("BlogGetir", b1);

        }


        [ValidateInput(false)]
        public ActionResult BlogGuncelle(Blog b)
        {
            var blg = db.Blog.Find(b.BlogID);
            blg.Yazi = b.Yazi;
            blg.Baslik = b.Baslik;
            blg.Tarih = b.Tarih;
            blg.durum = b.durum;
            HttpPostedFileBase photo = Request.Files["BlogImage"];
            if (photo != null && photo.ContentLength > 0)
            {
                string dosyaadi = seo1.AdresDuzenle(blg.Baslik) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(photo.FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/uploads/makale/" + dosyaadi /*+ uzanti*/;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                blg.Resim = dosyaadi /*+ uzanti*/;

                b.Resim = blg.Resim;

                db.SaveChanges();
            }
            db.SaveChanges();
            TempData["Guncelle"] = "Guncellendi";
            return RedirectToAction("TumMakaleler");

        }




        [HttpGet]
        public ActionResult YeniMakale()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult YeniMakale(Blog p)
        {
            try
            {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = seo1.AdresDuzenle(p.Baslik) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/uploads/makale/" + dosyaadi /*+ uzanti*/;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.Resim = dosyaadi /*+ uzanti*/;
                //p.SeoUrl = seo1.AdresDuzenle(p.Baslik);
                int sayi = 50;
                p.OkumaSayisi = sayi;
                string yazarid = TempData["KullaniciID"].ToString();
                p.Yazar = Convert.ToInt32(yazarid);
                p.Tarih = DateTime.Now;
                db.Blog.Add(p);
                db.SaveChanges();

                ViewBag.Success = "KayıtT";
                ModelState.Clear();

            }
            else
            {
                ViewBag.Error = "KayıtF";
            }


            }
            catch
            {
               ViewBag.Error = "KayıtF";

            }

            return View();
        }





        [HttpGet]
        public ActionResult YeniHizmet()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult YeniHizmet(Hizmetler h)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    string dosyaadi = seo1.AdresDuzenle(h.HizmetAdi) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/uploads/hizmetler/" + dosyaadi /*+ uzanti*/;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    h.HizmetResim = dosyaadi /*+ uzanti*/;
                    //p.SeoUrl = seo1.AdresDuzenle(p.Baslik);


                    db.Hizmetler.Add(h);
                    db.SaveChanges();

                    ViewBag.Success = "KayıtT";
                    ModelState.Clear();

                }
                else
                {
                    ViewBag.Error = "KayıtF";
                }


            }
            catch
            {
                ViewBag.Error = "KayıtF";

            }

            return View();
        }


        public ActionResult HizmetGetir(int id)
        {
            var h = db.Hizmetler.Find(id);
            return View("HizmetGetir", h);

        }

        [ValidateInput(false)]
        public ActionResult HizmetGuncelle(Hizmetler b)
        {
            var blg = db.Hizmetler.Find(b.HizmetID);
            blg.Yazi = b.Yazi;
            blg.HizmetAdi = b.HizmetAdi;

            blg.durum = b.durum;
            HttpPostedFileBase photo = Request.Files["BlogImage"];
            if (photo != null && photo.ContentLength > 0)
            {
                string dosyaadi = seo1.AdresDuzenle(blg.HizmetAdi) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(photo.FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/uploads/hizmetler/" + dosyaadi /*+ uzanti*/;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                blg.HizmetResim = dosyaadi /*+ uzanti*/;

                b.HizmetResim = blg.HizmetResim;
                blg.HizmetResim = b.HizmetResim;

                db.SaveChanges();
            }
            db.SaveChanges();
            TempData["Guncelle"] = "Guncellendi";
            return RedirectToAction("TumHizmetler");

        }

        public ActionResult TumHizmetler()
        {
            var hizmetler = db.Hizmetler.OrderByDescending(i => i.HizmetID).ToList();
            return View(hizmetler);
        }

        public ActionResult HizmetSil(int id)
        {
            var b = db.Hizmetler.Find(id);
            db.Hizmetler.Remove(b);
            db.SaveChanges();
            TempData["Başarılı"] = "SilT";
            return RedirectToAction("TumHizmetler");
        }


        [HttpGet]
        public ActionResult YeniProje()
        {

            IEnumerable<SelectListItem> degerler = (from i in db.ProjeKategoriler.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = i.KategoriAd,
                                                        Value = i.ProjeKatID.ToString()
                                                    }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]

        public ActionResult YeniProje(Projeler h)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    string dosyaadi = seo1.AdresDuzenle(h.Baslik) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/uploads/projeler/" + dosyaadi /*+ uzanti*/;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    h.Resim = dosyaadi /*+ uzanti*/;
                    //p.SeoUrl = seo1.AdresDuzenle(p.Baslik);

                    var ktg = db.ProjeKategoriler.Where(m => m.ProjeKatID == h.ProjeKategoriler.ProjeKatID).FirstOrDefault();
                    h.ProjeKategoriler = ktg;
                    h.Tarih = DateTime.Now;
                    db.Projeler.Add(h);
                    db.SaveChanges();

                    ViewBag.Success = "KayıtT";
                    ModelState.Clear();
                    IEnumerable<SelectListItem> degerler = (from i in db.ProjeKategoriler.ToList()
                                                            select new SelectListItem
                                                            {
                                                                Text = i.KategoriAd,
                                                                Value = i.ProjeKatID.ToString()
                                                            }).ToList();
                    ViewBag.dgr = degerler;



                }
                else
                {


                    ViewBag.Error = "KayıtF";
                }


            }
            catch
            {
                ViewBag.Error = "KayıtF";

            }



            int id = db.Projeler.Max(u => u.ProjeID);
            return RedirectToAction("GaleriEkle", "Admin", new { @id = id });
            //return RedirectToAction("GaleriEkle", id );
        }

        [HttpGet]
        public ActionResult GaleriEkle(int id)
        {
            var resimler = db.ProjeResimler.Where(x => x.ProjeID == id).OrderByDescending(i => i.ResimID).ToList();
            return View(resimler);


        }
        [HttpPost]
        public ActionResult GaleriEkle(int id, ProjeResimler p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/uploads/projeler/galeri/" + dosyaadi /*+ uzanti*/;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.ResimURL = dosyaadi /*+ uzanti*/;
                //p.SeoUrl = seo1.AdresDuzenle(p.Baslik);
                p.ProjeID = id;
                db.ProjeResimler.Add(p);
                db.SaveChanges();

                ViewBag.Success = "KayıtT";
                ModelState.Clear();

            }
            else
            {
                ViewBag.Error = "KayıtF";
            }
            var resimler = db.ProjeResimler.Where(x => x.ProjeID == id).OrderByDescending(i => i.ResimID).ToList();
            return View(resimler);


        }


        public ActionResult GaleriFotoSil(int id)
        {

            var b = db.ProjeResimler.Find(id);
            db.ProjeResimler.Remove(b);
            db.SaveChanges();
            TempData["GaleriSil"] = "SilT";


            return RedirectToAction("TumProjeler");
        }

        public ActionResult TumProjeler()
        {
            var p = db.Projeler.OrderByDescending(i => i.ProjeID).ToList();
            return View(p);
        }

        public ActionResult ProjeGetir(int id)
        {
            var ktgr = db.ProjeKategoriler.Find(id);
            List<SelectListItem> degerler = (from i in db.ProjeKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.ProjeKatID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            var h = db.Projeler.Find(id);
            return View("ProjeGetir", h);

        }

        [ValidateInput(false)]
        public ActionResult ProjeGuncelle(Projeler b)
        {
            var blg = db.Projeler.Find(b.ProjeID);
            blg.Yazi = b.Yazi;
            blg.Baslik = b.Baslik;


            blg.Durum = b.Durum;
            HttpPostedFileBase photo = Request.Files["BlogImage"];
            if (photo != null && photo.ContentLength > 0)
            {
                string dosyaadi = seo1.AdresDuzenle(blg.Baslik) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(photo.FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/uploads/projeler/" + dosyaadi /*+ uzanti*/;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                blg.Resim = dosyaadi /*+ uzanti*/;

                b.Resim = blg.Resim;
                var ktg = db.ProjeKategoriler.Where(m => m.ProjeKatID == b.ProjeKategoriler.ProjeKatID).FirstOrDefault();
                b.KategoriID = ktg.ProjeKatID;
                blg.KategoriID = b.KategoriID;

                db.SaveChanges();
            }
            else
            {
                var ktgs = db.ProjeKategoriler.Where(m => m.ProjeKatID == b.ProjeKategoriler.ProjeKatID).FirstOrDefault();
                b.KategoriID = ktgs.ProjeKatID;
                blg.KategoriID = b.KategoriID;
            }

            db.SaveChanges();
            TempData["Guncelle"] = "Guncellendi";
            return RedirectToAction("TumProjeler");

        }

        public ActionResult ProjeSil(int id)
        {
            //var projeid = db.ProjeResimler.Find(id);
            //db.ProjeResimler.Remove(projeid);
            db.ProjeResimler.RemoveRange(db.ProjeResimler.Where(c => c.ProjeID == id));
            db.SaveChanges();



            var b = db.Projeler.Find(id);
            db.Projeler.Remove(b);
            db.SaveChanges();
            TempData["Başarılı"] = "SilT";
            return RedirectToAction("TumProjeler");
        }



        [HttpGet]
        public ActionResult ProjeKategoriler()
        {
            return View();

        }

        [HttpPost]
        public ActionResult ProjeKategoriler(ProjeKategoriler p)
        {
            p.KategoriURL = seo1.AdresDuzenle(p.KategoriAd);
            p.YaziSayisi = 0;
            db.ProjeKategoriler.Add(p);
            db.SaveChanges();
            TempData["ProjeKatGuncellendi"] = "ProjeKatGuncellendi";
            return View("ProjeKategoriler");
        }

        public ActionResult KategoriSil(int id)
        {
            var b = db.ProjeKategoriler.Find(id);
            db.ProjeKategoriler.Remove(b);
            db.SaveChanges();
            TempData["Başarılı"] = "SilT";
            return RedirectToAction("ProjeKategoriler");
        }


        public PartialViewResult KategoriListele()
        {
            var p = db.ProjeKategoriler.OrderByDescending(i => i.ProjeKatID).ToList();
            return PartialView(p);
        }



        public ActionResult ProjeKategoriGetir(int id)
        {
            var h = db.ProjeKategoriler.Find(id);
            return View("ProjeKategoriGetir", h);

        }


        public ActionResult KatGuncelle(ProjeKategoriler b)
        {
            var blg = db.ProjeKategoriler.Find(b.ProjeKatID);
            blg.KategoriAd = b.KategoriAd;
            b.KategoriURL = seo1.AdresDuzenle(blg.KategoriAd);



            db.SaveChanges();
            TempData["Guncelle"] = "Guncellendi";
            return RedirectToAction("ProjeKategoriler");

        }

        public PartialViewResult ProjeGaleri()
        {
            var p = db.ProjeResimler.OrderByDescending(i => i.Projeler.ProjeID).ToList();
            return PartialView(p);
        }


        public ActionResult Hakkimizda()
        {
            var b1 = db.Hakkimizda.First();
            return View("Hakkimizda", b1);
        }


        [ValidateInput(false)]
        public ActionResult HakkimizdaGuncelle(Hakkimizda b)
        {
            var blg = db.Hakkimizda.First();
            blg.Yazi = b.Yazi;
            blg.Baslik = b.Baslik;


            HttpPostedFileBase photo = Request.Files["BlogImage"];
            if (photo != null && photo.ContentLength > 0)
            {
                string dosyaadi = seo1.AdresDuzenle(blg.Baslik) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(photo.FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/uploads/" + dosyaadi /*+ uzanti*/;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                blg.Resim = dosyaadi /*+ uzanti*/;

                b.Resim = blg.Resim;

                db.SaveChanges();
            }
            db.SaveChanges();
            //ViewBag.Success = "GuncelleT";
            TempData["Başarılı"] = "OK";
            //TempData["Hata"] = "NO";
            return RedirectToAction("Hakkimizda");
            //var b1 = c.Blogs.Find(id);
            //return View("BlogGetir", b1);
        }


        public ActionResult Slider()
        {

            return View();
        }


        public ActionResult SliderEkle(Slider p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/uploads/slider/" + dosyaadi /*+ uzanti*/;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.Resim = dosyaadi /*+ uzanti*/;
                //p.SeoUrl = seo1.AdresDuzenle(p.Baslik);
                p.Durum = 1;
                db.Slider.Add(p);
                db.SaveChanges();

                ViewBag.Success = "KayıtT";
                ModelState.Clear();


            }
            else
            {
                ViewBag.Error = "KayıtF";
            }
            var resimler = db.Slider.OrderByDescending(i => i.SliderID).ToList();
            //return View(resimler);

            return View("Slider");


        }


        public ActionResult SliderFotoSil(int id)
        {

            var b = db.Slider.Find(id);
            db.Slider.Remove(b);
            db.SaveChanges();
            TempData["GaleriSil"] = "SilT";


            return RedirectToAction("Slider");
        }

        public PartialViewResult SliderListele()
        {
            var resimler = db.Slider.OrderByDescending(i => i.SliderID).ToList();
            return PartialView(resimler);
        }

        public ActionResult iletisim()
        {
            var b1 = db.iletisim.First();
            return View("iletisim", b1);
        }

        [ValidateInput(false)]
        public ActionResult İletisimGuncelle(iletisim b)
        {
            var blg = db.iletisim.First();
            blg.HaritaURL = b.HaritaURL;
            blg.Baslik = b.Baslik;
            db.SaveChanges();
            TempData["Başarılı"] = "OK";
            return RedirectToAction("iletisim");

        }


        public ActionResult Ayar()
        {
            var b1 = db.Bilgi.First();
            return View("Ayar", b1);
        }

        [ValidateInput(false)]
        public ActionResult AyarGuncelle(Bilgi b)
        {
            var blg = db.Bilgi.First();
            blg.tel = b.tel;
            blg.eposta = b.eposta;
            blg.CPR = b.CPR;
            blg.face = b.face;
            blg.twit = b.twit;
            blg.linkedin = b.linkedin;
            blg.skyp = b.skyp;
            blg.adres = b.adres;
            blg.saat = b.saat;
            blg.SiteBaslik = b.SiteBaslik;
            blg.SiteAnahtar = b.SiteAnahtar;
            blg.SiteAciklama = b.SiteAciklama;


            HttpPostedFileBase photo = Request.Files["BlogImage"];
            if (photo != null && photo.ContentLength > 0)
            {
                string dosyaadi = seo1.AdresDuzenle(blg.SiteBaslik) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(photo.FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/uploads/" + dosyaadi /*+ uzanti*/;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                blg.SİteLogo = dosyaadi /*+ uzanti*/;

                b.SİteLogo = blg.SİteLogo;

                db.SaveChanges();
            }
            db.SaveChanges();
            //ViewBag.Success = "GuncelleT";
            TempData["Başarılı"] = "OK";
            //TempData["Hata"] = "NO";
            return RedirectToAction("Ayar");
            //var b1 = c.Blogs.Find(id);
            //return View("BlogGetir", b1);
        }
        public ActionResult Profil()
        {
            var b1 = db.Kullanicilar.First();
            return View("Profil", b1);
        }





        public ActionResult TumKullanicilar()
        {
            var p = db.Kullanicilar.OrderByDescending(i => i.KullaniciID).ToList();
            return View(p);
        }


        [HttpGet]
        public ActionResult KullaniciEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult KullaniciEkle(Kullanicilar p)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    string dosyaadi = seo1.AdresDuzenle(p.Ad) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/uploads/kullanicilar/" + dosyaadi /*+ uzanti*/;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    p.foto = dosyaadi /*+ uzanti*/;

                    p.sifre = seo1.MD5(p.sifre);
                    db.Kullanicilar.Add(p);
                    db.SaveChanges();

                   
                   
                    ViewBag.Success = "KayıtT";
                    ModelState.Clear();

                }
                else
                {
                    ViewBag.Error = "KayıtF";
                }


            }
            catch
            {
                ViewBag.Error = "KayıtF";

            }

            return View();
        }


        public ActionResult KullaniciGetir(int id)
        {
             

            var h = db.Kullanicilar.Find(id);
            return View("KullaniciGetir", h);

        }


        public ActionResult KullaniciGuncelle(Kullanicilar b)
        {
            var blg = db.Kullanicilar.Find(b.KullaniciID);
            if (b.sifre!=null)
            {
                blg.Ad = b.Ad;
                blg.Soyad = b.Soyad;
                blg.Eposta = b.Eposta;
                blg.sifre = seo1.MD5(b.sifre);
                HttpPostedFileBase photo = Request.Files["BlogImage"];
                if (photo != null && photo.ContentLength > 0)
                {
                    string dosyaadi = seo1.AdresDuzenle(blg.Ad) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(photo.FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/uploads/kullanicilar/" + dosyaadi /*+ uzanti*/;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    blg.foto = dosyaadi /*+ uzanti*/;

                    b.foto = blg.foto;
                     

                    db.SaveChanges();
                }
                 
                db.SaveChanges();
                TempData["Guncelle"] = "Guncellendi";
                return RedirectToAction("TumKullanicilar");
            }
            else
            {
                blg.Ad = b.Ad;
                blg.Soyad = b.Soyad;
                blg.Eposta = b.Eposta;
                HttpPostedFileBase photo = Request.Files["BlogImage"];
                if (photo != null && photo.ContentLength > 0)
                {
                    string dosyaadi = seo1.AdresDuzenle(blg.Ad) + "-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + "-" + Path.GetFileName(photo.FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/uploads/kullanicilar/" + dosyaadi /*+ uzanti*/;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    blg.foto = dosyaadi /*+ uzanti*/;

                    b.foto = blg.foto;
                     
                     

                    db.SaveChanges();
                }
                 

                db.SaveChanges();
                TempData["Guncelle"] = "Guncellendi";
                return RedirectToAction("TumKullanicilar");
            }
            


           
         

        }

        public ActionResult KullaniciSil(int id)
        {
            try
            {
                var b = db.Kullanicilar.Find(id);
                db.Kullanicilar.Remove(b);
                db.SaveChanges();
                TempData["Başarılı"] = "SilT";
                return RedirectToAction("TumKullanicilar");
            }
            catch  
            {
                TempData["Hata"] = "Silf";
                return RedirectToAction("TumKullanicilar");

            }
            
        }
       

    }
}