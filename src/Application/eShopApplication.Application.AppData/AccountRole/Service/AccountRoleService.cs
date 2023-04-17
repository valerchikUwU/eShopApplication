using eShopApplication.Application.AppData.AccountRole.Repository;
using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Contracts.AccountRole;
using eShopApplication.Contracts.Adverts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.AccountRole.Service
{
    public class AccountRoleService : IAccountRoleService
    {

        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }
        public async Task<Guid> AddAccountRoleAsync(CreateAccountRoleDto createAccountRoleDto, CancellationToken cancellationToken)
        {
            var accountRole = new Domain.AccountRole.AccountRole
            {

                AccountRoleName = createAccountRoleDto.AccountRoleName,
                AccountRoleDescription = createAccountRoleDto.AccountRoleDescription
            };
            return await _accountRoleRepository.AddAccountRoleAsync(accountRole, cancellationToken);
        }

        public async Task DeleteAccountRoleAsync(Guid id, CancellationToken cancellationToken)
        {
            await _accountRoleRepository.DeleteAccountRoleAsync(id, cancellationToken);
        }

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
