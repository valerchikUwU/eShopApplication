using AutoMapper;
using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Application.AppData.File.Repository;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Contracts.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.File.Service
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="FileService"/>.
        /// </summary>        
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public Task DeleteFileAsync(Guid id, CancellationToken cancellationToken)
        {
            return _fileRepository.DeleteFileAsync(id, cancellationToken);
        }

        public async Task<CreateFileDto> DownloadFileAsync(Guid id, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileByIdAsync(id, cancellationToken);
            var result = new CreateFileDto
            {
                Name = file.Name,
                Content = file.Content,
                ContentType = file.ContentType
            };

            return result;
        }

        public async Task<ReadFileDto> GetFileByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetFileByIdAsync(id, cancellationToken);
            var result = new ReadFileDto
            {
                Id= file.Id,
                Name= file.Name,
                Created = file.Created,
                Length= file.Length,
            };
            return result;
        }

        public async Task<Guid> UploadFileAsync(CreateFileDto createFileDto, CancellationToken cancellationToken)
        {
            var file = new Domain.File.File
            {
                Name= createFileDto.Name,
                Content= createFileDto.Content,
                ContentType = createFileDto.ContentType,
                Length= createFileDto.Length,
                Created = DateTime.UtcNow,
            };

            await _fileRepository.UploadFileAsync(file, cancellationToken);
            return file.Id;
        }
    }
}
