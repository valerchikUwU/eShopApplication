using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.EmailService
{
    /// <summary>
    /// Сервис для отправки электронных писем
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправить письмо
        /// </summary>
        /// <param name="email">Почта пользователя, запрашивающего восстановление пароля</param>
        /// <param name="link">Ссылка восстановления</param>
        /// <returns></returns>
        public bool SendEmailPasswordReset(string email, string link);
    }
}
