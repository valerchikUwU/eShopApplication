using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Domain.AccountRole
{
    /// <summary>
    /// Модель роли
    /// </summary>
    public class AccountRole
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public Guid AccountRoleId { get; set; }
        /// <summary>
        /// Название роли
        /// </summary>
        public string AccountRoleName { get; set; }
        /// <summary>
        /// Описание роли
        /// </summary>
        public string AccountRoleDescription { get; set;}
        /// <summary>
        /// Список аккаунтов
        /// </summary>
        public virtual List<Domain.Account.Account> Accounts { get; set; }

    }
}
