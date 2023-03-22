using eShopApplication.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Account.Services
{
    public interface IAccountService
    {
        Task<Guid> CreateAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellation);
        Task<ReadAccountDto> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ReadAccountDto> GetAccountByNameAsync(string name, CancellationToken cancellationToken);
        Task<List<ReadAccountDto>> GetAll(CancellationToken cancellationToken);
        Task DeleteAccountAsync(Domain.Account.Account model, CancellationToken cancellationToken);
        Task UpdateAccountAsync(Domain.Account.Account model, CancellationToken cancellationToken);

    }
}
