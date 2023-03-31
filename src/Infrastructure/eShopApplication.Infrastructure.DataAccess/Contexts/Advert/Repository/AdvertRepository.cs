using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Advert.Repository
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly IRepository<Domain.Advert.Advert> _repository;

        public AdvertRepository(IRepository<Domain.Advert.Advert> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddAdvertAsync(Domain.Advert.Advert advert, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(advert, cancellationToken);
            return advert.Id;
        }

        public async Task<Domain.Advert.Advert> GetAdvertByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<List<Domain.Advert.Advert>> GetAdvertsByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(s => s.Name.Contains(name)).ToListAsync(cancellationToken);
        }

        public async Task<List<Domain.Advert.Advert>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAll().ToListAsync(cancellationToken);
        }

        public Task<Guid> DeleteAdvert(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Advert.Advert> UpdateAdvert(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
