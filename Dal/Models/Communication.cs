using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Communication
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
