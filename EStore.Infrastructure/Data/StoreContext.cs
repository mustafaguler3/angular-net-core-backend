using System;
using System.Reflection;
using EStore.Core.Entities;
using EStore.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Data
{
	public class StoreContext : DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options):base(options)
		{
		}

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> ProductBrands { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

			if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
			{
				foreach(var entityType in modelBuilder.Model.GetEntityTypes())
				{
					var properties = entityType.ClrType.GetProperties().Where(i => i.PropertyType == typeof(decimal));

					foreach(var property in properties)
					{
						modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
					}
				}
			}
        }
    }
}

