using BusinessLogic.DTO;
using BusinessLogic.DTO.Responses;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPowerService : IBaseService<Power>
    {
        Task<PowerDTO> GetByIdAsync(int id);

        Task<PowersResponseDTO> GetAllAsync();

        Task<PowerDTO> GetByNameAsync(string name);

        Task<PowerDTO> Create(PowerDTO powerDTO);

        Task<PowerDTO> Update(int id, string name, string? description);

        Task<PowerDTO> DeleteById(int id);

        Task<PowerDTO> DeleteByName(string name);
    }
}
