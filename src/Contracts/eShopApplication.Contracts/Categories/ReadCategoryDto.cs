using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Categories
{
    /// <summary>
    /// Модель чтения категории
    /// </summary>
    public class ReadCategoryDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор родительской категории
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

    }
}
