using System;
using EStore.Core.Entities;
using EStore.Core.RequestParameters;

namespace EStore.Core.Specifications
{
	public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
	{
		public ProductWithFiltersForCountSpecification(ProductSpecParams productSpec) : base(x => (string.IsNullOrEmpty(productSpec.Search) || x.Name.ToLower().Contains(productSpec.Search)) && (!productSpec.BrandId.HasValue || x.ProductBrandId == productSpec.BrandId) && (!productSpec.TypeId.HasValue || productSpec.TypeId == x.ProductTypeId))
        {
		}
	}
}

