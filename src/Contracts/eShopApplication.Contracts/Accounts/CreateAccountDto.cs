using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Accounts
{
    public class CreateAccountDto
    {    
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string PhoneNumber { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

    }
}
