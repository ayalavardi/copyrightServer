using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.models
{
    public class Tasks
    {
        public int TaskId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public StatusCodeProject Status { get; set; } 

        public PriorityCode? Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public User? AssignedTo { get; set; }

        public Projects? Project { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
