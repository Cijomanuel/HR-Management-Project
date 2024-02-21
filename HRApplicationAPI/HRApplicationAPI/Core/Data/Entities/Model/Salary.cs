using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRApplicationAPI.Model
{
    public partial class Salary
    {
        public int SalaryId { get; set; }
        [Required]
        public int? EmployeeId { get; set; }
        [Required]
        public double? AnnualSalary { get; set; }
        [Required]
        public double? TaxableAmount { get; set; }
        [Required]
        public double? InsuranceAmount { get; set; }
        [Required]
        public double? InHandSalary { get; set; }
        [Required]
        public double? MealAllowance { get; set; }
        [Required]
        public double? TransportationAllowance { get; set; }
        [Required]
        public double? MedicalAllowance { get; set; }
        [Required]
        public double? OtherAllowance { get; set; }
        [Required]
        public double? Pf { get; set; }
        [Required]
        public double? TotalSalary { get; set; }
        [Required]

        public virtual Employee? Employee { get; set; }
    }
}
