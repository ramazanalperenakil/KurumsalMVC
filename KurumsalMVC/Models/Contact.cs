using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KurumsalMVC.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string IP { get; set; }
    }
}