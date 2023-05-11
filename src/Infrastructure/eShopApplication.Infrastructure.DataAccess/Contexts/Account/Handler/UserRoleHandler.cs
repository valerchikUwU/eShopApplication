using eShopApplication.Infrastructure.DataAccess.Contexts.Account.Requirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Account.Handler
{
    /// <summary>
    /// Обработчик для оценки требований роли как обычного пользователя
    /// </summary>
    public class UserRoleHandler : AuthorizationHandler<UserRoleRequirement>
    {

        private readonly IHttpContextAccessor _contextAccessor;
        public UserRoleHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Обработчик
        /// </summary>
        /// <param name="context">Контекст сведений об авторизации</param>
        /// <param name="requirement">Требование роли</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRoleRequirement requirement)
        {
            var claims = _contextAccessor.HttpContext.User.Claims;
            var claimRole = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


            if (claimRole is null)
            {
                return Task.CompletedTask;
            }


            if (claimRole == "User")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
