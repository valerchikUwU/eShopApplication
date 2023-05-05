﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Domain.Account
{
    /// <summary>
    /// Модель аккаунта
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
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
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Никнейм пользователя
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public Guid AccountRoleId { get; set; }
        /// <summary>
        /// Название роли
        /// </summary>
        public string AccountRoleName { get; set; }
        /// <summary>
        /// Модель роли
        /// </summary>
        public virtual Domain.AccountRole.AccountRole AccountRole { get; set; }
        /// <summary>
        /// Список моделей объявлений
        /// </summary>
        public virtual List<Domain.Advert.Advert> Adverts { get; set; }
    }
}
