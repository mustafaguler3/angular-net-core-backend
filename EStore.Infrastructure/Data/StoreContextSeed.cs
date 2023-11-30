using System;
using System.Text.Json;
using EStore.Core.Entities;
using EStore.Core.Entities.OrderAggregate;

namespace EStore.Infrastructure.Data
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext context)
		{
			if (!context.ProductBrands.Any())
			{
				var brandsData = File.ReadAllText("../EStore.Infrastructure/Data/SeedData/brands.json");
				var brandData = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				context.ProductBrands.AddRange(brandData);
			}

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../EStore.Infrastructure/Data/SeedData/types.json");
                var typeData = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                context.ProductTypes.AddRange(typeData);
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../EStore.Infrastructure/Data/SeedData/products.json");
                var data = JsonSerializer.Deserialize<List<Product>>(productsData);

                context.Products.AddRange(data);
            }

            if (!context.DeliveryMethods.Any())
            {
                var deliverData = File.ReadAllText("../EStore.Infrastructure/Data/SeedData/delivery.json");
                var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverData);

                context.DeliveryMethods.AddRange(delivery);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
	}
}

