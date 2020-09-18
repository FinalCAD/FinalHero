using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class HeroPowerService:BaseService<HeroPower,IBaseRepository<HeroPower>>, IHeroPowerService
    {
        private readonly IBaseRepository<HeroPower> _repository;
        private readonly IMapper _mapper;


        public HeroPowerService(IBaseRepository<HeroPower> repository, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task DeleteHeroPowersByHeroId(int id)
        {
            var heroPowers = await _repository.Query(x => x.HeroId == id).ToListAsync();
            
            await _repository.DeleteRangeAsync(heroPowers);

        }


        
    }
}
