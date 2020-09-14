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
        Task DeleteAsync(int id);
        Task DeleteRangeAsync(ICollection<T> entries);
        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> exp, int offset, int max);

    }
}
