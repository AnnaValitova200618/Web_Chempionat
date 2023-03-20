using System;
using System.Collections.Generic;

namespace WebApplication1.Model;

public partial class Worker
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int? IdSubdivision { get; set; }

    public int? IdDepartment { get; set; }

    public int? Code { get; set; }

    public virtual ICollection<GroupVisit> GroupVisits { get; } = new List<GroupVisit>();

    public virtual Department? IdDepartmentNavigation { get; set; }

    public virtual Subdivision? IdSubdivisionNavigation { get; set; }

    public virtual ICollection<PersonalVisit> PersonalVisits { get; } = new List<PersonalVisit>();

    public virtual ICollection<Request> Requests { get; } = new List<Request>();
}
