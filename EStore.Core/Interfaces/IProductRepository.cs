using System;
using EStore.Core.Entities;

namespace EStore.Core.Interfaces
{
	public interface IProductRepository
	{
		Task<Product> GetProductByIdAsync(int id);

		Task<IReadOnlyList<Product>> GetProductsAsync();

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}

