using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> AddAsync(T entity);
        Task Delete(int id);
        Task<T> Update(T entity);
    }
}