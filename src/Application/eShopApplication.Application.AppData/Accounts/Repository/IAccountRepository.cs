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
        /// <param name="predicate"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Domain.Account.Account> FindWhere(Expression<Func<Domain.Account.Account, bool>> predicate, CancellationToken cancellation);

        /// <summary>
        /// Поиск пользователя по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор пользователя</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Domain.Account.Account> FindById(Guid id, CancellationToken cancellation);


        /// <summary>
        /// Добавление пользователя.
        /// </summary>
        /// <param name="entity">Пользователь.</param>
        /// <returns></returns>
        Task<Guid> AddAsync(Domain.Account.Account entity, CancellationToken cancellation);
    }
}
