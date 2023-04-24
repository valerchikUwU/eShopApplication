using eShopApplication.Application.AppData.File.Repository;
using eShopApplication.Contracts.Files;
using eShopApplication.Infrastructure.Repositories;
using eShopApplication.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.File.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly IRepository<Domain.File.File> _fileRepository;

        public FileRepository(IRepository<Domain.File.File> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task DeleteFileAsync(Guid id, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetByIdAsync(id , cancellationToken);
            if (file != null)
            {
                return;
            }

            await _fileRepository.DeleteAsync(file, cancellationToken);
        }

        public async Task<Domain.File.File> DownloadFileAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _fileRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Domain.File.File> GetFileByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _fileRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Guid> UploadFileAsync(Domain.File.File file, CancellationToken cancellationToken)
        {
            await _fileRepository.AddAsync(file, cancellationToken);
            return file.Id;
        }
    }
}
