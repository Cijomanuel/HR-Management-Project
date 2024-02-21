using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class Designation
    {
        public Designation()
        {
            Employees = new HashSet<Employee>();
        }

        public int DesignaitionId { get; set; }
        [Required]
        public string? DesignationName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
