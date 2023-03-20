using System;
using System.Collections.Generic;

namespace WebApplication1.Model;

public partial class User
{
    public int Id { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? Patronymic { get; set; }

    public string? NumberPhone { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? PassportSeries { get; set; }

    public string? PassportNumber { get; set; }

    public string? Login { get; set; }

    public string Password { get; set; } = null!;

    public byte[]? PassportScan { get; set; }

    public string? Note { get; set; }

    public string? Organization { get; set; }

    public string? NameScan { get; set; }

    public virtual ICollection<CrossUserRequest> CrossUserRequests { get; } = new List<CrossUserRequest>();

    public virtual ICollection<GroupVisit> GroupVisits { get; } = new List<GroupVisit>();

    public virtual ICollection<PersonalVisit> PersonalVisits { get; } = new List<PersonalVisit>();
}
