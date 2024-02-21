using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class ProjectMemberDTO
    {
        public int ProjectMemebersId { get; set; }
        [Required]
        public int? ProjectId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }

        public virtual EmployeeDTO? Employee { get; set; }
        public virtual ProjectDetailDTO? Project { get; set; }
    }
}
