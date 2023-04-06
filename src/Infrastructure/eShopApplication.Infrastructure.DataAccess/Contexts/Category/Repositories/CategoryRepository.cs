using eShopApplication.Application.AppData.Categories.Repository;
using eShopApplication.Domain.Advert;
using eShopApplication.Infrastructure.Repositories;
using eShopApplication.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Category.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Domain.Category.Category> _categoryRepository;

        public CategoryRepository(IRepository<Domain.Category.Category> repository)
        {
            _categoryRepository = repository;
        }
        public async Task<Guid> AddCategoryAsync(Domain.Category.Category category, CancellationToken cancellationToken)
        {
            await _categoryRepository.AddAsync(category, cancellationToken);
            return category.Id;

        }

        public async Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);

            if (existingCategory == null)
            {
                return;
            }

            await _categoryRepository.DeleteAsync(existingCategory, cancellationToken);
        }

        public async Task<List<Domain.Category.Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAll().ToListAsync(cancellationToken);
        }

        public async Task<Domain.Category.Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<List<Domain.Category.Category>> GetCategoriesByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAll().Where(s => s.Name.Contains(name)).ToListAsync(cancellationToken);
        }

        public async Task<Guid> UpdateCategoryAsync(Domain.Category.Category category, CancellationToken cancellationToken)
        {
            await _categoryRepository.UpdateAsync(category, cancellationToken);
            return category.Id;
        }
    }
}
