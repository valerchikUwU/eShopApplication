using eShopApplication.Contracts.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Application.AppData.File.Service
{
    public interface IFileService
    {
        /// <summary>
        /// Получение информации о файле по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о файле.</returns>
        Task<ReadFileDto> GetFileByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="readFileDto">Модель файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadFileAsync(CreateFileDto createFileDto, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о скачиваемом файле.</returns>
        Task<CreateFileDto> DownloadFileAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление файла по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>        
        Task DeleteFileAsync(Guid id, CancellationToken cancellationToken);
    }
}
