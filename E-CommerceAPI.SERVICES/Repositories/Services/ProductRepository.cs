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
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductRepository(ECommerceDbContext context, IMapper mapper) :base(context)
        {
            //_unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllProducts()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            if(products!=null && products.Count() > 0)
            {
                var dto = _mapper.Map<List<ProductDto>>(products);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto,

                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "An error occured or there is no Products in stock."
            };
            

        }
    }
}
