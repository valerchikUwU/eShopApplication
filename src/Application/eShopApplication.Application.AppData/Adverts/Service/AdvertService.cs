using eShopApplication.Application.AppData.Account.Services;
using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Domain.Advert;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eShopApplication.Application.AppData.Adverts.Service
{

    /// <inheritdoc cref="IAdvertService"/>
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdvertService(IAdvertRepository advertRepository, IHttpContextAccessor httpContextAccessor)
        {
            _advertRepository = advertRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <inheritdoc cref="IAdvertService.AddAdvertAsync(CreateAdvertDto, CancellationToken)"/>
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


        /// <inheritdoc cref="IAdvertService.DeleteAdvertAsync(Guid, CancellationToken)"/>
        public async Task DeleteAdvertAsync(Guid id, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var advert = await GetAdvertByIdAsync(id, cancellationToken);
            if (advert.AccountId != Guid.Parse(claimId))
            {
                throw new Exception($"Ваш идентификатор: '{advert.AccountId}' не совпадает с идентификатором автора: '{id}'.");
            }
            await _advertRepository.DeleteAdvertAsync(id, cancellationToken);
        }

        /// <inheritdoc cref="IAdvertService.GetAdvertByIdAsync(Guid, CancellationToken)"/>
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

            if(result == null)
            {
                throw new Exception($"Объявление с идентификатором {id} не найдена");
            }

            return result;
        }

        /// <inheritdoc cref="IAdvertService.GetUpdateAdvertDtoByIdAsync(Guid, CancellationToken)"/>
        public async Task<UpdateAdvertDto> GetUpdateAdvertDtoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var advert = await GetAdvertByIdAsync(id, cancellationToken);
            if (advert == null)
            {
                throw new Exception($"Объявление с идентификатором {id} не найдена");
            }
            if (advert.AccountId != Guid.Parse(claimId))
            {
                throw new Exception($"Ваш идентификатор: '{advert.AccountId}' не совпадает с идентификатором автора: '{id}'.");
            }

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

        /// <inheritdoc cref="IAdvertService.GetAdvertsByNameAsync(string, CancellationToken)"/>
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
            }).Where(s => s.Name.Contains(name)).Where(s => s.IsActive == true);

            if (result == null)
            {
                throw new Exception($"Объявлений по ключевому полю {name} не найдены");
            }

            return result.ToList();
        }

        /// <inheritdoc cref="IAdvertService.GetAllAdvertsAsync(CancellationToken)"/>
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
            }).Where(s => s.IsActive == true);

            if (result == null)
            {
                throw new Exception("Объявлений не найдены");
            }

            return result.ToList();
        }

        /// <inheritdoc cref="IAdvertService.UpdateAdvertAsync(Guid, UpdateAdvertDto, CancellationToken)"/>
        public async Task<Guid> UpdateAdvertAsync(Guid id, UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var existingAdvert = await _advertRepository.GetAdvertByIdAsync(id, cancellationToken);

            if (existingAdvert == null)
            {
                throw new Exception($"Объявление с идентификатором {id} не найдена");
            }

            if (existingAdvert.AccountId != Guid.Parse(claimId)) 
            {
                throw new Exception($"Ваш идентификатор: '{existingAdvert.AccountId}' не совпадает с идентификатором автора: '{id}'.");
            }

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


        /// <inheritdoc cref="IAdvertService.GetAllAdvertsOfCurrentUserAsync(CancellationToken)"/>
        public async Task<List<ReadAdvertDto>> GetAllAdvertsOfCurrentUserAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var id = Guid.Parse(claimId);
            Console.WriteLine(claimId);
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
            }).Where(s => s.AccountId.Equals(id));

            if (result == null)
            {
                throw new Exception("У вас нет объявлений");
            }

            return result.ToList();
        }
    }
}
