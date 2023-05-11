using eShopApplication.Application.AppData.Accounts.Repository;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Domain.Account;
using eShopApplication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
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

        public async Task<Guid> AddAsync(Domain.Account.Account model, CancellationToken cancellation)
        {
            await _repository.AddAsync(model, cancellation);
            return model.Id;
        }

        public Task<Domain.Account.Account> FindById(Guid id, CancellationToken cancellation)
        {
            return _repository.GetByIdAsync(id, cancellation);
        }

        public async Task<Domain.Account.Account> FindWhere(Expression<Func<Domain.Account.Account, bool>> predicate, CancellationToken cancellation)
        {
            var data = _repository.GetAllFiltered(predicate);

            Domain.Account.Account account = await data.Where(predicate).FirstOrDefaultAsync(cancellation);

            return account;
        }

        public async Task<Guid> UpdateAccountAsync(Domain.Account.Account account, CancellationToken cancellation)
        {
            await _repository.UpdateAsync(account, cancellation);
            return account.Id;
        }
    }
}
