using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.EmailService
{
    public interface IEmailService
    {
        public bool SendEmailPasswordReset(string email, string link);
    }
}
