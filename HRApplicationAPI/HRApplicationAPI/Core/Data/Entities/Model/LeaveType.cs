using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class LeaveType
    {
        public LeaveType()
        {
            LeaveRecords = new HashSet<LeaveRecord>();
            Leaves = new HashSet<Leave>();
        }

        public int LeaveTypeId { get; set; }
        [Required]
        public string? LeaveName { get; set; }

        public virtual ICollection<LeaveRecord> LeaveRecords { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
    }
}
