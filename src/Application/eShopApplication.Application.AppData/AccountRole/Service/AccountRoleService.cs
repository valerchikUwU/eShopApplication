using eShopApplication.Application.AppData.AccountRole.Repository;
using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Contracts.AccountRole;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Domain.AccountRole;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.AccountRole.Service
{
    /// <inheritdoc cref="IAccountRoleService"/>
    public class AccountRoleService : IAccountRoleService
    {

        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        /// <inheritdoc cref="IAccountRoleService.AddAccountRoleAsync(CreateAccountRoleDto, CancellationToken)"/>
        public async Task<Guid> AddAccountRoleAsync(CreateAccountRoleDto createAccountRoleDto, CancellationToken cancellationToken)
        {
            var accountRole = new Domain.AccountRole.AccountRole
            {
                AccountRoleName = createAccountRoleDto.AccountRoleName,
                AccountRoleDescription = createAccountRoleDto.AccountRoleDescription
            };
            return await _accountRoleRepository.AddAccountRoleAsync(accountRole, cancellationToken);
        }


        /// <inheritdoc cref="IAccountRoleService.DeleteAccountRoleAsync(Guid, CancellationToken)"/>
        public async Task DeleteAccountRoleAsync(Guid id, CancellationToken cancellationToken)
        {
            var accountRoles = await _accountRoleRepository.GetAllAccountRolesAsync(cancellationToken);

            var role = accountRoles.Select(s => new ReadAccountRoleDto
            {
                AccountRoleId = s.AccountRoleId,
                AccountRoleName = s.AccountRoleName,
                AccountRoleDescription = s.AccountRoleDescription
            }).Where(s => s.AccountRoleId.Equals(id));

            if(role == null)
            {
                throw new Exception($"Роль с идентификатором {id} не найдена.");
            }

            await _accountRoleRepository.DeleteAccountRoleAsync(id, cancellationToken);
        }


        /// <inheritdoc cref="IAccountRoleService.GetAllAccountRolesAsync(CancellationToken)"/>
        public async Task<List<ReadAccountRoleDto>> GetAllAccountRolesAsync(CancellationToken cancellationToken)
        {
            var accountRoles = await _accountRoleRepository.GetAllAccountRolesAsync(cancellationToken);
            var result = accountRoles.Select(s => new ReadAccountRoleDto
            {
                AccountRoleId = s.AccountRoleId,
                AccountRoleName = s.AccountRoleName,
                AccountRoleDescription = s.AccountRoleDescription
            });
            return result.ToList();
        }
    }
}
