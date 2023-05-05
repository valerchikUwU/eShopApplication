using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Accounts
{
    /// <summary>
    /// Токен восстановления пароля
    /// </summary>
    public class ResetPasswordTokenAccountDto
    {
        public string Login { get; set; }
    }
}
