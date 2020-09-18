using DAL.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class , IBaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task DeleteAsync(T entity);

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
