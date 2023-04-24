using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Contracts
{
    /// Модель ошибки.
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Код ошибки.
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;

        /// <summary>
        /// Сообщение для пользователя.
        /// </summary>
        public string UserMessage { get; set; } = string.Empty;

        /// <summary>
        /// Вложенные ошибки.
        /// </summary>
        public ErrorDto[] InternalErrors { get; set; }
    }

}
