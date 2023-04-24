using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Adverts
{

    /// <summary>
    /// Модель объявления
    /// </summary>
    public class ReadAdvertDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; set; }

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
        /// Идентификатор категории
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Дата/время создания (UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; }
        public List<Guid> FileIds { get; set; } = new List<Guid>();
        public Guid AccountId { get; set; }
    }
}
