using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopApplication.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace eShopApplication.Host.DbMigrator
{
    public class MigrationDbContext : eShopApplicationDbContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
