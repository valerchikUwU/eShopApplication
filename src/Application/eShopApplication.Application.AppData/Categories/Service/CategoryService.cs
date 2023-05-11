using eShopApplication.Application.AppData.AccountRole.Repository;
using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Application.AppData.Adverts.Service;
using eShopApplication.Application.AppData.Categories.Repository;
using eShopApplication.Contracts.AccountRole;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Contracts.Categories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Categories.Service
{
    /// <inheritdoc cref="ICategoryService"/>
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }


        ///<inheritdoc cref="ICategoryService.AddCategoryAsync(CreateCategoryDto, CancellationToken)"/>
        public async Task<Guid> AddCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
        {
            var category = new Domain.Category.Category
            {
                Name = createCategoryDto.Name,
                ParentId = createCategoryDto.ParentId
            };
            return await _categoryRepository.AddCategoryAsync(category, cancellationToken);
        }


        /// <inheritdoc cref="ICategoryService.DeleteCategoryAsync(Guid, CancellationToken)"/>
        public async Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);

            var category = categories.Select(s => new ReadCategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                ParentId = s.ParentId
            }).Where(s => s.Id.Equals(id));

            if (category == null)
            {
                throw new Exception($"Категория с идентификатором {id} не найдена.");
            }
            await _categoryRepository.DeleteCategoryAsync(id, cancellationToken);
        }


        /// <inheritdoc cref="ICategoryService.GetAllCategoriesAsync(CancellationToken)"/>
        public async Task<List<ReadCategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var adverts = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
            var result = adverts.Select(s => new ReadCategoryDto
            {
                Id= s.Id,
                Name= s.Name,
                ParentId= s.ParentId
            });

            if (result == null)
            {
                throw new Exception($"Категорий не найдено");
            }

            return result.ToList();
        }


        /// <inheritdoc cref="ICategoryService.GetCategoryByIdAsync(Guid, CancellationToken)"/>
        public async Task<ReadCategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
            var result = new ReadCategoryDto
            {
                Id = id,
                Name = category.Name,
                ParentId = category.ParentId
            };

            if (result == null)
            {
                throw new Exception($"Категория с идентификатором {id} не найдена");
            }

            return result;
        }


        /// <inheritdoc cref="ICategoryService.GetCategoriesByNameAsync(string, CancellationToken)"/>
        public async Task<List<ReadCategoryDto>> GetCategoriesByNameAsync(string name, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
            var result = categories.Select(s => new ReadCategoryDto
            {
                Id = s.Id,
                ParentId= s.ParentId,
                Name = s.Name,
            }).Where(s => s.Name.Contains(name));

            if (result == null)
            {
                throw new Exception($"Категорий по ключевому полю {name} не найдено");
            }

            return result.ToList();
        }


        /// <inheritdoc cref="ICategoryService.UpdateCategoryAsync(ReadCategoryDto, CancellationToken)"/>
        public async Task<Guid> UpdateCategoryAsync(ReadCategoryDto readCategoryDto, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(readCategoryDto.Id, cancellationToken);
            if (existingCategory == null)
            {
                throw new Exception($"Объявление с идентификатором {readCategoryDto.Id} не найдено");
            }

            existingCategory.Name = readCategoryDto.Name;
            existingCategory.ParentId = readCategoryDto.ParentId;

            return await _categoryRepository.UpdateCategoryAsync(existingCategory, cancellationToken);
        }
    }
}
