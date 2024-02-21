using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Core.Request
{
    public class UserCreationRequest
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public int? DesignationId { get; set; }
        [Required]
        public string? DepartmentName { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? MobileNumber { get; set; }
        [Required]
        public string? Sex { get; set; }


    }
}
