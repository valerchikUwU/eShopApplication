using eShopApplication.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess
{
    public class eShopApplicationDbContextConfiguration : IDbContextOptionsConfigurator<eShopApplicationDbContext>
    {

        private const string MSSQLConnectionStringName = "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public eShopApplicationDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public void Configure(DbContextOptionsBuilder<eShopApplicationDbContext> options)
        {
            var connectionString = _configuration.GetConnectionString(MSSQLConnectionStringName);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"Не найдена строка подключения с именем '{MSSQLConnectionStringName}'");
            }
            Console.WriteLine(connectionString);
            options.UseSqlServer(connectionString);
            options.UseLoggerFactory(_loggerFactory);
        }
    }
}
