﻿//using HRApplicationAPI.Model;
//using System.ComponentModel.DataAnnotations;

//namespace HRApplicationAPI.Core.Data.Identity
//{
//    public partial class AspNetUser
//    {
//        public AspNetUser()
//        {
//            //AspNetUserClaims = new HashSet<AspNetUserClaim>();
//            //AspNetUserLogins = new HashSet<AspNetUserLogin>();
//            //AspNetUserTokens = new HashSet<AspNetUserToken>();
//            Employees = new HashSet<Employee>();
//            //Roles = new HashSet<AspNetRole>();
//        }
//        [Key]
//        public string Id { get; set; } = null!;
//        public string? UserName { get; set; }
//        public string? NormalizedUserName { get; set; }
//        public string? Email { get; set; }
//        public string? NormalizedEmail { get; set; }
//        public bool EmailConfirmed { get; set; }
//        public string? PasswordHash { get; set; }
//        public string? SecurityStamp { get; set; }
//        public string? ConcurrencyStamp { get; set; }
//        public string? PhoneNumber { get; set; }
//        public bool PhoneNumberConfirmed { get; set; }
//        public bool TwoFactorEnabled { get; set; }
//        public DateTimeOffset? LockoutEnd { get; set; }
//        public bool LockoutEnabled { get; set; }
//        public int AccessFailedCount { get; set; }

//        public virtual ICollection<Employee> Employees { get; set; }

//    }
//}
