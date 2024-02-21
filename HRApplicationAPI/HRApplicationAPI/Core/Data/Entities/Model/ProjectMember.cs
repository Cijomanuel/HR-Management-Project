using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class ProjectMember
    {
        public int ProjectMemebersId { get; set; }
        [Required]
        public int? ProjectId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual ProjectDetail? Project { get; set; }
    }
}
