using E_CommerceAPI.ENTITES.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<ResponseDto> GetAllCategories();
        public Task<ResponseDto> GetCategoryById(int id);
        public Task<ResponseDto> GetCategoryByName(string name);

        public Task<ResponseDto> AddCategory(CategoryDto dto);
        public Task<ResponseDto> UpdateCategory(int id,  CategoryDto dto);
        public Task<ResponseDto> DeleteCategory(int id);
    }
}
