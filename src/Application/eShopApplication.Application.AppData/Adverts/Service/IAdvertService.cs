using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Adverts.Service
{
    public interface IAdvertService
    {
        Task<Guid> AddAdvertAsync(CreateAdvertDto advert, CancellationToken cancellationToken);
        Task<ReadAdvertDto> GetAdvertByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<ReadAdvertDto>> GetAdvertsByNameAsync(string name, CancellationToken cancellationToken);
        Task<List<ReadAdvertDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
