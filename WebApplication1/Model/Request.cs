using System;
using System.Collections.Generic;

namespace WebApplication1.Model;

public partial class Request
{
    public int Id { get; set; }

    public int IdWorker { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public int IdStatus { get; set; }

    public int IdVisitPurpose { get; set; }

    public int? IdRejectionReason { get; set; }

    public virtual ICollection<CrossUserRequest> CrossUserRequests { get; } = new List<CrossUserRequest>();

    public virtual RejectonReason? IdRejectionReasonNavigation { get; set; }

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual VisitPurpose IdVisitPurposeNavigation { get; set; } = null!;

    public virtual Worker IdWorkerNavigation { get; set; } = null!;
}
