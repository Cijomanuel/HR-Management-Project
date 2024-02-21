using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class ProjectDetailDTO
    {
        public ProjectDetailDTO()
        {
            ProjectMembers = new HashSet<ProjectMemberDTO>();
        }

        public int ProjectId { get; set; }
        [Required]
        public string? ProjectName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Required]
        public string? ProjectLead { get; set; }
        public string? Remark { get; set; }

        public virtual ICollection<ProjectMemberDTO> ProjectMembers { get; set; }
    }
}
