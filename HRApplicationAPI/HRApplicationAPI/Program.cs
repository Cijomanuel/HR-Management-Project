using HRApplicationAPI.Core.Data;
using HRApplicationAPI.Core.Data.Repositories;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Core.Data.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<HrDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<HrDbContext>();


builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();
builder.Services.AddScoped<IAttendanceCalendarRepository, AttendanceCalendarRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<ILeaveRecordRepository, LeaveRecordRepository>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
builder.Services.AddScoped<IProjectDetailRepository, ProjectDetailRepository>();
builder.Services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
builder.Services.AddScoped<IUserValidateRepository, UserValidateRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

//builder.Services.AddAuthentication("BasicAuthentication")
//    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = "BasicAuthentication";
    opts.DefaultChallengeScheme = "BasicAuthentication";
    opts.DefaultScheme = "BasicAuthentication";
    opts.AddScheme<BasicAuthenticationHandler>("BasicAuthentication", null);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
