﻿using DAL.Models;
using DAL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{

    public interface IBaseRepository<T> where T : class, IBaseEntity
    {

        Task<ICollection<T>> ListAsync(Expression<Func<T, bool>> exp, int offset, int limit);


        IQueryable<T> Query(Expression<Func<T, bool>> exp, int offset, int limit);
       
        IQueryable<T> Query(Expression<Func<T, bool>> exp);
        
        IQueryable<T> Query();

        Task<T> GetById(int id);

        Task<T> GetAsync(Expression<Func<T, bool>> exp);

        Task<T> InsertAsync(T entity);

        Task<ICollection<T>> InsertRangeAsync(ICollection<T> entities);

        Task<T> UpdateAsync(T entity);

        Task<int> DeleteAsync(T entity);
        
        Task<int> DeleteRangeAsync(ICollection<T> entities);

        Task<long> CountAsync(Expression<Func<T, bool>> exp = null);

        Task<bool> ExistEntityAsync(int id);
        Task<bool> ExistEntitiesRangeAsync(ICollection<int> ids);

        Task<bool> ExistEntityAsync(Expression<Func<T, bool>> exp);

    }
}