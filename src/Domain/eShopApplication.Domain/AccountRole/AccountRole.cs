using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Domain.AccountRole
{
    public class AccountRole
    {
        public Guid AccountRoleId { get; set; }

        public string AccountRoleName { get; set; }

        public string AccountRoleDescription { get; set;}

        public virtual List<Domain.Account.Account> Accounts { get; set; }

    }
}
