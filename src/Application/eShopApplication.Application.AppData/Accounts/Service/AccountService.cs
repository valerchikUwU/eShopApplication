using eShopApplication.Application.AppData.Accounts.Repository;
using eShopApplication.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Principal;
using eShopApplication.Domain.Account;

namespace eShopApplication.Application.AppData.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService (IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> CreateAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellation)
        {
            var account = new Domain.Account.Account
            {
                Name = createAccountDto.Name,
                Password = createAccountDto.Password,
                Email = createAccountDto.Email
            };
            return await _repository.AddAccountAsync(account, cancellation);
        }

        public async Task<ReadAccountDto> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var account = await _repository.GetAccountByIdAsync(id, cancellationToken);
            var result = new ReadAccountDto
            {
                Id = account.Id,
                Name = account.Name,
                Password = account.Password,
                Email = account.Email,
                RegistrationDate = account.RegistrationDate
            };
            return result;
        }

        public async Task<List<ReadAccountDto>> GetAccountsByNameAsync(string name, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetAccountsByNameAsync(name, cancellationToken);
            var result = accounts.Select(s => new ReadAccountDto
            {
                Id = s.Id,
                Name = s.Name,
                Password = s.Password,
                Email = s.Email,
                RegistrationDate = s.RegistrationDate
            });
            return result.ToList();
        }

        public async Task<List<ReadAccountDto>> GetAll(CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetAllAsync(cancellationToken);
            var result = accounts.Select(s => new ReadAccountDto
            {
                Id = s.Id,
                Name = s.Name,
                Password = s.Password,
                Email = s.Email,
                RegistrationDate = s.RegistrationDate
            });
            return result.ToList();
        }

        Task<Guid> IAccountService.DeleteAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Guid> IAccountService.UpdateAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
