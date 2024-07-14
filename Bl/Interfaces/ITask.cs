using Dto.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Interfaces
{
    public interface ITasks : IBlcrud<Tasks>
    {
        Task<Tasks> GetById(int id);
        Task<List<StatusCodeProject>> ReadAllStatusAsync();
        Task<List<PriorityCode>> ReadAllPriorityAsync();


    }
}
