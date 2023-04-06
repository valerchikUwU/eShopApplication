using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Domain.Account
{
    public class Account
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        //public virtual List<Domain.Advert.Advert> Adverts { get; set; }
    }
}
