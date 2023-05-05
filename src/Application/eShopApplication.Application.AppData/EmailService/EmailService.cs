using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.EmailService
{
    /// <inheritdoc cref="IEmailService"/>
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger) 
        {
            _logger = logger;
        }

        /// <inheritdoc cref="IEmailService.SendEmailPasswordReset(string, string)"/>
        public bool SendEmailPasswordReset(string email, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("maksim4eg6@mail.ru");
            mailMessage.To.Add(new MailAddress(email));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Body = link;

            SmtpClient client= new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("maksim4eg6@mail.ru", "Lm9kMCaiNDxQwBVENvTh");
            client.Host = "smtp.mail.ru";
            client.EnableSsl = true;
            client.Port = 25;
            client.DeliveryFormat = SmtpDeliveryFormat.International;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to send message: {message}", mailMessage);
            }
            return false;
        }
    }
}
