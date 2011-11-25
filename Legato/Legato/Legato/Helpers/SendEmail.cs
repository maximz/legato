using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Legato.Helpers
{
    /// <summary>
    /// Handles all email sending functions.
    /// </summary>
    public static class SendEmail
    {
        /// <summary>
        /// Sends the specified System.Net.Mail.MailMessage from our Legato Network mailer account.
        /// </summary>
        /// <param name="m">The m.</param>
        public static void Send(MailMessage m)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                //Credentials = new NetworkCredential("maximz.mailer@gmail.com", "MailMarvin")
                Credentials = new NetworkCredential("mailer@legatonetwork.com", "gHds178l")
            };

            smtp.Send(m);
        }

        /// <summary>
        /// Returns a System.Net.Mail.MailMessage with properties set to values for a standard no-reply message.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">if set to <c>true</c> [is body HTML].</param>
        /// <returns></returns>
        public static MailMessage StandardNoReply(string to, string subject, string body, bool isBodyHtml)
        {
            var netEmailMessage = new MailMessage()
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml,
                From = new System.Net.Mail.MailAddress("no-reply@legatonetwork.com", "Legato Network"),
                Priority = System.Net.Mail.MailPriority.Normal,
                BodyEncoding = Encoding.UTF8
            };
            netEmailMessage.To.Add(new System.Net.Mail.MailAddress(to));

            return netEmailMessage;
        }
    }
}