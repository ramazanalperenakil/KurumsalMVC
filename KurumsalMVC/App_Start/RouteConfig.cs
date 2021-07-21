using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KurumsalMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Anasayfa",
                url: "",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Anasayfa2",
              url: "Anasayfa",
              defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: "Hakkimizda",
               url: "Hakkimizda",
               defaults: new { controller = "Hakkimizda", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Projeler",
                url: "Projeler",
                defaults: new { controller = "Projeler", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ProjeDetay",
               url: "Proje/{baslik}-{id}",
               defaults: new { controller = "Projeler", action = "ProjeDetay", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Hizmetler",
              url: "Hizmetler",
              defaults: new { controller = "Hizmetler", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "HizmetDetay",
              url: "Hizmet/{baslik}-{id}",
              defaults: new { controller = "Hizmetler", action = "HizmetDetay", id = UrlParameter.Optional }
            );


            routes.MapRoute(
              name: "Blog",
              url: "Makaleler",
              defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "BlogDetay",
              url: "Makale/{baslik}-{id}",
              defaults: new { controller = "Blog", action = "BlogDetay", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "İletişim",
              url: "iletisim",
              defaults: new { controller = "İletisim", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
              name: "ProjeKategori",
              url: "Kategori/{baslik}-{id}",
              defaults: new { controller = "Projeler", action = "Kategorisel", id = UrlParameter.Optional }
           );




        


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
