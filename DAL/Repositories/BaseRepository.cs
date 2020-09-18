using DAL.Models.Interfaces;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BaseRepository<TConcext, T> : IBaseRepository<T>
        where T : class, IBaseEntity
        where TConcext : DbContext
    {
        private readonly TConcext _Context;

        #region Constructor
        public BaseRepository(TConcext appContext)
        {
            _Context = appContext;
        }

        #endregion

        #region Create

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> InsertAsync(T entry)
        {
            await _Context.Set<T>().AddAsync(entry);
            _Context.SaveChanges();
            return entry;
        }


        public async Task<ICollection<T>> InsertRangeAsync(ICollection<T> entries)
        {
            await _Context.Set<T>().AddRangeAsync(entries);
            _Context.SaveChanges();
            return entries;
        }

        #endregion

        #region Read

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<ICollection<T>> ListAsync(Expression<System.Func<T, bool>> exp, int offset, int limit)
        {
            return await _Context.Set<T>().Skip(offset).Take(limit).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> exp, int offset, int limit)
        {
            return _Context.Set<T>().Where(exp).Skip(offset).Take(limit);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> exp)
        {
            return _Context.Set<T>().Where(exp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> Query()
        {
            return _Context.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Set()
        {
            return _Context.Set<T>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<long> CountAsync(Expression<System.Func<T, bool>> exp = null)
        {
            return await _Context.Set<T>().CountAsync(exp);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistEntityAsync(int id)
        {
            return await _Context
                .Set<T>()
                .AnyAsync(t => t.Id == id);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<bool> ExistEntityAsync(Expression<System.Func<T, bool>> exp)
        {
            return await _Context
                .Set<T>()
                .Where(exp)
                .AnyAsync();
        }


        #endregion

        #region Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T entity)
        {

            var entityInDb = _Context.Set<T>().Find(entity.Id);

            if (entityInDb == null)
                throw new System.Exception();

            _Context.Entry(entityInDb).CurrentValues.SetValues(entity);
            await _Context.SaveChangesAsync();
            return entityInDb;
        }

        #endregion

        #region Delete

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(T entity)
        {
            _Context.Set<T>().Remove(entity);
            return await _Context.SaveChangesAsync();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> DeleteRangeAsync(ICollection<T> entities)
        {
            _Context.Set<T>().RemoveRange(entities);
            return await _Context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> exp) => await _Context.Set<T>().FirstOrDefaultAsync();


        public async Task<bool> ExistEntitiesRangeAsync(ICollection<int> entityIds)
        {
            foreach (var id in entityIds)
            {
                if (!await _Context.Set<T>()
                    .AnyAsync(t => t.Id == id))
                    return false;
            }
            return true;
        }


        public async Task<bool> ExistEntitiesRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                if (!await _Context.Set<T>()
                    .AnyAsync(t => t.Id == entity.Id))
                    return false;
            }
            return true;
        }
        #endregion
    }
}
