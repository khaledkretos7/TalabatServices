using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Product_spec;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Spacifications.Productspec;
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
        public async Task<IActionResult> GetAll([FromQuery]ProductSpecParam productSpecParam) 
        {
            
            ProductWithBrandAndCategorySpecification spec = new ProductWithBrandAndCategorySpecification(productSpecParam);
            var result = await _productRepo.GetAllWithSpecAsync(spec);
            var dto = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<productToReturnDto>>(result);
            ProductWithCount productWithCount = new(productSpecParam);
            var Count= await _productRepo.GetCountAsync(productWithCount);
            return Ok(new Pagination<productToReturnDto>(productSpecParam.PageIndex,productSpecParam.PageSize,Count,dto));
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
