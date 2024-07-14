﻿using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Status { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual StatusCodeProject? StatusNavigation { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
