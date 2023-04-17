using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.AccountRole.Repository
{
    public interface IAccountRoleRepository
    {
        Task<Guid> AddAccountRoleAsync(Domain.AccountRole.AccountRole accountRole, CancellationToken cancellationToken);

        Task DeleteAccountRoleAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Domain.AccountRole.AccountRole>> GetAllAccountRolesAsync(CancellationToken cancellationToken);
    }
}
