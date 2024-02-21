using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class LeaveRecord
    {
        public int LeaveRecordId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public int? LeaveTypeId { get; set; }
        [Required]
        public int? TotalLeaves { get; set; }
        public int? RemainingDays { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual LeaveType? LeaveType { get; set; }
    }
}
