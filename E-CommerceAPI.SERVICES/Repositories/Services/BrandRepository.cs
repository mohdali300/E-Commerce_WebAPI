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
    public class BrandRepository:GenericRepository<Brand>,IBrandRepository
    {
        private readonly IMapper _mapper;

        public BrandRepository(ECommerceDbContext context,IMapper mapper):base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllBrands()
        {
            var brands=await _context.Brands.AsNoTracking().ToListAsync();
            if (brands != null && brands.Count > 0)
            {
                var dto=_mapper.Map<List<BrandDto>>(brands);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message="No Brands."
            };
        }

        public async Task<ResponseDto> GetBrandById(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                var dto = _mapper.Map<BrandDto>(brand);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 404,
                IsSucceeded = false,
                Message = "Brand not found."
            };
        }

        public async Task<ResponseDto> GetBrandByName(string name)
        {
            var brand = await _context.Brands.Where(b=>b.Name==name).FirstOrDefaultAsync();
            if (brand != null)
            {
                var dto = _mapper.Map<BrandDto>(brand);
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 404,
                IsSucceeded = false,
                Message = "Brand not found."
            };
        }

        public async Task<ResponseDto> AddBrand(BrandDto dto)
        {
            var brand=_mapper.Map<Brand>(dto);
            await _context.AddAsync(brand);

            var entity=_context.Entry(brand);
            if (entity.State == EntityState.Added)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = " Failed to add Brand."
            };
        }

        public async Task<ResponseDto> UpdateBrand(int id, BrandDto dto)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "Brand not found."
                };
            }

            var newBrand=_mapper.Map<Brand>(dto);
            newBrand.Id=id;
            _context.Entry(brand).CurrentValues.SetValues(newBrand);

            var entity = _context.Entry(brand);
            if (entity.State == EntityState.Modified)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = " Failed to edit Brand."
            };
        }

        public async Task<ResponseDto> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return new ResponseDto
                {
                    StatusCode = 404,
                    IsSucceeded = false,
                    Message = "Brand not found."
                };
            }

            var dto=_mapper.Map<BrandDto>(brand);
            _context.Remove(brand);
            var entity = _context.Entry(brand);
            if (entity.State == EntityState.Deleted)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = " Failed to delete Brand."
            };
        }
    }
}
