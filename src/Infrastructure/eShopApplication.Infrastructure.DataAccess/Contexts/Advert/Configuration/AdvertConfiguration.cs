using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Advert.Configuration
{
    public class AdvertConfiguration : IEntityTypeConfiguration<Domain.Advert.Advert>
    {
        public void Configure(EntityTypeBuilder<Domain.Advert.Advert> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.CreatedAt).HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
        }
    }
}
