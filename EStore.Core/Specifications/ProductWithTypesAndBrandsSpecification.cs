using System;
using EStore.Core.Entities;
using EStore.Core.RequestParameters;

namespace EStore.Core.Specifications
{
	public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductWithTypesAndBrandsSpecification(ProductSpecParams productSpec):base(x=> (string.IsNullOrEmpty(productSpec.Search) || x.Name.ToLower().Contains(productSpec.Search))&&(!productSpec.BrandId.HasValue || x.ProductBrandId == productSpec.BrandId) && (!productSpec.TypeId.HasValue || productSpec.TypeId == x.ProductTypeId))
		{
			AddInclude(x => x.ProductType);
			AddInclude(x => x.ProductBrand);
			AddOrderBy(i => i.Name);
			ApplyPaging(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);

			if (!string.IsNullOrEmpty(productSpec.Sort))
			{
				switch (productSpec.Sort)
				{
					case "priceAsc":
						AddOrderBy(p => p.Price);
						break;
					case "priceDesc":
						AddOrderByDescending(p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}
			}
		}

        public ProductWithTypesAndBrandsSpecification(int id):base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}

