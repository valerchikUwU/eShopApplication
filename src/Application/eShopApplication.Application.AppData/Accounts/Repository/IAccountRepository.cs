using System.Linq.Expressions;

namespace eShopApplication.Application.AppData.Accounts.Repository
{
    /// <summary>
    /// Репозиторий для работы с аккаунтами.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Поиск пользователя по фильтру.
        /// </summary>
        /// <param name="predicate">Ключевое слово</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Модель аккаунта</returns>
        Task<Domain.Account.Account> FindWhere(Expression<Func<Domain.Account.Account, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Поиск пользователя по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Модель аккаунта</returns>
        Task<Domain.Account.Account> FindById(Guid id, CancellationToken cancellationToken);


        /// <summary>
        /// Добавить аккаунт
        /// </summary>
        /// <param name="account">Модель аккаунта</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор добавленного аккаунта</returns>
        Task<Guid> AddAsync(Domain.Account.Account account, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить аккаунт
        /// </summary>
        /// <param name="account">Модель аккаунта</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор обновленного аккаунта</returns>
        Task<Guid> UpdateAccountAsync(Domain.Account.Account account, CancellationToken cancellationToken);
    }
}
