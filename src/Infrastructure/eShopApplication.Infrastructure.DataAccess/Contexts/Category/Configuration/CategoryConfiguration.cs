using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopApplication.Infrastructure.DataAccess.Contexts.Category.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Category.Category>
    {
        public void Configure(EntityTypeBuilder<Domain.Category.Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ParentId);
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();

            builder.HasMany(c => c.Adverts)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
