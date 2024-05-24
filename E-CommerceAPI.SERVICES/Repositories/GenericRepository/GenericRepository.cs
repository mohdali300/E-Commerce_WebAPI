using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SERVICES.Repositories.GenericRepository
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
    }
}
