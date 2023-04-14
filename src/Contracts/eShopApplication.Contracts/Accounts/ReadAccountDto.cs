using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Accounts
{
    public class ReadAccountDto
    {
      
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string PhoneNumber { get; set; }


        public string NickName { get; set; }

        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Признак блокировки.
        /// </summary>
        public bool IsBlocked { get; set; }

    }
}
