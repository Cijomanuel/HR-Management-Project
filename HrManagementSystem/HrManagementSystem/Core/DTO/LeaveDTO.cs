using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class LeaveDTO
    {
        public int LeaveId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public int? LeaveTypeId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Required]
        public int? TotalDays { get; set; }
        [Required]
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public string? Comment { get; set; }

        public virtual EmployeeDTO? Employee { get; set; }
        public virtual LeaveTypeDTO? LeaveType { get; set; }
    }
}
