using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class DepartmentDTO
    {
        public DepartmentDTO()
        {
            Employees = new HashSet<EmployeeDTO>();
        }

        public int DepartmentId { get; set; }

        [Required]
        public string? DepartmentName { get; set; }

        public virtual ICollection<EmployeeDTO> Employees { get; set; }
    }
}
