using eShopApplication.Contracts.AccountRole;
using eShopApplication.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.AccountRole.Service
{
    public interface IAccountRoleService
    {
        Task<Guid> AddAccountRoleAsync(CreateAccountRoleDto createAccountRoleDto, CancellationToken cancellationToken);

        Task DeleteAccountRoleAsync(Guid id, CancellationToken cancellationToken);

        Task<List<ReadAccountRoleDto>> GetAllAccountRolesAsync(CancellationToken cancellationToken);
    }
}
