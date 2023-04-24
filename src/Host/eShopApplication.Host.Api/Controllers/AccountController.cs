﻿using eShopApplication.Application.AppData.Account.Services;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace eShopApplication.Host.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с аккаунтами.
    /// </summary>
    /// <response code="500">Произошла внутренняя ошибка.</response>
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        /// <summary>
        /// Инициализирует экземпляр <see cref="AccountController"/>
        /// </summary>
        /// <param name="logger">Сервис логирования.</param>
        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Зарегистрировать новый аккаунт.
        /// </summary>
        /// <param name="dto">Модель регистрации аккаунта.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <response code="201">Аккаунт успешно зарегистрирован.</response>
        /// <response code="400">Модель данных запроса невалидна.</response>
        /// <response code="422">Произошёл конфликт бизнес-логики.</response>
        /// <returns>Модель зарегистрированного аккаунта.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ReadAccountDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterAccount([FromBody] CreateAccountDto dto, CancellationToken cancellation)
        {
            _logger.LogInformation("Регистрация нового аккаунта.");

            var result = await _accountService.RegisterAccountAsync(dto, cancellation);

            return await Task.Run(() => CreatedAtAction(nameof(Login), result), cancellation);
        }

        /// <summary>
        /// Войти в аккаунт.
        /// </summary>
        /// <param name="dto">Модель входа в аккаунт.</param>
        /// <param name="cancellation">Токен отмены.</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="400">Модель данных запроса невалидна.</response>
        /// <response code="403">Доступ запрещён (пользователь заблокирован).</response>
        /// <response code="404">Пользователь не найден.</response>
        /// <returns>Модель с данными входа.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResultDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginAccountDto dto, CancellationToken cancellation)
        {
            _logger.LogInformation("Вход в аккаунт.");

            var result = await _accountService.LoginAsync(dto, cancellation);

            return await Task.Run(() => Ok(result), cancellation);
        }

        [HttpPost("logout")]
        public async Task Logout(string token)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpPost("GetUserInfo")]
        public async Task<ReadAccountDto> GetUserInfo(CancellationToken cancellation)
        {
            var result = await _accountService.GetCurrentAsync(cancellation);

            return result;

        }

        [HttpPut("UpdateCurrentAccount")]
        public async Task<IActionResult> UpdateAccountAsync([FromBody] CreateAccountDto createAccountDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос на обновление аккаунта на: {JsonConvert.SerializeObject(createAccountDto)}");
            await _accountService.UpdateAccountAsync(createAccountDto, cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, createAccountDto);
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> PatchAccountAsync( [FromBody] JsonPatchDocument<CreateAccountDto> patch, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Неполное обновление поста");
            var account = await _accountService.GetCurrentCreatedDtoAsync(cancellationToken);

            var original = new CreateAccountDto 
            {
                Name = account.Name,
                LastName = account.LastName,
                PhoneNumber = account.PhoneNumber,
                NickName = account.NickName,
                Login = account.Login,
                Password = account.Password,
            };

            patch.ApplyTo(account, ModelState);

            var isValid = TryValidateModel(account);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await _accountService.UpdateAccountAsync(account, cancellationToken);

            var model = new
            {
                original,
                patched = account
            };

            return Ok(model);


        }
    }
}
