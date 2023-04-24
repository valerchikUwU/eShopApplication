using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Files
{
    public class ReadFileDto
    {
        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование файла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата создания файла.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Размер файла.
        /// </summary>
        public int Length { get; set; }
    }
}
