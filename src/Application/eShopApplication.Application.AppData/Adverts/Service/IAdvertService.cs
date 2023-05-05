using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Adverts.Service
{

    /// <summary>
    /// Сервис для работы с объявлениями.
    /// </summary>
    public interface IAdvertService
    {

        /// <summary>
        /// Добавить объявления
        /// </summary>
        /// <param name="advert">Модель создания объявления</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Идентификатор добавленного объявления</returns>
        Task<Guid> AddAdvertAsync(CreateAdvertDto advert, CancellationToken cancellationToken);

        /// <summary>
        /// Получить объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель объявления.</returns>
        Task<ReadAdvertDto> GetAdvertByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список объявления по ключевому слову
        /// </summary>
        /// <param name="name">Ключевое слово</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список объявления по ключевому слову</returns>
        Task<List<ReadAdvertDto>> GetAdvertsByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список объявлений.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список объявлений.</returns>
        Task<List<ReadAdvertDto>> GetAllAdvertsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Обновить объявления
        /// </summary>
        /// <param name="updateAdvertDto">Модель обновления объявления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор обновленного объявления</returns>
        Task<Guid> UpdateAdvertAsync(Guid id, UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель объявления по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объявления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Модель обновления объявления</returns>
        Task<UpdateAdvertDto> GetUpdateAdvertDtoByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="id">Идентификатор объявления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAdvertAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить все объявления авторизированного пользователя
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список объявлений авторизованного пользователя</returns>
        Task<List<ReadAdvertDto>> GetAllAdvertsOfCurrentUserAsync(CancellationToken cancellationToken);
    }
}
