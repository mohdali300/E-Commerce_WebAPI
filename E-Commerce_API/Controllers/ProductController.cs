using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            if (ModelState.IsValid)
            {
                var products = await _unitOfWork.Products.GetAllProducts();
                if(products != null)
                    return Ok(products);
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);

        }
    }
}
