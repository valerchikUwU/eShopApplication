using eShopApplication.Application.AppData.Accounts.Repository;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Domain.Account;
using eShopApplication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Account.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IRepository<Domain.Account.Account> _repository;

        public AccountRepository(IRepository<Domain.Account.Account> repository)
        {
            _repository = repository;
        }
        public async Task<Guid> AddAccountAsync(Domain.Account.Account account, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(account, cancellationToken);
            return account.Id;
        }

        public async Task<Guid> DeleteAccount(Domain.Account.Account account, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(account, cancellationToken);
            return account.Id;
        }

        public async Task<Domain.Account.Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken)
        {
             return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<List<Domain.Account.Account>> GetAccountsByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(s => s.Name.Contains(name)).ToListAsync(cancellationToken);
        }

        public async Task<List<Domain.Account.Account>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAll().ToListAsync(cancellationToken);
        }

        public async Task<Guid> UpdateAccount(Domain.Account.Account account, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(account, cancellationToken);
            return account.Id;
        }
    }
}
