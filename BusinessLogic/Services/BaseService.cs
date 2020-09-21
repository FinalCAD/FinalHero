using BusinessLogic.Exceptions;
using BusinessLogic.Services.Interfaces;
using DAL.Models.Interfaces;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BaseService<T> : IBaseService<T> where T : class, IBaseEntity
    {
        private readonly IBaseRepository<T> _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This service gets an entity by its id
        /// </summary>
        /// <param name="id">Entity's id</param>
        public async Task<T> GetByIdAsyncBase(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity;
        }

        /// <summary>
        /// This service creates an entity
        /// </summary>
        /// <param name="entity">Entity to create</param>
        public async Task<T> CreateBase(T entity)
        {
            var check = await GetByIdAsyncBase(entity.Id);
            if (check != null)
            {
                throw new BadRequestException("Cannot create " + typeof(T).Name +" because it already exists");
            }
            await _repository.InsertAsync(entity);
            return await _repository.GetByIdAsync(entity.Id);
        }

        /// <summary>
        /// This service updates an entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public async Task<T> UpdateBase(T entity)
        {
            var check = await GetByIdAsyncBase(entity.Id);
            if (check == null)
            {
                throw new BadRequestException("Cannot update " + typeof(T).Name + " because it doesn't exist");
            }
            await _repository.UpdateAsync(entity);
            return await _repository.GetByIdAsync(entity.Id); ;
        }

        /// <summary>
        /// This service deletes an entity by its id
        /// </summary>
        /// <param id="id">Entity's id</param>
        public virtual async Task<T> DeleteByIdBase(int id)
        {
            var entity = await GetByIdAsyncBase(id);
            if (entity == null)
            {
                throw new NotFoundException("Cannot delete " + typeof(T).Name + " with id " + id + " because not found");
            }
            await _repository.DeleteAsync(entity);
            return await GetByIdAsyncBase(id);
        }
    }
}
