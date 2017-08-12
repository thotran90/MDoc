using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using MDoc.Services.Contract;
using MDoc.Services.Contract.DataContracts;

namespace MDoc.Services.Implements
{
    public class EmailService : IEmailService
    {
        protected const string FromEmail = "congtho90@gmail.com";
        protected const string FromPassword = "941990";
        protected const string SmtpAddres = "smtp.gmail.com";
        protected const int SmtpPort = 587;

        public void SendEmailToUser(EmailModel email)
        {

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail, FromPassword)
            };
            using (var message = new MailMessage(FromEmail, email.ToAddress)
            {
                IsBodyHtml = true,
                Subject = email.Subject,
                Body = email.Body

            })
            {
                smtp.Send(message);
            }


            //using (var mail = new MailMessage())
            //{
            //    mail.From = new MailAddress(FromEmail);
            //    mail.To.Add(email.ToAddress);
            //    mail.Subject = email.Subject;
            //    mail.Body = email.Body;
            //    mail.IsBodyHtml = true;

            //    using (var smtp = new SmtpClient(SmtpAddres, SmtpPort))
            //    {
            //        smtp.Timeout = 10000;
            //        smtp.UseDefaultCredentials = false;
            //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //        smtp.Credentials = new NetworkCredential(FromEmail, FromPassword);
            //        smtp.EnableSsl = true;
            //        smtp.Send(mail);
            //    }
            //}
        }

        public void SendEmailToListOfUser(List<EmailModel> emails)
        {
            throw new NotImplementedException();
        }
    }
}