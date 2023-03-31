using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.Adverts.Repository
{
    public interface IAdvertRepository
    {
        Task<Guid> AddAdvertAsync(Domain.Advert.Advert advert, CancellationToken cancellationToken);
        Task<Domain.Advert.Advert> GetAdvertByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Domain.Advert.Advert>> GetAdvertsByNameAsync(string name, CancellationToken cancellationToken);
        Task<List<Domain.Advert.Advert>> GetAllAsync(CancellationToken cancellationToken);
        Task<Guid> DeleteAdvert(Guid id, CancellationToken cancellationToken);
        Task<Domain.Advert.Advert> UpdateAdvert(Guid id, CancellationToken cancellationToken);
    }
}
