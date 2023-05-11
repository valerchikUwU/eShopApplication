using eShopApplication.Application.AppData.Account.Services;
using eShopApplication.Contracts.Accounts;
using eShopApplication.Contracts.Adverts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eShopApplication.Application.AppData.EmailService;
using Microsoft.AspNetCore.Identity;
using Azure;
using eShopApplication.Contracts;

namespace eShopApplication.Host.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с аккаунтами.
    /// </summary>
    /// <response code="500">Произошла внутренняя ошибка.</response>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _сonfiguration;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Инициализиурет экземпляр контроллера
        /// </summary>
        /// <param name="logger">Сервис логгирования</param>
        /// <param name="accountService">Сервис аккаунтов</param>
        /// <param name="configuration">Сервис для работы с конфигурационными свойствами</param>
        /// <param name="emailService">Сервис отправки электронных писем</param>
        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IConfiguration configuration, IEmailService emailService)
        {
            _logger = logger;
            _accountService = accountService;
            _сonfiguration = configuration;
            _emailService = emailService;
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
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAccount([FromBody] CreateAccountDto dto, CancellationToken cancellation)
        {
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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginAccountDto dto, CancellationToken cancellation)
        {
            var result = await _accountService.LoginAsync(dto, cancellation);
            return await Task.Run(() => Ok(result), cancellation);
        }

        /// <summary>
        /// Получить информацию об авторизированном пользователе
        /// </summary>
        /// <param name="cancellation">Токен отмены</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="400">Модель данных запроса невалидна.</response>
        /// <response code="404">Пользователь не найден.</response>
        /// <returns>Модель чтения аккаунта></returns>
        [HttpPost("GetUserInfo")]
        [ProducesResponseType(typeof(ReadAccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ReadAccountDto> GetUserInfo(CancellationToken cancellation)
        {
            var result = await _accountService.GetCurrentAsync(cancellation);
            return result;
        }

        /// <summary>
        /// Обновить аккаунт
        /// </summary>
        /// <param name="createAccountDto">Модель создания аккаунта</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор обновленной модели</returns>
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        [HttpPut("UpdateCurrentAccount")]
        [Authorize]
        public async Task<IActionResult> UpdateAccountAsync([FromBody] CreateAccountDto createAccountDto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Запрос на обновление аккаунта на: {JsonConvert.SerializeObject(createAccountDto)}");
            await _accountService.UpdateAccountAsync(createAccountDto, cancellationToken);

            return StatusCode((int)HttpStatusCode.OK, createAccountDto);
        }


        /// <summary>
        /// Частично обновить аккаунт
        /// </summary>
        /// <param name="patch">Изменяемые поля</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Измененную модель</returns>
        [HttpPatch("PatchCurrentAccount")]
        [ProducesResponseType(typeof(CreateAccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized, "Вы не авторизованы")]
        [Authorize]
        public async Task<IActionResult> PatchAccountAsync([FromBody] JsonPatchDocument<CreateAccountDto> patch, CancellationToken cancellationToken)
        {
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


        /// <summary>
        /// Отправка на почту токена восстановления пароля
        /// </summary>
        /// <param name="resetPasswordTokenAccountDto">Модель токена</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Письмо на почту</returns>
        [HttpPost("reset-password-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordToken([FromBody] ResetPasswordTokenAccountDto resetPasswordTokenAccountDto, CancellationToken cancellationToken)
        {
            var existingAccount = await _accountService.GetAccountByLoginAsync(resetPasswordTokenAccountDto.Login, cancellationToken);
            if (existingAccount == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Не найден аккаунт с логином: {resetPasswordTokenAccountDto.Login}.");
            }
            var secretKey = _сonfiguration["Jwt:Key"];
            var token = new JwtSecurityToken
                (
                expires: DateTime.UtcNow.AddHours(2),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256
                    )
                );


            bool emailResponse = _emailService.SendEmailPasswordReset(resetPasswordTokenAccountDto.Login, token);

            if (emailResponse)
            {
                return StatusCode((int)HttpStatusCode.OK, $"Письмо с токеном восстановления отправлено на: {resetPasswordTokenAccountDto.Login}.");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, $"Невозможно отправить письмо на почту: {resetPasswordTokenAccountDto.Login}!");
            }
        }


        /// <summary>
        /// Восстановление пароля
        /// </summary>
        /// <param name="resetPasswordAccountDto">Модель восстановления пароля</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Обновленная модель аккаунта</returns>
        [HttpPost("ResetPassword")]
        [ProducesResponseType(typeof(ResetPasswordAccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordAccountDto resetPasswordAccountDto, CancellationToken cancellationToken)
        {
            var existingAccount = await _accountService.GetAccountByLoginAsync(resetPasswordAccountDto.Login, cancellationToken);
            if (existingAccount == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (string.Compare(resetPasswordAccountDto.Password, resetPasswordAccountDto.ConfirmedPassword) != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Пароли не совпадают");
            }

            if(string.IsNullOrEmpty(resetPasswordAccountDto.Token))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Токен не найден");
            }

            await _accountService.ResetPasswordAsync(resetPasswordAccountDto, cancellationToken);


            return StatusCode(StatusCodes.Status200OK, "Пароль успешно сменён!");
        }
    }
}
