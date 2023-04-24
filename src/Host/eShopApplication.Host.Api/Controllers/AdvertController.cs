using eShopApplication.Application.AppData.Adverts.Service;
using eShopApplication.Contracts.Adverts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace eShopApplication.Host.Api.Controllers
{

    /// <summary>
    /// Контроллер для работы с объявлениями
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;
        private readonly ILogger<AdvertController> _logger;


        /// <summary>
        /// Инициализирует экземпляр <see cref="AdvertController"/>
        /// </summary>
        /// <param name="logger">Сервис логирования.</param>
        /// <param name="advertService">Сервис для работы с объявлениями.</param>
        public AdvertController(IAdvertService advertService, ILogger<AdvertController> logger)
        {
            _advertService = advertService;
            _logger = logger;
        }


        /// <summary>
        /// Получить список объявлений.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <returns>Список моделей объявлений.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReadAdvertDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос всех постов");
            var result = await _advertService.GetAllAdvertsAsync(cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Создать объвление
        /// </summary>
        /// <param name="createAdvertDto">Модель создания объявления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор созданного объвления</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateAdvertDto createAdvertDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос на создание объявления: {JsonConvert.SerializeObject(createAdvertDto)}");
            var result = await _advertService.AddAdvertAsync(createAdvertDto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }


        /// <summary>
        /// Обновление объявления
        /// </summary>
        /// <param name="updateAdvertDto">Модель обновления объявления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор обновленного объявления</returns>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdvertDto updateAdvertDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос на обновление объявления на: {JsonConvert.SerializeObject(updateAdvertDto)}");
            await _advertService.UpdateAdvertAsync(id, updateAdvertDto, cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, updateAdvertDto);
        }


        /// <summary>
        /// Удалить объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {

            _logger.LogInformation($"Запрос на удаление объявления по идентификатору: {id}");
            await _advertService.DeleteAdvertAsync(id, cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, id);
        }


        /// <summary>
        /// Получить объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель объявления</returns>
        [HttpGet("{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос объявления по идентификатору: {id}");
            var result = await _advertService.GetAdvertByIdAsync(id, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        /// <summary>
        /// Получить список объвления по ключевому слову
        /// </summary>
        /// <param name="name">Ключевое слово</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список объвлений</returns>
        [HttpGet("by_name")]
        [ProducesResponseType(typeof(IEnumerable<ReadAdvertDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAdvertsByNameAsync(string name, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос всех постов по ключевому полю: {name}");
            var result = await _advertService.GetAdvertsByNameAsync(name, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpPatch("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> PatchAdvertAsync(Guid id, [FromBody] JsonPatchDocument<UpdateAdvertDto> patch, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Неполное обновление поста");
            var advert = await _advertService.GetUpdateAdvertByIdAsync(id, cancellationToken);

            var original = advert;

            patch.ApplyTo(advert, ModelState);

            var isValid = TryValidateModel(advert);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await _advertService.UpdateAdvertAsync(id, advert, cancellationToken);

            var model = new
            {
                original,
                patched = advert
            };

            return Ok(model);


        }

    }
}
