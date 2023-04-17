using eShopApplication.Application.AppData.AccountRole.Repository;
using eShopApplication.Application.AppData.Accounts.Repository;
using eShopApplication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.AccountRole.Repository
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly IRepository<Domain.AccountRole.AccountRole> _repository;

        public AccountRoleRepository(IRepository<Domain.AccountRole.AccountRole> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddAccountRoleAsync(Domain.AccountRole.AccountRole accountRole, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(accountRole, cancellationToken);
            return accountRole.AccountRoleId;
        }

        public async Task DeleteAccountRoleAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingAccountRole = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingAccountRole == null)
            {
                return;
            }

            await _repository.DeleteAsync(existingAccountRole, cancellationToken);
        }

        public async Task<List<Domain.AccountRole.AccountRole>> GetAllAccountRolesAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAll().ToListAsync(cancellationToken);
        }
    }
}
