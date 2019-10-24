using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Shipping.ViewModels.Account;

namespace Shipping.Utilities
{
    public static class Email
    {
        public static bool send(SentEmailViewModel email)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sandeepreddyg77@gmail.com");
            
            if (email.CC != null)
            {
                string[] multipleCc = email.CC.Split(';');
                foreach (var cc in multipleCc)
                {
                    mail.CC.Add(new MailAddress(cc));
                }
            }
            string[] multipleTo = email.To.Split(';');
            foreach (var to in multipleTo)
            {
                mail.To.Add(new MailAddress(to));
            }

            mail.Subject = email.Subject;
            mail.Body = email.Body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                "sandeepreddyg77@gmail.com", "August@2019");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)

            {
                return false;
            }
            return true;
        }
    }
}