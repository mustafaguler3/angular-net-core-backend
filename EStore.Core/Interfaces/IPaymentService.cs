using System;
using EStore.Core.Entities;

namespace EStore.Core.Interfaces
{
	public interface IPaymentService
	{
		Task<Basket> CreateOrUpdatePaymentIntent(string basketId);
	}
}

