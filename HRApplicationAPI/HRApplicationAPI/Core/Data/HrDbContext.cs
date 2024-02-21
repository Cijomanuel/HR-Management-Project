//using HRApplicationAPI.Core.Data.Identity;
using HRApplicationAPI.Core.Data.Entities.Model;
using HRApplicationAPI.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRApplicationAPI.Core.Data
{
    //Here, the models are build via Database-first method
    public class HrDbContext : IdentityDbContext
    {
        public HrDbContext(DbContextOptions<HrDbContext> options) : base(options)
        {

        }
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<AttendanceCalendar> AttendanceCalendars { get; set; } = null!;
        public virtual DbSet<ClaimStore> ClaimStores { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Designation> Designations { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Leave> Leaves { get; set; } = null!;
        public virtual DbSet<LeaveRecord> LeaveRecords { get; set; } = null!;
        public virtual DbSet<LeaveType> LeaveTypes { get; set; } = null!;
        public virtual DbSet<ProjectDetail> ProjectDetails { get; set; } = null!;
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; } = null!;
        public virtual DbSet<Salary> Salaries { get; set; } = null!;
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=UIGLT002\\SQLSERVER2019;Database=HrManagement;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);


            //});


            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.AttendenceId);

                entity.ToTable("tbl_Attendence");

                entity.Property(e => e.AttendenceId).HasColumnName("attendenceId");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.Remark)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.Session1TimeIn)
                    .HasColumnType("datetime")
                    .HasColumnName("session1TimeIn");

                entity.Property(e => e.Session1TimeOut)
                    .HasColumnType("datetime")
                    .HasColumnName("session1TimeOut");

                entity.Property(e => e.Session2TimeIn)
                    .HasColumnType("datetime")
                    .HasColumnName("session2TimeIn");

                entity.Property(e => e.Session2TimeOut)
                    .HasColumnType("datetime")
                    .HasColumnName("session2TimeOut");

                entity.Property(e => e.Session3TimeIn)
                    .HasColumnType("datetime")
                    .HasColumnName("session3TimeIn");

                entity.Property(e => e.Session3TimeOut)
                    .HasColumnType("datetime")
                    .HasColumnName("session3TimeOut");

                entity.Property(e => e.TotalTime).HasColumnName("totalTime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Attendences)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_tbl_Attendence_tbl_Employee");
            });

            modelBuilder.Entity<AttendanceCalendar>(entity =>
            {
                entity.HasKey(e => e.AttendenceCalendarId);

                entity.ToTable("tbl_AttendenceCalendar");

                entity.Property(e => e.AttendenceCalendarId).HasColumnName("attendenceCalendarId");


                entity.Property(e => e.AttendenceDate).HasColumnName("attendenceDate");

                entity.Property(e => e.HolidayReason)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("holidayReason");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("remarks");
            });

            modelBuilder.Entity<ClaimStore>(entity =>
            {
                entity.HasKey(e => e.ClaimId);

                entity.ToTable("tbl_ClaimStore");

                entity.Property(e => e.ClaimType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("tbl_Department");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("departmentName");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.HasKey(e => e.DesignaitionId);

                entity.ToTable("tbl_Designation");

                entity.Property(e => e.DesignaitionId).HasColumnName("designaitionId");

                entity.Property(e => e.DesignationName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("designationName");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("tbl_Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("bloodType");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentId");

                entity.Property(e => e.DesignationId).HasColumnName("designationId");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasColumnType("string")
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.ImageFilePath)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("imageFilePath");

                entity.Property(e => e.JoiningDate)
                    .HasColumnType("datetime")
                    .HasColumnName("joiningDate");

                entity.Property(e => e.LastName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("middleName");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("mobileNumber");

                entity.Property(e => e.ResignationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("resignationDate");

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sex");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.SupervisorId)
                    .HasMaxLength(450)
                    .HasColumnName("supervisorId");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_tbl_Employee_tbl_Department");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DesignationId)
                    .HasConstraintName("FK_tbl_Employee_tbl_Designation");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.TblEmployees)
                //    .HasForeignKey(d => d.UserId)
                //    .HasConstraintName("FK_tbl_Employee_AspNetUsers");
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.HasKey(e => e.LeaveId);

                entity.ToTable("tbl_Leave");

                entity.Property(e => e.LeaveId).HasColumnName("leaveId");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.LeaveTypeId).HasColumnName("leaveTypeId");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.TotalDays).HasColumnName("totalDays");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("status"); 

                entity.Property(e => e.Comment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comment");

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_tbl_Leave_tbl_Employee");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .HasConstraintName("FK_tbl_Leave_tbl_LeaveType");
            });

            modelBuilder.Entity<LeaveRecord>(entity =>
            {
                entity.HasKey(e => e.LeaveRecordId);

                entity.ToTable("tbl_LeaveRecord");

                entity.Property(e => e.LeaveRecordId).HasColumnName("leaveRecordId");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.LeaveTypeId).HasColumnName("leaveTypeId");

                entity.Property(e => e.TotalLeaves).HasColumnName("totalLeaves");

                entity.Property(e => e.RemainingDays).HasColumnName("remainingDays");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveRecords)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_tbl_LeaveRecord_tbl_Employee");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.LeaveRecords)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .HasConstraintName("FK_tbl_LeaveRecord_tbl_LeaveType");
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.HasKey(e => e.LeaveTypeId);

                entity.ToTable("tbl_LeaveType");

                entity.Property(e => e.LeaveTypeId).HasColumnName("leaveTypeId");

                entity.Property(e => e.LeaveName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("leaveName");
            });

            modelBuilder.Entity<ProjectDetail>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("tbl_ProjectDetails");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("endDate");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("projectName"); 

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.ProjectLead)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("projectLead");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");
            });

            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(e => e.ProjectMemebersId);

                entity.ToTable("tbl_ProjectMember");

                entity.Property(e => e.ProjectMemebersId).HasColumnName("projectMemebersId");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProjectMembers)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_tbl_ProjectMember_tbl_Employee");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectMembers)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_tbl_ProjectMember_tbl_ProjectDetails");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasKey(e => e.SalaryId);

                entity.ToTable("tbl_Salary");

                entity.Property(e => e.SalaryId).HasColumnName("salaryId");

                entity.Property(e => e.AnnualSalary).HasColumnName("annualSalary");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.MealAllowance).HasColumnName("mealAllowance");

                entity.Property(e => e.MedicalAllowance).HasColumnName("medicalAllowance");

                entity.Property(e => e.OtherAllowance).HasColumnName("otherAllowance");

                entity.Property(e => e.Pf).HasColumnName("PF");

                entity.Property(e => e.TaxableAmount).HasColumnName("taxableAmount");

                entity.Property(e => e.TransportationAllowance).HasColumnName("transportationAllowance");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_tbl_Salary_tbl_Employee");
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}
