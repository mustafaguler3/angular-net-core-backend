using System;
using EStore.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EStore.Infrastructure.config
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public OrderConfiguration()
		{
		}

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(i => i.ShipToAddress, i => i.WithOwner());
            builder.Navigation(i => i.ShipToAddress).IsRequired();
            builder.Property(i => i.Status).HasConversion(i => i.ToString(),i => (OrderStatus)Enum.Parse(typeof(OrderStatus), i));

            builder.HasMany(i => i.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}

