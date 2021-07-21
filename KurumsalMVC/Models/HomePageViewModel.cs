using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KurumsalMVC.Models.Entitiy;
using PagedList;

namespace KurumsalMVC.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<ProjeKategoriler> kat { get; set; }
        public IEnumerable<Projeler> proje { get; set; }


        public IEnumerable<Projeler> pr { get; set; }
        public IEnumerable<ProjeResimler> pres { get; set; }


    }
}