using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = Dal.Models.Task;


namespace Dal.Interfaces
{
    public interface ITasks : IDalCrud<Task>
    {
        Task<Task> GetById(int id);
        Task<List<PriorityCode>> ReadAllPriorityAsync();
        Task<List<StatusCodeProject>> ReadAllStatusAsync();
    }
}
