using eShopApplication.Application.AppData.Adverts.Service;
using eShopApplication.Contracts.Adverts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eShopApplication.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;
        private readonly ILogger<AdvertController> _logger;

        public AdvertController(IAdvertService advertService, ILogger<AdvertController> logger)
        {
            _advertService = advertService;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReadAdvertDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос постов");
            var result = await _advertService.GetAllAsync(cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateAdvertDto dto, CancellationToken cancellationToken)
        {
            var result = await _advertService.AddAdvertAsync(dto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }
}
