using eShopApplication.Application.AppData.Adverts.Service;
using eShopApplication.Application.AppData.Categories.Service;
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
    [Authorize(Policy = "AdminPolicy")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }



        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReadAdvertDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запрос категорий");
            var result = await _categoryService.GetAllCategoriesAsync(cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
        {
            var result = await _categoryService.AddCategoryAsync(dto, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] ReadCategoryDto readCategoryDto, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateCategoryAsync(readCategoryDto, cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, readCategoryDto);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос на удаление объявления по идентификатору: {id}");
            await _categoryService.DeleteCategoryAsync(id, cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, id);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос категории по идентификатору: {id}");
            var result = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpGet("by_name")]
        [ProducesResponseType(typeof(IEnumerable<ReadCategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategoriesByNameAsync(string name, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос всех категорий по ключевому полю: {name}");
            var result = await _categoryService.GetCategoriesByNameAsync(name, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }
}
