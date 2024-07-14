using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.models
{
    public class Communications
    {
        public int CommunicationId { get; set; }

        public string Type { get; set; } = null!;

        public DateTime? Date { get; set; }

        public string? Details { get; set; }

        public string RelatedTo { get; set; } = null!;

        public int RelatedId { get; set; }

        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
