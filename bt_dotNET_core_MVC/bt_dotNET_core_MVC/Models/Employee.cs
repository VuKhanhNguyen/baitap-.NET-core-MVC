using System;
using System.Collections.Generic;

namespace bt_dotNET_core_MVC.Models;

public partial class Employee
{
    public string EmployeesId { get; set; } = null!;

    public string EmployeesName { get; set; } = null!;

    public int Age { get; set; }

    public string? Sdt { get; set; }

    public string? Position { get; set; }

    public string? Gender { get; set; }

    public decimal? Salary { get; set; }

    public string? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
