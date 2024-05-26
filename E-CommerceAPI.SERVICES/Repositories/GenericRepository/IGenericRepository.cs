using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByNameAsync(string name);
        public Task<T> AddAsync(T model);
        public Task<T> UpdateAsync(T model);
        public Task<T> DeleteAsync(int Id);

    }
}
