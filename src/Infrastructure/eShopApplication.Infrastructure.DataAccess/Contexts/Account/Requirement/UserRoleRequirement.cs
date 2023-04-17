using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Account.Requirement
{
    public class UserRoleRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; }

        public UserRoleRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
