using eShopApplication.Application.AppData.File.Service;
using eShopApplication.Contracts;
using eShopApplication.Contracts.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eShopApplication.Host.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с файлами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        /// <summary>
        /// Инициализирует экземпляр контроллера
        /// </summary>
        /// <param name="fileService">Сервис для работы с файлами</param>
        /// <param name="logger">Сервис логгирования</param>
        public FileController(IFileService fileService, ILogger<FileController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        /// <summary>
        /// Получение информации о файле по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <response code="200">Запрос выполнен успешно.</response>
        /// <response code="404">Файл с указанным идентификатором не найден.</response>
        /// <returns>Информация о файле.</returns>
        [HttpGet("{id}/info")]
        [ProducesResponseType(typeof(ReadFileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetInfoById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _fileService.GetFileByIdAsync(id, cancellationToken);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="file">Файл.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <response code="201">Файл успешно загружен.</response>
        /// <response code="400">Модель данных запроса невалидна.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
        [DisableRequestSizeLimit]
        [Authorize]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            var bytes = await GetBytesAsync(file, cancellationToken);
            var createFileDto = new CreateFileDto
            {
                Content = bytes,
                ContentType = file.ContentType,
                Name = file.FileName,
                Length= bytes.Length,
            };
            var result = await _fileService.UploadFileAsync(createFileDto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Скачивание файла по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <response code="200">Запрос выполнен успешно.</response>
        /// <response code="404">Файл с указанным идентификатором не найден.</response>
        /// <returns>Файл в виде потока.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {
            var result = await _fileService.DownloadFileAsync(id, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            Response.ContentLength = result.Content.Length;
            return File(result.Content, result.ContentType, result.Name, true);
        }


        /// <summary>
        /// Удаление файла по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <response code="403">Доступ запрещён.</response>
        /// <response code="404">Файл с указанным идентификатором не найден.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _fileService.DeleteFileAsync(id, cancellationToken);
            return NoContent();
        }

        private static async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            return ms.ToArray();
        }
    }
}
