using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            if(ModelState.IsValid)
            {
                var response=await _unitOfWork.Categories.GetAllCategories();
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Categories.GetCategoryById(id);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("Name/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Categories.GetCategoryByName(name);
                if (response.IsSucceeded)
                    return StatusCode(response.StatusCode, response.Model);
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Categories.AddCategory(dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> EditCategory(int id,CategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Categories.UpdateCategory(id,dto);
                if (response.IsSucceeded)
                {
                    await _unitOfWork.Save();
                    return StatusCode(response.StatusCode, response.Model);
                }
                return StatusCode(response.StatusCode, response.Message);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _unitOfWork.Categories.DeleteCategory(id);
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
