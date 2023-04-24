using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts.Files
{
    public class CreateFileDto
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Контент файла.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// ContentType файла.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Размер файла.
        /// </summary>
        public int Length { get; set; }
    }
}
