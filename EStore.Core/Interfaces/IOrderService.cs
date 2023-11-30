using System;
using EStore.Core.Entities.OrderAggregate;

namespace EStore.Core.Interfaces
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);

		Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

		Task<Order> GetOrderByIdAsync(int id, string buyerEmail);

		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
	}
}

