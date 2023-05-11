using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Account.Requirement
{
    /// <summary>
    /// Класс требований для оценки юзера как админа
    /// </summary>
    public class AdminRoleRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; }

        public AdminRoleRequirement(string roleName) 
        {
            RoleName = roleName;
        }
    }
}
