using System;
using System.Collections.Generic;

namespace WebApplication1.Model;

public partial class PersonalVisit
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdWorker { get; set; }

    public DateTime Date { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual Worker IdWorkerNavigation { get; set; } = null!;
}
