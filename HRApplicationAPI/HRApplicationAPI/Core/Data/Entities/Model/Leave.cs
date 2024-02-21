using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class Leave
    {
        public int LeaveId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public int? LeaveTypeId { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public int? TotalDays { get; set; }
        [Required]
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public string? Comment { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual LeaveType? LeaveType { get; set; }
    }
}
