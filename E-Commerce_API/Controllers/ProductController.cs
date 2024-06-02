using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs.ProductDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Authorization;
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
                if (products.IsSucceeded)
                    return StatusCode(products.StatusCode, products.Model);
                return StatusCode(products.StatusCode, products.Message);
            }
            return BadRequest(ModelState);

        }

        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (ModelState.IsValid)
            {
                var product = await _unitOfWork.Products.GetProductById(id);
                if (product.IsSucceeded)
                    return StatusCode(product.StatusCode, product.Model);
                return StatusCode(product.StatusCode, product.Message);
            }
            return BadRequest(ModelState);

        }

        [HttpGet("Name/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            if (ModelState.IsValid)
            {
                var product = await _unitOfWork.Products.GetProductByName(name);
                if (product.IsSucceeded)
                    return StatusCode(product.StatusCode, product.Model);
                return StatusCode(product.StatusCode, product.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("BrandId/{id}")]
        public async Task<IActionResult> GetProductsByBrandId(int id)
        {
            if (ModelState.IsValid)
            {
                var products = await _unitOfWork.Products.GetProductsByBrandId(id);
                if (products.IsSucceeded)
                    return StatusCode(products.StatusCode, products.Model);
                return StatusCode(products.StatusCode, products.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("BrandName/{name}")]
        public async Task<IActionResult> GetProductsByBrandName(string name)
        {
            if (ModelState.IsValid)
            {
                var products = await _unitOfWork.Products.GetProductsByBrandName(name);
                if (products.IsSucceeded)
                    return StatusCode(products.StatusCode, products.Model);
                return StatusCode(products.StatusCode, products.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("CategoryId/{id}")]
        public async Task<IActionResult> GetProductsByCategoryId(int id)
        {
            if (ModelState.IsValid)
            {
                var products = await _unitOfWork.Products.GetProductsByCategoryId(id);
                if (products.IsSucceeded)
                    return StatusCode(products.StatusCode, products.Model);
                return StatusCode(products.StatusCode, products.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("CategoryName/{name}")]
        public async Task<IActionResult> GetProductsByCategoryName(string name)
        {
            if (ModelState.IsValid)
            {
                var products = await _unitOfWork.Products.GetProductsByCategoryName(name);
                if (products.IsSucceeded)
                    return StatusCode(products.StatusCode, products.Model);
                return StatusCode(products.StatusCode, products.Message);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var product=await _unitOfWork.Products.AddProductAsync(dto);
                if (product.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(product.StatusCode, product.Model);
                }
                return StatusCode(product.StatusCode, product.Message);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                var product = await _unitOfWork.Products.DeleteProductAsync(id);
                if (product.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(product.StatusCode, product.Model);
                }
                return StatusCode(product.StatusCode, product.Message);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id,[FromForm] AddProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Products.UpdateProductAsync(id,dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
