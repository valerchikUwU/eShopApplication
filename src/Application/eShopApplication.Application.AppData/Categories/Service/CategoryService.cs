using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Application.AppData.Categories.Repository;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Contracts.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Categories.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Guid> AddCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
        {
            var category = new Domain.Category.Category
            {
                Name = createCategoryDto.Name,
                ParentId = createCategoryDto.ParentId
            };
            return await _categoryRepository.AddCategoryAsync(category, cancellationToken);
        }

        public async Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteCategoryAsync(id, cancellationToken);
        }

        public async Task<List<ReadCategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var adverts = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
            var result = adverts.Select(s => new ReadCategoryDto
            {
                Id= s.Id,
                Name= s.Name,
                ParentId= s.ParentId
            });
            return result.ToList();
        }

        public async Task<ReadCategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
            var result = new ReadCategoryDto
            {
                Id = id,
                Name = category.Name,
                ParentId = category.ParentId
            };
            return result;
        }

        public async Task<List<ReadCategoryDto>> GetCategoriesByNameAsync(string name, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);
            var result = categories.Select(s => new ReadCategoryDto
            {
                Id = s.Id,
                ParentId= s.ParentId,
                Name = s.Name,
            }).Where(s => s.Name.Contains(name));
            return result.ToList();
        }

        public async Task<Guid> UpdateCategoryAsync(ReadCategoryDto readCategoryDto, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(readCategoryDto.Id, cancellationToken);
            existingCategory.Name = readCategoryDto.Name;
            existingCategory.ParentId = readCategoryDto.ParentId;

            return await _categoryRepository.UpdateCategoryAsync(existingCategory, cancellationToken);
        }
    }
}
