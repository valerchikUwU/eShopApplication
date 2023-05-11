using eShopApplication.Application.AppData.AccountRole.Service;
using eShopApplication.Application.AppData.Categories.Service;
using eShopApplication.Contracts;
using eShopApplication.Contracts.AccountRole;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Contracts.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eShopApplication.Host.Api.Controllers
{
    /// <summary>
    /// Контроллер ролей аккаунтов
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "AdminPolicy")]
    [Produces("application/json")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleService _accountRoleService;
        private readonly ILogger<AccountRoleController> _logger;

        public AccountRoleController(IAccountRoleService accountRoleService, ILogger<AccountRoleController> logger)
        {
            _accountRoleService = accountRoleService;
            _logger = logger;
        }


        /// <summary>
        /// Получить все роли
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список ролей</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReadAccountRoleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _accountRoleService.GetAllAccountRolesAsync(cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }


        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="dto">Модель создания роли</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор созданной модели</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateAccountRoleDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateAccountRoleDto dto, CancellationToken cancellationToken)
        {
            var result = await _accountRoleService.AddAccountRoleAsync(dto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос на удаление роли по идентификатору: {id}");
            await _accountRoleService.DeleteAccountRoleAsync(id, cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, id);
        }

    }
}
