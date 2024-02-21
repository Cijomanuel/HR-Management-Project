using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class Attendance
    {
        public int AttendenceId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime? Session1TimeIn { get; set; }
        public DateTime? Session1TimeOut { get; set; }
        public DateTime? Session2TimeIn { get; set; }
        public DateTime? Session2TimeOut { get; set; }
        public DateTime? Session3TimeIn { get; set; }
        public DateTime? Session3TimeOut { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public string? Remark { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
