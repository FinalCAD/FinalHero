using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PowerRepository : BaseRepository<AppContext, Power>, IPowerRepository
    {
        private readonly AppContext _context;

        public PowerRepository(AppContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a power by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Power> GetByNameAsync(string name)
        {
            return await _context.Powers.FirstOrDefaultAsync(e => e.Name == name);
        }
    }
}
