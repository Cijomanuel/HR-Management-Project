using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class DesignationDTO
    {
        public DesignationDTO()
        {
            Employees = new HashSet<EmployeeDTO>();
        }

        public int DesignaitionId { get; set; }
        [Required]
        public string? DesignationName { get; set; }

        public virtual ICollection<EmployeeDTO> Employees { get; set; }
    }
}
