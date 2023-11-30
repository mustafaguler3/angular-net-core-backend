using System;
using EStore.Core.Entities;
using EStore.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Infrastructure.config
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(i => i.Id).IsRequired();
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Description).IsRequired().HasMaxLength(150);
            builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
            builder.Property(i => i.PictureUrl).IsRequired();

            builder.HasOne(i => i.ProductBrand).WithMany().HasForeignKey(i => i.ProductBrandId);

            builder.HasOne(i => i.ProductType).WithMany().HasForeignKey(i => i.ProductTypeId);
        }
    }
}

