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

    public class ProductController(IGenericRepository<Product> genericRepository, IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo= genericRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet("")]
        public async Task<IActionResult> GetAll() 
        {
            ProductWithBrandAndCategorySpecification spec = new ProductWithBrandAndCategorySpecification();
            var result = await _productRepo.GetAllWithSpecAsync(spec);
            var dto = _mapper.Map<IReadOnlyList<productToReturnDto>>(result);
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
            var dto = _mapper.Map<productToReturnDto>(result);
            return Ok(dto);
        }
    }
}
