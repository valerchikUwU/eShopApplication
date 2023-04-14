using eShopApplication.Contracts.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Categories.Service
{

    /// <summary>
    /// Сервис для работы с категориями.
    /// </summary>
    public interface ICategoryService
    {

        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="createCategoryDto">Модель создания категории</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор категории</returns>
        Task<Guid> AddCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);

        /// <summary>
        /// Получить категорию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Модель категории</returns>
        Task<ReadCategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список категорий по ключевому слову
        /// </summary>
        /// <param name="name">Ключевое слово</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список категорий</returns>
        Task<List<ReadCategoryDto>> GetCategoriesByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список категорий</returns>
        Task<List<ReadCategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить категорию
        /// </summary>
        /// <param name="readCategoryDto">Модель категории</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор категории</returns>
        Task<Guid> UpdateCategoryAsync(ReadCategoryDto readCategoryDto, CancellationToken cancellationToken);
    }
}
