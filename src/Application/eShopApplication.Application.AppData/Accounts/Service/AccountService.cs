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
using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Contracts.Adverts;

namespace eShopApplication.Application.AppData.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _сonfiguration;

        public AccountService(
        IAccountRepository accountRepository,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration сonfiguration)
        {
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
            _сonfiguration = сonfiguration;
        }
        public async Task<Guid> RegisterAccountAsync(CreateAccountDto accountDto, CancellationToken cancellationToken)
        {
            var account = new Domain.Account.Account
            {
                Name = accountDto.Login,
                LastName = accountDto.LastName,
                NickName = accountDto.NickName,
                PhoneNumber = accountDto.PhoneNumber,
                Login = accountDto.Login,
                Password = accountDto.Password,
                RegistrationDate = DateTime.UtcNow,
                AccountRoleName = "User",
                AccountRoleId = Guid.Parse("1cc4dad8-6318-4519-bb02-08db3f35bbdf")
            };

            var existingAccount = await _accountRepository.FindWhere(account => account.Login == accountDto.Login, cancellationToken);
            if (existingAccount != null)
            {
                throw new Exception($"Пользователь с логином '{accountDto.Login}' уже зарегистрирован!");
            }

            await _accountRepository.AddAsync(account, cancellationToken);

            return account.Id;
        }

        /// <inheritdoc />
        public async Task<string> LoginAsync(LoginAccountDto accountDto, CancellationToken cancellationToken)
        {
            var existingAccount = await _accountRepository.FindWhere(account => account.Login == accountDto.Login, cancellationToken);
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
                new Claim(ClaimTypes.Name, existingAccount.Login),
                new Claim(ClaimTypes.Role, existingAccount.AccountRoleName)
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
        public async Task<ReadAccountDto> GetCurrentAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return null;
            }

            var id = Guid.Parse(claimId);
            var user = await _accountRepository.FindById(id, cancellationToken);

            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'.");
            }

            var result = new ReadAccountDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                NickName = user.NickName,
                Login = user.Login,
                RegistrationDate = DateTime.UtcNow,
                Claims = claims.ToList(),
            };

            return result;
        }

        public async Task<CreateAccountDto> GetCurrentCreatedDtoAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return null;
            }

            var id = Guid.Parse(claimId);
            var user = await _accountRepository.FindById(id, cancellationToken);

            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'.");
            }

            var result = new CreateAccountDto
            {
                Name = user.Name,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                NickName = user.NickName,
                Login = user.Login,
                Password = user.Password,
            };

            return result;
        }

        public async Task<Guid> UpdateAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return Guid.Empty;
            }

            var id = Guid.Parse(claimId);
            var user = await _accountRepository.FindById(id, cancellationToken);

            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'.");
            }

            user.Name = createAccountDto.Name;
            user.LastName = createAccountDto.LastName;
            user.PhoneNumber = createAccountDto.PhoneNumber;
            user.NickName = createAccountDto.NickName;
            user.Login = createAccountDto.Login;
            user.Password = createAccountDto.Password;
            

            return await _accountRepository.UpdateAccountAsync(user, cancellationToken);
        }

        
    }
}
