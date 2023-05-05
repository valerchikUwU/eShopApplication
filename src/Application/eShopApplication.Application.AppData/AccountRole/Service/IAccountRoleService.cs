using eShopApplication.Contracts.AccountRole;
using eShopApplication.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.AccountRole.Service
{

    /// <summary>
    /// Сервис для работы с ролями
    /// </summary>
    public interface IAccountRoleService
    {
        /// <summary>
        /// Добавить роль
        /// </summary>
        /// <param name="createAccountRoleDto">Модель создания роли</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор добавленной роли</returns>
        Task<Guid> AddAccountRoleAsync(CreateAccountRoleDto createAccountRoleDto, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить роль
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAccountRoleAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список ролей
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список ролей</returns>
        Task<List<ReadAccountRoleDto>> GetAllAccountRolesAsync(CancellationToken cancellationToken);
    }
}
