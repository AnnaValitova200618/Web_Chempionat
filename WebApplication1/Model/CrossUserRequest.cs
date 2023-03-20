using System;
using System.Collections.Generic;

namespace WebApplication1.Model;

public partial class CrossUserRequest
{
    public int Id { get; set; }

    public int IdRequest { get; set; }

    public int IdUser { get; set; }

    public virtual Request IdRequestNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
