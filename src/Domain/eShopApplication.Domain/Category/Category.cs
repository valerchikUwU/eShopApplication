using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Domain.Category
{
    public class Category
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
        /// Наименование
        /// </summary>
        public string Name { get; set; }
  
        /// <summary>
        /// Объявления
        /// </summary>
        public virtual List<Domain.Advert.Advert> Adverts { get; set; }
    }
}
