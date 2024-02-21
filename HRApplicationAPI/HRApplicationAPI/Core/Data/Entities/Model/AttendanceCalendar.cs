using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class AttendanceCalendar
    {
        public int AttendenceCalendarId { get; set; }

        [Required]
        public DateTime AttendenceDate { get; set; }
        [Required]
        public string? HolidayReason { get; set; }
        public string? Remarks { get; set; }
    }
}
