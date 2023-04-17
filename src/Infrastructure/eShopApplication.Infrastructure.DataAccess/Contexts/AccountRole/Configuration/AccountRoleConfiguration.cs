using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.AccountRole.Configuration
{
    public class AccountRoleConfiguration : IEntityTypeConfiguration<Domain.AccountRole.AccountRole>
    {
        public void Configure(EntityTypeBuilder<Domain.AccountRole.AccountRole> builder)
        {
            builder.HasKey(a => a.AccountRoleId);
            builder.Property(a => a.AccountRoleName);
            builder.Property(a => a.AccountRoleDescription).HasMaxLength(1000).IsRequired();

            
        }
    }
}
