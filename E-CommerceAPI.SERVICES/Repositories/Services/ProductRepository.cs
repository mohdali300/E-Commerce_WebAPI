using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Data;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using E_CommerceAPI.SERVICES.Repositories.Interfaces;
using E_CommerceAPI.SERVICES.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Services
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        //private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ECommerceDbContext context, IMapper mapper) :base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllProducts()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            if(products!=null && products.Count() > 0)
            {
                var dto = _mapper.Map<List<ProductDto>>(products);
                foreach(var product in dto)
                {
                    product.Category =await _context.Products.Where(p => p.Name == product.Name)
                        .Select(p => p.Category.Name).FirstOrDefaultAsync();
                    product.Brand=await _context.Products.Where(p => p.Name == product.Name)
                        .Select(p => p.Brand.Name).FirstOrDefaultAsync();
                }

                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto,
                    Message=""
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "An error occured or there is no Products in stock."
            };
            
        }

        public async Task<ResponseDto> GetProductById(int id)
        {
            var product=await _context.Products.FindAsync(id);
            if (product != null)
            {
                var dto =_mapper.Map<ProductDto>(product);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Message = "",
                    Model = dto,
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                Message = "Sorry, this product does not exist!",
                IsSucceeded = false,
            };
        }

        public Task<ResponseDto> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetProductsByBrandId(int catId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetProductsByBrandName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetProductsByCategoryId(int catId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetProductsByCategoryName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
