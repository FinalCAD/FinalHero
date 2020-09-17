using AutoMapper;
using BusinessLogic.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class PowerService : BaseService<Power, IBaseRepository<Power>>, IPowerService
    {
        private readonly IPowerRepository _repository;
        private readonly IMapper _mapper;

        public PowerService(IPowerRepository repository, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
