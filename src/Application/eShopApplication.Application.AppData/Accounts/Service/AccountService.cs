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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace eShopApplication.Application.AppData.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _сonfiguration;

        public AccountService(
        IAccountRepository accountRepository,
        IHttpContextAccessor httpContextAccesso,
        IConfiguration сonfiguration)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccesso;
            _сonfiguration = сonfiguration;
        }
        public async Task<Guid> RegisterAccountAsync(CreateAccountDto accountDto, CancellationToken cancellation)
        {
            var account = new Domain.Account.Account
            {
                Name = accountDto.Login,
                LastName = accountDto.LastName,
                PhoneNumber = accountDto.PhoneNumber,
                NickName = accountDto.NickName,
                Login = accountDto.Login,
                Password = accountDto.Password,
                RegistrationDate = DateTime.UtcNow
            };

            var existingAccount = await _accountRepository.FindWhere(account => account.Login == accountDto.Login, cancellation);
            if (existingAccount != null)
            {
                throw new Exception($"Пользователь с логином '{accountDto.Login}' уже зарегистрирован!");
            }

            await _accountRepository.AddAsync(account, cancellation);

            return account.Id;
        }

        /// <inheritdoc />
        public async Task<string> LoginAsync(LoginAccountDto accountDto, CancellationToken cancellation)
        {
            var existingAccount = await _accountRepository.FindWhere(account => account.Login == accountDto.Login, cancellation);
            if (existingAccount == null)
            {
                throw new Exception("Пользователь не найден!");
            }

            if (!existingAccount.Password.Equals(accountDto.Password))
            {
                throw new Exception("Неверный логин или пароль.");
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, existingAccount.Id.ToString()),
            new Claim(ClaimTypes.Name, existingAccount.Login)
        };

            var secretKey = _сonfiguration["Jwt:Key"];

            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256
                    )
                );

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }

        /// <inheritdoc />
        public async Task<ReadAccountDto> GetCurrentAsync(CancellationToken cancellation)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return null;
            }

            var id = Guid.Parse(claimId);
            var user = await _accountRepository.FindById(id, cancellation);

            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'.");
            }

            //TODO
            var result = new ReadAccountDto
            {
                Name = user.Login,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                NickName = user.NickName,
                Login = user.Login,
                RegistrationDate = DateTime.UtcNow
            };

            return result;
        }
    }
}
