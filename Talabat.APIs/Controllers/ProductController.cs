using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Product_spec;
using Talabat.Core.Repository.Contract;
using Talabat.Repository.Repositories;

namespace Talabat.APIs.Controllers
{

    public class ProductController(IGenericRepository<Product> genericRepository, IMapper mapper, IGenericRepository<ProductBrand> BrandRepository, IGenericRepository<ProductCategory> CategoryRepository) : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo= genericRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IGenericRepository<ProductBrand> _BrandRepo = BrandRepository;
        private readonly IGenericRepository<ProductCategory> _categoryRepo = CategoryRepository;


        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] string? sort,[FromQuery]int? brandId,[FromQuery]int? categoryId) 
        {
            ProductWithBrandAndCategorySpecification spec = new ProductWithBrandAndCategorySpecification(sort, brandId ,categoryId);
            var result = await _productRepo.GetAllWithSpecAsync(spec);
            var dto = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<productToReturnDto>>(result);
            return Ok(dto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            ProductWithBrandAndCategorySpecification spec = new ProductWithBrandAndCategorySpecification(id);
            var result = await _productRepo.GetWithSpecAsync(spec);
            if (result == null) 
            {
                return NotFound(new ApiResponse(404));
            }
            var dto = _mapper.Map<Product,productToReturnDto>(result);
            return Ok(dto);
        }
        [HttpGet("Brands")]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _BrandRepo.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("Category")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _categoryRepo.GetAllAsync();
            return Ok(result);
        }
    }
}
