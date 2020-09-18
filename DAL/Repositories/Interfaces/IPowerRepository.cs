using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IPowerRepository : IBaseRepository<Power>
    {
        Task<Power> GetByNameAsync(string name);
    }
}
