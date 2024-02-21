using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class EmployeeDTO
    {
        public EmployeeDTO()
        {
            Attendences = new HashSet<AttendanceDTO>();
            LeaveRecords = new HashSet<LeaveRecordDTO>();
            Leaves = new HashSet<LeaveDTO>();
            ProjectMembers = new HashSet<ProjectMemberDTO>();
            Salaries = new HashSet<SalaryDTO>();
        }

        public int EmployeeId { get; set; }
        public string? UserId { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int? DesignationId { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        public string? Status { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }
        public string? BloodType { get; set; }
        public string? Sex { get; set; }
        public int? Age { get; set; }
        public string? ImageFilePath { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? ResignationDate { get; set; }
        public string? SupervisorId { get; set; }

        public virtual DepartmentDTO? Department { get; set; }
        public virtual DesignationDTO? Designation { get; set; }
        //public virtual AspNetUser? User { get; set; }
        public virtual ICollection<AttendanceDTO> Attendences { get; set; }
        public virtual ICollection<LeaveRecordDTO> LeaveRecords { get; set; }
        public virtual ICollection<LeaveDTO> Leaves { get; set; }
        public virtual ICollection<ProjectMemberDTO> ProjectMembers { get; set; }
        public virtual ICollection<SalaryDTO> Salaries { get; set; }
    }
}
