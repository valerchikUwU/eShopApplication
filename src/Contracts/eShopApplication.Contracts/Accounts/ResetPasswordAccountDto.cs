using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Accounts
{
    public class ResetPasswordAccountDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Token { get; set; }
    }
}
