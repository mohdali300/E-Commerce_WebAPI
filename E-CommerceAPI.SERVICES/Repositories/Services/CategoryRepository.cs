using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Data;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        private readonly IMapper _mapper;
        public CategoryRepository(ECommerceDbContext context, IMapper mapper) :base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllCategories()
        {
            var categories=await _context.Categories.AsNoTracking().ToListAsync();
            if(categories!=null && categories.Count > 0 )
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = categories
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "There is no categories yet."
            };
        }

        public async Task<ResponseDto> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "This category is not exist."
                };
            }

            return new ResponseDto
            {
                StatusCode = 200,
                IsSucceeded = true,
                Model= category
            };
        }

        public async Task<ResponseDto> GetCategoryByName(string name)
        {
            var category = await _context.Categories.Where(c=>c.Name==name).FirstOrDefaultAsync();
            if (category == null)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "This category is not exist."
                };
            }

            return new ResponseDto
            {
                StatusCode = 200,
                IsSucceeded = true,
                Model = category
            };
        }

        public async Task<ResponseDto> AddCategory(CategoryDto dto)
        {
            if(await _context.Categories.AnyAsync(c => c.Name == dto.Name))
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "This category is already exists."
                };
            }

            var category=_mapper.Map<Category>(dto);
            var result=await _context.Categories.AddAsync(category);
            var entity=_context.Entry(category);

            if (entity.State == EntityState.Added)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Message = "This category is already exists.",
                    Model=category
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Failed to add this category."
            };
        }

        public async Task<ResponseDto> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                var entity = _context.Entry(category);
                if(entity.State == EntityState.Deleted)
                {
                    return new ResponseDto
                    {
                        StatusCode = 200,
                        IsSucceeded = true,
                        Model = category,
                        Message = "Category deleted successfully."
                    };
                }

                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Failed to delete this category."
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "This category is not exist."
            };
        }

        public async Task<ResponseDto> UpdateCategory(int id, CategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                var result = _mapper.Map<Category>(dto);
                _context.Entry(category).CurrentValues.SetValues(result);
                var entity = _context.Entry(category);
                if (entity.State == EntityState.Modified)
                {
                    return new ResponseDto
                    {
                        StatusCode = 200,
                        IsSucceeded = true,
                        Model = category,
                        Message = "Category Updated successfully."
                    };
                }

                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Failed to update this category."
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "This category is not exist."
            };
        }
    }
}
