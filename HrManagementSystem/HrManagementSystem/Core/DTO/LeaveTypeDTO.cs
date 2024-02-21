using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class LeaveTypeDTO
    {
        public LeaveTypeDTO()
        {
            LeaveRecords = new HashSet<LeaveRecordDTO>();
            Leaves = new HashSet<LeaveDTO>();
        }

        public int LeaveTypeId { get; set; }
        [Required]
        public string? LeaveName { get; set; }

        public virtual ICollection<LeaveRecordDTO> LeaveRecords { get; set; }
        public virtual ICollection<LeaveDTO> Leaves { get; set; }
    }
}
