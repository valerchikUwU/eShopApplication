using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Domain.Advert
{
    public class Advert
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак актуальности.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Дата/время создания (UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Локация продавца
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Кол-во экземпляров
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Идентификатор аккаунта.
        /// </summary>
        //public Guid AccountId { get; set; }

        /// <summary>
        /// Аккаунт.
        /// </summary>
        //public virtual Domain.Account.Account Account { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Категория.
        /// </summary>
        public virtual Domain.Category.Category Category { get; set; }
    }
}
