namespace eShopApplication.Application.AppData.Accounts.Repository
{
    /// <summary>
    /// Репозиторий для работы с аккаунтами.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Добавить аккаунт
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор пользователя</returns>
        Task<Guid> AddAccountAsync(Domain.Account.Account account, CancellationToken cancellationToken);

        /// <summary>
        /// Получить пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Пользователь</returns>
        Task<Domain.Account.Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список пользователей по ключевому слову
        /// </summary>
        /// <param name="name">Ключевое слово</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список пользователей</returns>
        Task<List<Domain.Account.Account>> GetAccountsByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список всех пользователей</returns>
        Task<List<Domain.Account.Account>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="account">Пользователь</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор удаленного пользователя</returns>
        Task<Guid> DeleteAccount(Domain.Account.Account account, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить информацию о пользователе
        /// </summary>
        /// <param name="account">Пользователь</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор пользователя</returns>
        Task<Guid> UpdateAccount(Domain.Account.Account account, CancellationToken cancellationToken);
    }
}
