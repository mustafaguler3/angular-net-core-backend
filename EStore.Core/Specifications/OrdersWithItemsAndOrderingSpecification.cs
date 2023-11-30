using System;
using EStore.Core.Entities.OrderAggregate;

namespace EStore.Core.Specifications
{
	public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
	{
		public OrdersWithItemsAndOrderingSpecification(string email): base(o => o.BuyerEmail == email)
		{
			AddInclude(o => o.OrderItems);
			AddInclude(o => o.DeliveryMethod);
			AddOrderByDescending(o => o.OrderDate);
		}

        public OrdersWithItemsAndOrderingSpecification(int id,string email) : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}

