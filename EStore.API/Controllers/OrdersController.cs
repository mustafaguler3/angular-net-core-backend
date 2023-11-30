using System;
using System.Security.Claims;
using AutoMapper;
using EStore.API.Dtos;
using EStore.API.Extentions;
using EStore.Core.Entities.OrderAggregate;
using EStore.Core.Exceptions;
using EStore.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.API.Controllers
{
	[Authorize]
	public class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, IMapper mapper, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            
                //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                var email = HttpContext.User.RetrieveEmailFromPrincipal();

                var address = _mapper.Map<Address>(orderDto.ShipToAddress);

                var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

                if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

                return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderReturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<OrderReturnDto>>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id,email);

            if (order == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<OrderReturnDto>(order));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}

