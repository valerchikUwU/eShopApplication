using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Domain.Account
{
    public class Account
    {
        
        public Guid Id { get; set; }

        
        public string Name { get; set; }

        
        public string Email { get; set; }

        
        public string Password { get; set; }

        
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    }
}
