using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class Employee
    {
        public Employee()
        {
            Attendences = new HashSet<Attendance>();
            LeaveRecords = new HashSet<LeaveRecord>();
            Leaves = new HashSet<Leave>();
            ProjectMembers = new HashSet<ProjectMember>();
            Salaries = new HashSet<Salary>();
        }

        public int EmployeeId { get; set; }
        public string? UserId { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int? DesignationId { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        [Required]
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

        public virtual Department? Department { get; set; }
        public virtual Designation? Designation { get; set; }
        //public virtual AspNetUser? User { get; set; }
        public virtual ICollection<Attendance> Attendences { get; set; }
        public virtual ICollection<LeaveRecord> LeaveRecords { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
