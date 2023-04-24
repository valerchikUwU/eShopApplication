using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Domain.Advert;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Adverts.Service
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdvertService(IAdvertRepository advertRepository, IHttpContextAccessor httpContextAccessor)
        {
            _advertRepository = advertRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> AddAdvertAsync(CreateAdvertDto createAdvertDto, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var advert = new Domain.Advert.Advert
            {
                AccountId = Guid.Parse(claimId),
                Name = createAdvertDto.Name,
                Description = createAdvertDto.Description,
                CategoryId = createAdvertDto.CategoryId,
                Cost = createAdvertDto.Cost,
                Location = createAdvertDto.Location,
                Quantity= createAdvertDto.Quantity,
                FileIds = createAdvertDto.FileIds
            };
            return await _advertRepository.AddAdvertAsync(advert, cancellationToken);
        }

        public async Task DeleteAdvertAsync(Guid id, CancellationToken cancellationToken)
        {
            await _advertRepository.DeleteAdvertAsync(id, cancellationToken);
        }

        public async Task<ReadAdvertDto> GetAdvertByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.GetAdvertByIdAsync(id, cancellationToken);
            var result = new ReadAdvertDto
            {
                Id = advert.Id,
                Name = advert.Name,
                Description = advert.Description,
                IsActive = advert.IsActive,
                CreatedAt = advert.CreatedAt,
                CategoryId = advert.CategoryId,
                Cost = advert.Cost,
                Location = advert.Location,
                Quantity = advert.Quantity,
                AccountId = advert.AccountId,
                FileIds = advert.FileIds
            };
            return result;
        }

        public async Task<UpdateAdvertDto> GetUpdateAdvertByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var advert = await _advertRepository.GetAdvertByIdAsync(id, cancellationToken);
            var result = new UpdateAdvertDto
            {
                Name = advert.Name,
                Description = advert.Description,
                IsActive = advert.IsActive,
                CategoryId = advert.CategoryId,
                Cost = advert.Cost,
                Location = advert.Location,
                Quantity = advert.Quantity,
                FileIds = advert.FileIds
            };
            return result;
        }

        public async Task<List<ReadAdvertDto>> GetAdvertsByNameAsync(string name, CancellationToken cancellationToken)
        {
            var adverts = await _advertRepository.GetAllAdvertsAsync(cancellationToken);
            var result = adverts.Select(s => new ReadAdvertDto
            {
                Id = s.Id,
                Description = s.Description,
                Name = s.Name,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                CategoryId = s.CategoryId,
                Cost = s.Cost,
                Location = s.Location,
                Quantity = s.Quantity,
                AccountId = s.AccountId,
                FileIds = s.FileIds
            }).Where(s => s.Name.Contains(name));
            return result.ToList();
        }

        public async Task<List<ReadAdvertDto>> GetAllAdvertsAsync(CancellationToken cancellationToken)
        {
            var adverts = await _advertRepository.GetAllAdvertsAsync(cancellationToken);
            var result = adverts.Select(s => new ReadAdvertDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                CategoryId = s.CategoryId,
                Cost = s.Cost,
                Location = s.Location,
                Quantity = s.Quantity,
                AccountId = s.AccountId,
                FileIds = s.FileIds
            });
            return result.ToList();
        }


        public async Task<Guid> UpdateAdvertAsync(Guid id, UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken)
        {
            var existingAdvert = await _advertRepository.GetAdvertByIdAsync(id, cancellationToken);
            existingAdvert.Name= updateAdvertDto.Name;
            existingAdvert.Description = updateAdvertDto.Description;
            existingAdvert.CategoryId = updateAdvertDto.CategoryId;
            existingAdvert.Cost = updateAdvertDto.Cost;
            existingAdvert.Location = updateAdvertDto.Location;
            existingAdvert.Quantity = updateAdvertDto.Quantity;
            existingAdvert.IsActive = updateAdvertDto.IsActive;
            existingAdvert.FileIds = updateAdvertDto.FileIds;

            return await _advertRepository.UpdateAdvertAsync(existingAdvert, cancellationToken);
        }
    }
}
