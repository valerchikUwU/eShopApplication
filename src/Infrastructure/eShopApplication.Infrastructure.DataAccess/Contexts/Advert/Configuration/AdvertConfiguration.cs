using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Conteats.Advert.Configuration
{
    public class AdvertConfiguration : IEntityTypeConfiguration<Domain.Advert.Advert>
    {
        public void Configure(EntityTypeBuilder<Domain.Advert.Advert> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(2000).IsRequired();
            builder.Property(a => a.CreatedAt).HasConversion(a => a, a => DateTime.SpecifyKind(a, DateTimeKind.Utc));
            builder.Property(a => a.Quantity);
            builder.Property(a => a.IsActive).HasDefaultValue(true);
            builder.Property(a => a.Cost);
        }
    }
}
