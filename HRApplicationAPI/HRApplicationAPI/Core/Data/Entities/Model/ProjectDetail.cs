using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class ProjectDetail
    {
        public ProjectDetail()
        {
            ProjectMembers = new HashSet<ProjectMember>();
        }

        public int ProjectId { get; set; }
        [Required]
        public string? ProjectName { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public string? ProjectLead { get; set; }
        public string? Remark { get; set; }

        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }
    }
}
