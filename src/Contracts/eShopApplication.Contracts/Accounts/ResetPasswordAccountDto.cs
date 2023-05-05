using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Accounts
{
    /// <summary>
    /// Модель для восстановления пароля
    /// </summary>
    public class ResetPasswordAccountDto
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string ConfirmedPassword { get; set; }
        /// <summary>
        /// Токен восстановления
        /// </summary>
        public string Token { get; set; }
    }
}
