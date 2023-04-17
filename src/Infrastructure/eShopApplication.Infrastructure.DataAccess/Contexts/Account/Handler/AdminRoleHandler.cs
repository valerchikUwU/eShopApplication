using eShopApplication.Infrastructure.DataAccess.Contexts.Account.Requirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Account.Handler
{
    public class AdminRoleHandler : AuthorizationHandler<AdminRoleRequirement>
    {

        private readonly IHttpContextAccessor _contextAccessor;
        public AdminRoleHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRoleRequirement requirement)
        {
            var claims = _contextAccessor.HttpContext.User.Claims;
            var claimRole = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (claimRole is null)
            {
                return Task.CompletedTask;
            }


            if (claimRole == "Admin")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
