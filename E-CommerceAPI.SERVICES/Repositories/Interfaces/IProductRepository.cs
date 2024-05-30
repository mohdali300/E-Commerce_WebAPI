using E_CommerceAPI.ENTITES.DTOs;
using E_CommerceAPI.ENTITES.DTOs.ProductDTO;
using E_CommerceAPI.ENTITES.Models;
using E_CommerceAPI.SERVICES.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Task<ResponseDto> GetAllProducts();
        public Task<ResponseDto> GetProductById(int id);
        public Task<ResponseDto> GetProductByName(string name);
        public Task<ResponseDto> GetProductsByCategoryId(int idd);
        public Task<ResponseDto> GetProductsByCategoryName(string name);
        public Task<ResponseDto> GetProductsByBrandId(int id);
        public Task<ResponseDto> GetProductsByBrandName(string name);

        public Task<ResponseDto> AddProductAsync(AddProductDto dto);
        public Task<ResponseDto> DeleteProductAsync(int id);
        public Task<ResponseDto> UpdateProductAsync(ProductDto dto);

    }
}
