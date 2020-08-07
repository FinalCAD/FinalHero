using DAL.Context;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly AppContext _context;

        public HeroRepository(AppContext context)
        {
            _context = context;
        }
    }
}
