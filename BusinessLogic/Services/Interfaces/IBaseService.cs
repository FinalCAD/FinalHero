using DAL.Models.Interfaces;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBaseService<T> where T : class, IBaseEntity
    {
        Task<T> GetByIdAsyncBase(int id);

        Task<T> CreateBase(T entity);

        Task<T> UpdateBase(T entity);

        Task<T> DeleteByIdBase(int id);
    }
}
