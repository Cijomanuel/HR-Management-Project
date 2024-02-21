using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class AttendanceDTO
    {
        public int AttendenceId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime? Session1TimeIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Session1TimeOut { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Session2TimeIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Session2TimeOut { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Session3TimeIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Session3TimeOut { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan? TotalTime { get; set; }
        public string? Remark { get; set; }

        public virtual EmployeeDTO? Employee { get; set; }
    }
}
