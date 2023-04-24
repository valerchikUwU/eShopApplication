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
            builder.Property(a => a.PhoneNumber).HasMaxLength(12).IsRequired();
            builder.Property(a => a.Login).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(50).IsRequired();
            builder.Property(a => a.RegistrationDate).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));
            builder.Property(a => a.AccountRoleId);
            builder.Property(a => a.AccountRoleName);

            builder.HasMany(ac => ac.Adverts)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId)
                .HasPrincipalKey(ac => ac.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.AccountRole)
                .WithMany(ar => ar.Accounts)
                .HasForeignKey(a => a.AccountRoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
           
        }
    }
}
