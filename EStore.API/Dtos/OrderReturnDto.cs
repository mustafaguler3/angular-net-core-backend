using System;
using EStore.Core.Entities.OrderAggregate;

namespace EStore.API.Dtos
{
	public class OrderReturnDto
	{
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } 
        public Address ShipToAddress { get; set; }
        public decimal ShippingPrice { get; set; }
        public string DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } 
    }
}

