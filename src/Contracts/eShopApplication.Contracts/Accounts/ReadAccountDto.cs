using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Accounts
{
    /// <summary>
    /// Модель чтения аккаунта
    /// </summary>
    public class ReadAccountDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Мобильный номер
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Никнейм пользователя
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Признак блокировки.
        /// </summary>
        public bool IsBlocked { get; set; }
        /// <summary>
        /// Клеймы пользователя
        /// </summary>
        public List<Claim> Claims { get; set; } = new List<Claim>();

    }
}
