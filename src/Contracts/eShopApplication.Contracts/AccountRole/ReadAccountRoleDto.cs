using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.AccountRole
{
    public class ReadAccountRoleDto
    {
        public Guid AccountRoleId { get; set; }

        public string AccountRoleName { get; set; }

        public string AccountRoleDescription { get; set; }
    }
}
