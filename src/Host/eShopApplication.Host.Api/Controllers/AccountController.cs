using eShopApplication.Application.AppData.Account.Services;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eShopApplication.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReadAccountDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос аккаунтов");
            var result = await _accountService.GetAll(cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateAccountDto dto, CancellationToken cancellationToken)
        {
            var result = await _accountService.CreateAccountAsync(dto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }
}
