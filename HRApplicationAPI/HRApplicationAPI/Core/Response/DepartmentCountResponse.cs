using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Core.Response
{
    public class DepartmentCountResponse
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string? DepartmentName { get; set; }

        [Required]
        public int EmployeeCount { get; set; }

    }
}
