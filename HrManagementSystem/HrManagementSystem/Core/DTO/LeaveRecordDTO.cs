using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class LeaveRecordDTO
    {
        public int LeaveRecordId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public int? LeaveTypeId { get; set; }
        [Required]
        public int? TotalLeaves { get; set; }
        public int? RemainingDays { get; set; }

        public virtual EmployeeDTO? Employee { get; set; }
        public virtual LeaveTypeDTO? LeaveType { get; set; }
    }
}
