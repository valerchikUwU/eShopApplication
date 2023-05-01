using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger) 
        {
            _logger = logger;
        }

        public bool SendEmailPasswordReset(string email)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("maksim4eg6@mail.ru");
            mailMessage.To.Add(new MailAddress(email));

            mailMessage.Subject = "Password Reset";
            mailMessage.Body = "dsgsdgsdg";

            SmtpClient client= new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("maksim4eg6@mail.ru", "KAKASHKa90");
            client.Host = "smtpout.secureserver.net";
            client.Port = 527;

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
