using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.File.Configuration
{
        /// <summary>
        /// Конфигурация сущности Файла.
        /// </summary>
        public class FileConfiguration : IEntityTypeConfiguration<Domain.File.File>
        {
            /// <inheritdoc/>
            public void Configure(EntityTypeBuilder<Domain.File.File> builder)
            {
                builder.HasKey(a => a.Id);
                builder.Property(a => a.Name).HasMaxLength(256).IsRequired();
                builder.Property(a => a.ContentType).HasMaxLength(256).IsRequired();
                builder.Property(a => a.Created).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));
            }
        }
    
}
