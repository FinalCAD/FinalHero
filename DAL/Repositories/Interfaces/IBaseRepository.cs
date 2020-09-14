using DAL.Models;
using DAL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{

    public interface IBaseRepository<T> where T : class, IBaseEntity
    {

        Task<ICollection<T>> ListAsync(Expression<Func<T, bool>> exp, int offset, int limit);

        Task<T> GetById(int id);

        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<int> DeleteAsync(T entity);
        
        Task<int> DeleteRangeAsync(ICollection<T> entities);

        Task<long> CountAsync(Expression<Func<T, bool>> exp = null);

        Task<bool> ExistEntityAsync(int id);

        Task<bool> ExistEntityAsync(Expression<Func<T, bool>> exp);

    }
}