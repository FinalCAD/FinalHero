using DAL.Models.Interfaces;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBaseService<T> where T : class, IBaseEntity
    {
        //Task<T> CreateOrUpdate(T entry, bool? ignoreQueryFilter = null);

        Task<T> GetByIdAsync(int id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<T> DeleteById(int id);
    }
}
