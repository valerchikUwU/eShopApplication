using eShopApplication.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Account.Services
{
    /// <summary>
    /// TODO: Подлежит доработке
    /// </summary>
    public interface IAccountService
    {
        ///
        Task<Guid> CreateAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellation);
        Task<ReadAccountDto> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ReadAccountDto>> GetAccountsByNameAsync(string name, CancellationToken cancellationToken);
        Task<List<ReadAccountDto>> GetAll(CancellationToken cancellationToken);
        Task<Guid> DeleteAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellationToken);
        Task<Guid> UpdateAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellationToken);

    }
}
