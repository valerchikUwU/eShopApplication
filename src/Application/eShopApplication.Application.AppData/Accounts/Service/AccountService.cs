using AutoMapper;
using eShopApplication.Application.AppData.Account.Repositories;
using eShopApplication.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;
        public async Task<ReadAccountDto> CreateAccount(CreateAccountDto createAccountDto, CancellationToken cancellation)
        {
            var accountModel = _mapper.Map<eShopApplication.Domain.Account>(createAccountDto);
            await _repository.CreateAccount(accountModel, cancellation);

        }
    }
}
