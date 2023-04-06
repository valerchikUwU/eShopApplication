using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Adverts
{

    /// <summary>
    /// Модель создания объявления
    /// </summary>
    public class CreateAdvertDto
    {


        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

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
        /// Флаг активности объявления 
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
