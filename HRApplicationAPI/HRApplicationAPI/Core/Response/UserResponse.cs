using HRApplicationAPI.Model;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HRApplicationAPI.Core.Response
{
    public class UserResponse
    {

        public int EmployeeId { get; set; }
        [Required]
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
        [Required]
        public DateTime? JoiningDate { get; set; }
        public DateTime? ResignationDate { get; set; }
        public string? SupervisorId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Designation? Designation { get; set; }

        public IList<Claim> Roles { get; set; }
    }
}
