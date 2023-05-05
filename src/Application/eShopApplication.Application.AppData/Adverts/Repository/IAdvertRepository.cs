using eShopApplication.Contracts.Adverts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Adverts.Repository
{
    /// <summary>
    /// Репозиторий для работы с объявлениями.
    /// </summary>
    public interface IAdvertRepository
    {

        /// <summary>
        /// Добавить объявление
        /// </summary>
        /// <param name="advert">Объявление</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор добавленного поста</returns>
        Task<Guid> AddAdvertAsync(Domain.Advert.Advert advert, CancellationToken cancellationToken);

        /// <summary>
        /// Получить объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Модель объявления.</returns>
        Task<Domain.Advert.Advert> GetAdvertByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список объявлений по ключевому слову
        /// </summary>
        /// <param name="name">Ключевое слово</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список объявлений по ключевому слову</returns>
        Task<List<Domain.Advert.Advert>> GetAdvertsByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список объявлений.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список объявлений.</returns>
        Task<List<Domain.Advert.Advert>> GetAllAdvertsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удалить объявление
        /// </summary>
        /// <param name="id">Идентификатор объявления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAdvertAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить объявление
        /// </summary>
        /// <param name="advert">Модель объявления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор обновленного объявления</returns>
        Task<Guid> UpdateAdvertAsync(Domain.Advert.Advert advert, CancellationToken cancellationToken);
    }
}
