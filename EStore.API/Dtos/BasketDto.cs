using System;
using System.ComponentModel.DataAnnotations;

namespace EStore.API.Dtos
{
	public class BasketDto
	{
		[Required]
		public string Id { get; set; }
		public List<BasketItemDto> Items { get; set; }

        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
    }
}

