using Dal.Models;
using Dto.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Interfaces
{
    public interface ILead: IBlcrud<Leads>
    {
        Task<Leads> GetByIdAsync(int id);
    }
}
