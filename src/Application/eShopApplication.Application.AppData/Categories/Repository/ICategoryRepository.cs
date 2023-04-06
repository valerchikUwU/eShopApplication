using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Categories.Repository
{

    /// <summary>
    /// Репозиторий для работы с категориями.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="category">Модель создания категории</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор категории</returns>
        Task<Guid> AddCategoryAsync(Domain.Category.Category category, CancellationToken cancellationToken);

        /// <summary>
        /// Получить категорию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Модель категории</returns>
        Task<Domain.Category.Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список категори по ключевому слову
        /// </summary>
        /// <param name="name">Ключевое слово</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список категорий</returns>
        Task<List<Domain.Category.Category>> GetCategoriesByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список категорий</returns>
        Task<List<Domain.Category.Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить категрию
        /// </summary>
        /// <param name="category">Модель категории</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор категории</returns>
        Task<Guid> UpdateCategoryAsync(Domain.Category.Category category, CancellationToken cancellationToken);
    }
}
