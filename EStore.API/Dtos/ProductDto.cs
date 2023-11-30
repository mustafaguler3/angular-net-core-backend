using System;
using EStore.Core.Entities;

namespace EStore.API.Dtos
{
	public class ProductDto : BaseEntity
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
    }
}

