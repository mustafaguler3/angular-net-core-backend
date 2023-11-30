using System;
using AutoMapper;
using EStore.API.Dtos;
using EStore.Core.Entities;
using EStore.Core.Entities.OrderAggregate;

namespace EStore.API.Mappers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(i => i.ProductBrand, i => i.MapFrom(i => i.ProductBrand.Name))
				.ForMember(i => i.ProductType, i => i.MapFrom(i => i.ProductType.Name))
				.ForMember(i => i.PictureUrl, i => i.MapFrom<ProductUrlResolver>());

			CreateMap<Core.Entities.Address, AddressDto>().ReverseMap();
			CreateMap<AddressDto, EStore.Core.Entities.OrderAggregate.Address>();
            CreateMap<BasketDto, Basket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<OrderDto, Order>().ReverseMap();
			
			CreateMap<Order, OrderReturnDto>()
				.ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
				.ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price)).ReverseMap();

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
				.ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
				.ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>()).ReverseMap();
			
            
        }
	}
}

