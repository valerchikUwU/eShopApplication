using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Account.Configuration
{
    public class AdvertConfiguration : IEntityTypeConfiguration<Domain.Account.Account>
    {
        public void Configure(EntityTypeBuilder<Domain.Account.Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.RegistrationDate).HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));

            builder.HasMany(x => x.Adverts).WithOne(x => x.Account).HasForeignKey(x => x.AccountId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
