using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace KurumsalMVC.Models
{
    public class Mail
    {
        public static void MailSender(string body,string gidecekeposta)
        {
            var fromAddress = new MailAddress("raailetisim@gmail.com");
            var toAddress = new MailAddress(gidecekeposta);
            const string subject = "Site Adı | Sitenizden Gelen İletişim Formu";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "793367c.")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}