using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }

        [Required]
        public string? DepartmentName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
