using AutoMapper;
using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.ProductDTO;
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
        private List<string> Extentions = new() { ".jpg",".png"};

        public ProductRepository(ECommerceDbContext context, IMapper mapper) :base(context)
        {
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p=>p.Brand).Include(p=>p.Category)
                .AsNoTracking().ToListAsync();
            if(products!=null && products.Count() > 0)
            {
                var dto = _mapper.Map<List<ProductDto>>(products);

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
            var product =await _context.Products
                .Include(p => p.Brand).Include(p => p.Category)
                .FirstOrDefaultAsync();
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

        public async Task<ResponseDto> GetProductByName(string name)
        {
            var product =await _context.Products.Where(p=>p.Name==name)
                .Include(p => p.Brand).Include(p => p.Category)
                .FirstOrDefaultAsync();
            if (product != null)
            {
                var dto = _mapper.Map<ProductDto>(product);

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

        public async Task<ResponseDto> GetProductsByBrandId(int id)
        {
            var products = await _context.Products.Where(p=>p.BrandId==id)
                .Include(p => p.Category).Include(p => p.Brand)
                .ToListAsync();
            if (products != null && products.Count() > 0)
            {
                var dto = _mapper.Map<List<ProductDto>>(products);

                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto,
                    Message = ""
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "No products in stock for this brand, it will be available soon."
            };
        }

        public async Task<ResponseDto> GetProductsByBrandName(string name)
        {
            var products = await _context.Products.Where(p => p.Brand!.Name == name)
                .Include(p => p.Category).Include(p => p.Brand)
                .ToListAsync();
            if (products != null && products.Count() > 0)
            {
                var dto = _mapper.Map<List<ProductDto>>(products);

                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto,
                    Message = ""
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "No products in stock for this brand, it will be available soon."
            };
        }

        public async Task<ResponseDto> GetProductsByCategoryId(int catId)
        {
            var products = await _context.Products.Where(p => p.CategoryId == catId)
                .Include(p => p.Category).Include(p => p.Brand)
                .ToListAsync();
            if (products != null && products.Count() > 0)
            {
                var dto = _mapper.Map<List<ProductDto>>(products);

                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto,
                    Message = ""
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "No products in stock for this category, it will be available soon."
            };
        }

        public async Task<ResponseDto> GetProductsByCategoryName(string name)
        {
            var products = await _context.Products.Where(p => p.Category!.Name == name)
                .Include(p => p.Category).Include(p => p.Brand)
                .ToListAsync();
            if (products != null && products.Count() > 0)
            {
                var dto = _mapper.Map<List<ProductDto>>(products);

                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = dto,
                    Message = ""
                };
            }

            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "No products in stock for this category, it will be available soon."
            };
        }

        public async Task<ResponseDto> AddProductAsync(AddProductDto dto)
        {
            var validCategory = _context.Categories.Any(c => c.Id == dto.CategoryId);
            var vaildBrand = _context.Brands.Any(b => b.Id == dto.BrandId);

            if(!validCategory || !vaildBrand)
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    Message = "Invalid Category or Brand",
                    IsSucceeded = false,
                };
            }

            if (!Extentions.Contains(Path.GetExtension(dto.ImageFile.FileName).ToLower()))
            {
                return new ResponseDto
                {
                    StatusCode = 400,
                    Message = "Invalid image extention!",
                    IsSucceeded = false,
                };
            }

            using var stream= new MemoryStream();
            await dto.ImageFile.CopyToAsync(stream);

            var product=_mapper.Map<Product>(dto);
            product.Image=stream.ToArray();
            await _context.Products.AddAsync(product);
            var entity = _context.Entry(product);

            if(entity.State == EntityState.Added)
            {
                return new ResponseDto
                {
                    StatusCode = 200,
                    IsSucceeded = true,
                    Model = product,
                    Message = ""
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "Product was not marked as Added."
            };
        }

        public async Task<ResponseDto> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                var entity= _context.Entry(product);
                if (entity.State == EntityState.Deleted)
                {
                    return new ResponseDto
                    {
                        StatusCode = 200,
                        IsSucceeded = true,
                        Model = product,
                        Message = "Product deleted successfully."
                    };
                }
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Error was occured, failed to delete product.",
                    Model = product,
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "This product already does not exist."
            };
        }

        public async Task<ResponseDto> UpdateProductAsync(int id, AddProductDto dto)
        {
            var result = await _context.Products.FindAsync(id);
            if(result != null)
            {
                var product=_mapper.Map<Product>(dto);
                product.Id = id;
                _context.Entry(result).CurrentValues.SetValues(product);
                var entity= _context.Entry(result);
                if(entity.State == EntityState.Modified)
                {
                    return new ResponseDto
                    {
                        StatusCode = 200,
                        IsSucceeded = true,
                        Model = product
                    };
                }
                return new ResponseDto
                {
                    StatusCode = 400,
                    IsSucceeded = false,
                    Message = "Failed to update product."
                };
            }
            return new ResponseDto
            {
                StatusCode = 400,
                IsSucceeded = false,
                Message = "This product does not exist."
            };
        }
    }
}
