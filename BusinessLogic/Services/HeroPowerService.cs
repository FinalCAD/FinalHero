using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Text;

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
    }
}
