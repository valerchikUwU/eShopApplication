using eShopApplication.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Account.Repositories
{
    public interface IAccountRepository
    {
        Task<ReadAccountDto> CreateAccount(CreateAccountDto createAccountDto, CancellationToken cancellation);
        Task<ReadAccountDto> GetAccountById(Guid id, CancellationToken cancellationToken);
        Task<ReadAccountDto> GetAccountByName(string name, CancellationToken cancellationToken);
    }
}
