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
    public class AccountConfiguration : IEntityTypeConfiguration<Domain.Account.Account>
    {
        public void Configure(EntityTypeBuilder<Domain.Account.Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).HasMaxLength(256).IsRequired();
            builder.Property(a => a.LastName).HasMaxLength(256).IsRequired();
            builder.Property(a => a.NickName).HasMaxLength(256).IsRequired();
            builder.Property(a => a.Login).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(50).IsRequired();
            builder.Property(a => a.RegistrationDate).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));
        }
    }
}
