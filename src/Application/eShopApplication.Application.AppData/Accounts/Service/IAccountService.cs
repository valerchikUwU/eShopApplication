using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Account.Services
{
    /// <summary>
    /// TODO: Подлежит доработке
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="createAccountDto">Модель для регистрации.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Идентификатор пользователя.</returns>
        Task<Guid> RegisterAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellation);

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="loginAccountDto">Модель для логина.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Токен.</returns>
        Task<string> LoginAsync(LoginAccountDto loginAccountDto, CancellationToken cancellation);

        /// <summary>
        /// Получение текущего пользователя.
        /// </summary>
        /// <param name="cancellation">Токен отмены.</param>
        /// <returns>Текущий пользователь.</returns>
        Task<ReadAccountDto> GetCurrentAsync(CancellationToken cancellation);

        Task<Guid> UpdateAccountAsync(CreateAccountDto createAccountDto, CancellationToken cancellationToken);

    }
}
