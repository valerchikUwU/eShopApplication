using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Account.Requirement
{
    /// <summary>
    /// Класс требований для оценки юзера как обычного пользователя
    /// </summary>
    public class UserRoleRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; }

        public UserRoleRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
