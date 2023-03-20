﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Model;

public partial class VisitPurpose
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; } = new List<Request>();
}
