using System;
using EStore.Core.Entities;
using EStore.Core.Entities.OrderAggregate;
using EStore.Core.Interfaces;
using EStore.Core.Specifications;
using Address = EStore.Core.Entities.OrderAggregate.Address;

namespace EStore.Infrastructure.Services
{
	public class OrderService : IOrderService
	{
    
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket from the repo
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // get items from the product repo
            var items = new List<OrderItem>();

                foreach (var item in basket.Items)
                {
                    var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                    var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                    items.Add(orderItem);
                }

                // get delivery method from repo
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

                // calc subtotal
                var subtotal = items.Sum(item => item.Price * item.Quantity);

                // check to see if order exists
                var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
                var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

                if(order != null)
                {
                order.ShipToAddress = shippingAddress;
                order.DeliveryMethod = deliveryMethod;
                order.Subtotal = subtotal;
                _unitOfWork.Repository<Order>().Update(order);
                }else
            {
                // create Order
                order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, basket.PaymentIntentId);
                _unitOfWork.Repository<Order>().Add(order);
            }

            
                // save to db
                await _unitOfWork.CompleteAsync();

                // delete basket
                await _basketRepository.DeleteBasketAsync(basketId);

                return order;
            
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}

