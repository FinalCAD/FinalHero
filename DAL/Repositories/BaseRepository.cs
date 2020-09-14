using DAL.Context;
using DAL.Models;
using DAL.Models.Interfaces;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BaseRepository<TConcext,T> : IBaseRepository<T>
        where T : class, IBaseEntity
        where TConcext : DbContext
    {
        private readonly TConcext _Context;
        
        public BaseRepository(TConcext appContext)
        {
            _Context = appContext;
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
        public async Task<T> GetById(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }


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
    }
}
