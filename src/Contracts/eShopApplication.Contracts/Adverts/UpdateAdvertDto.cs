using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Adverts
{
    /// <summary>
    /// Модель обновления объявления
    /// </summary>
    public class UpdateAdvertDto
    {

        /// <summary>
        /// Уникальынй идентификатор
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
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
