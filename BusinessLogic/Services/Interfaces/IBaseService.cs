using DAL.Models;
using DAL.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBaseService<T> 
        where T:  IBaseEntity
    {
        Task<T> GetByIdAsync(int id);
        
        Task<T> AddOrUpdateAsync(T entry);

        Task<ICollection<T>> AddOrUpdateRangeAsync(ICollection<T> entries);

        Task DeleteAsync(int id);

        Task<bool> ExistedAsync(int id);
        //Task<bool> ExistedAsync(T entity);

        Task<bool> ExistedRangeByIdsAsync(ICollection<int> entityIds);
        //Task<bool> ExistedRangeAsync(ICollection<T> entities);


        Task DeleteRangeAsync(ICollection<T> entries);
        
        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> exp, int offset, int max);

    }
}
