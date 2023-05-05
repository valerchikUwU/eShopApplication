using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.AccountRole.Repository
{

    /// <summary>
    /// Репозиторий для работы с ролями
    /// </summary>
    public interface IAccountRoleRepository
    {
        /// <summary>
        /// Добавить роль
        /// </summary>
        /// <param name="accountRole">Модель роли</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор добавленной роли</returns>
        Task<Guid> AddAccountRoleAsync(Domain.AccountRole.AccountRole accountRole, CancellationToken cancellationToken);

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
        Task<List<Domain.AccountRole.AccountRole>> GetAllAccountRolesAsync(CancellationToken cancellationToken);
    }
}
