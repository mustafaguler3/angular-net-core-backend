using System;
using AutoMapper;
using EStore.API.Dtos;
using EStore.Core.Entities;
using EStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketsController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasketById([FromQuery] string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new Basket(id));
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket(BasketDto basketDto)
        {
            var basket = _mapper.Map<BasketDto, Basket>(basketDto);

            var updated = await _basketRepository.UpdateBasketAsync(basket);

            return Ok(updated);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasket([FromQuery] string id)
        {
            await _basketRepository.DeleteBasketAsync(id);

            return Ok();
        }
    }
}

