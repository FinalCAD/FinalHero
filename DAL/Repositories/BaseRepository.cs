using DAL.Models.Interfaces;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BaseRepository<TContext, T> : IBaseRepository<T> where TContext : DbContext 
                                                                  where T : class , IBaseEntity
    {
        private readonly TContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(TContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        #region Methods
        /// <summary>
        /// Get all entities
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        /// <summary>
        /// Get an entity by its id 
        /// </summary>
        /// <param name="id">The entity's id</param>
        /// <returns>The entity</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Id == id);
        }  

        /// <summary>
        /// Delete an entity
        /// </summary>
        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Create an entity
        /// </summary>
        public async Task InsertAsync(T entity)
        {
            await _entities.AddAsync(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        public async Task UpdateAsync(T entity)
        {
            var oldEntity = await _context.FindAsync<T>(entity.Id);
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
