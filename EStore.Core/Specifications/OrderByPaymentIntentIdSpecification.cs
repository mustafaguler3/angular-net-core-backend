using System;
using EStore.Core.Entities.OrderAggregate;

namespace EStore.Core.Specifications
{
	public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
	{
		public OrderByPaymentIntentIdSpecification(string paymentIntentId) :base(i => i.PaymentIntentId == paymentIntentId)
		{
		}
	}
}

