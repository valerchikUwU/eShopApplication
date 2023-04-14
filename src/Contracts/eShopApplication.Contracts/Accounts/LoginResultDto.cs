using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Accounts
{
    /// <summary>
    /// Результат входа в аккаунт.
    /// </summary>
    public class LoginResultDto
    {
        /// <summary>
        /// Токен авторизации.
        /// </summary>
        public string Token { get; set; }
    }
}
