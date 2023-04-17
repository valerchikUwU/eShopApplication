using eShopApplication.Application.AppData.AccountRole.Service;
using eShopApplication.Application.AppData.Categories.Service;
using eShopApplication.Contracts.AccountRole;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Contracts.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eShopApplication.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleService _accountRoleService;
        private readonly ILogger<CategoryController> _logger;

        public AccountRoleController(IAccountRoleService accountRoleService, ILogger<CategoryController> logger)
        {
            _accountRoleService = accountRoleService;
            _logger = logger;
        }



        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReadAdvertDto>), StatusCodes.Status200OK)]
        [Authorize(Policy ="AdminPolicy")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос ролей");
            var result = await _accountRoleService.GetAllAccountRolesAsync(cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [Authorize(Policy ="AdminPolicy")]
        public async Task<IActionResult> Create([FromBody] CreateAccountRoleDto dto, CancellationToken cancellationToken)
        {
            var result = await _accountRoleService.AddAccountRoleAsync(dto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }


        [HttpDelete]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос на удаление роли по идентификатору: {id}");
            await _accountRoleService.DeleteAccountRoleAsync(id, cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, id);
        }

    }
}
