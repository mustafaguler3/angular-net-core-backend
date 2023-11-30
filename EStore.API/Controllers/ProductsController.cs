using System;
using AutoMapper;
using EStore.API.Dtos;
using EStore.API.Mappers;
using EStore.Core.Entities;
using EStore.Core.Exceptions;
using EStore.Core.Interfaces;
using EStore.Core.RequestParameters;
using EStore.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace EStore.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository, IGenericRepository<ProductType> productTypeRepository, IGenericRepository<ProductBrand> productBrandRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _productBrandRepository = productBrandRepository;
            _mapper = mapper;
        }

        [HttpGet("get-product-no-params")]
        public async Task<ActionResult<List<ProductDto>>> GetProductsNoParams()
        {
            var products = await _productRepository.ListAllAsync();

            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productSpec)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(productSpec);

            var countSpec = new ProductWithFiltersForCountSpecification(productSpec);

            var totalItems = await _productRepository.CountAsync(countSpec);

            var products = await _productRepository.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(new Pagination<ProductDto>(productSpec.PageIndex,productSpec.PageSize,totalItems,data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec);

            return _mapper.Map<ProductDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<Product>> GetProductBrands()
        {
            var brands = await _productBrandRepository.ListAllAsync();

            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<Product>> GetProductTypes()
        {
            var types = await _productTypeRepository.ListAllAsync();

            return Ok(types);
        }

    }
}

