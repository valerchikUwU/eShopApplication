using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Domain.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Adverts.Service
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;

        public AdvertService(IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;
        }

        public async Task<Guid> AddAdvertAsync(CreateAdvertDto createAdvertDto, CancellationToken cancellationToken)
        {
            var advert = new Domain.Advert.Advert
            {

                Name = createAdvertDto.Name,
                Description = createAdvertDto.Description
            };
            return await _advertRepository.AddAdvertAsync(advert, cancellationToken);
        }

        public async Task<ReadAdvertDto> GetAdvertByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.GetAdvertByIdAsync(id, cancellationToken);
            var result = new ReadAdvertDto
            {
                Id = advert.Id,
                Description = advert.Description,
                Name = advert.Name,
                IsActive = advert.IsActive,
                CreatedAt = advert.CreatedAt
            };
            return result;
        }

        public async Task<List<ReadAdvertDto>> GetAdvertsByNameAsync(string name, CancellationToken cancellationToken)
        {
            var adverts = await _advertRepository.GetAllAsync(cancellationToken);
            var result = adverts.Select(s => new ReadAdvertDto
            {
                Id = s.Id,
                Description = s.Description,
                Name = s.Name,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt
            }).Where(s => s.Name.Contains(name));
            return result.ToList();
        }

        public async Task<List<ReadAdvertDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var adverts = await _advertRepository.GetAllAsync(cancellationToken);
            var result = adverts.Select(s => new ReadAdvertDto
            {
                Id = s.Id,
                Description = s.Description,
                Name = s.Name,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt
            });
            return result.ToList();
        }
    }
}
