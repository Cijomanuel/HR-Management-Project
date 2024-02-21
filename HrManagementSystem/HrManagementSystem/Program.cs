using HrManagementSystem.Core.GuidGenerator;
using HrManagementSystem.Core.HttpClients;
using HrManagementSystem.Core.HttpClients.WebApplicationCore1.HttpClients;
using HrManagementSystem.Core.Startup;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
var client = new ApiClientConfiguration();
builder.Configuration.GetSection(nameof(ApiClientConfiguration)).Bind(client);
client.BaseUserName = builder.Configuration.GetSection("clientName").Value;
client.BasePassword = builder.Configuration.GetSection("Password").Value;

builder.Services.AddSingleton(client);

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var timeBasedKey = new TimeBasedKeyForRequestReplay();
builder.Configuration.GetSection(nameof(TimeBasedKeyForRequestReplay)).Bind(timeBasedKey);
builder.Services.AddSingleton(timeBasedKey);

builder.Services.AddScoped<IGenericHttpClient, GenericHttpClient>();
builder.Services.AddScoped<ITimeBasedGuidGenerator, TimeBasedGuidGenerator>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<GenericHttpClient>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminRolePolicy", policy =>
        policy.RequireAssertion(context => context.User.HasClaim(c =>
            (c.Type == "SuperAdminRoleClaim" || c.Type == "SuperAdminRoleClaim")
            && c.Issuer == "https://microsoftsecurity")));
    options.AddPolicy("HrRolePolicy", policy =>
        policy.RequireAssertion(context => context.User.HasClaim(c =>
            (c.Type == "HrRoleClaim" || c.Type == "HrRoleClaim")
            && c.Issuer == "https://microsoftsecurity")));
    options.AddPolicy("TeamLeadRolePolicy", policy =>
        policy.RequireAssertion(context => context.User.HasClaim(c =>
            (c.Type == "TeamLeadRoleClaim" || c.Type == "TeamLeadRoleClaim")
            && c.Issuer == "https://microsoftsecurity")));
    options.AddPolicy("DeveloperRolePolicy", policy =>
        policy.RequireAssertion(context => context.User.HasClaim(c =>
            (c.Type == "DeveloperRoleClaim" || c.Type == "DeveloperRoleClaim")
            && c.Issuer == "https://microsoftsecurity")));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Security/Error401");
});
// cookie authentication




builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.Secure = CookieSecurePolicy.SameAsRequest;
    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    //options.HandleSameSiteCookieCompatibility();
});



builder.Services.AddScoped<CookieAuthenticationEvents>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.SlidingExpiration = true;
    options.LoginPath = "/Security/Error401";
    options.EventsType = typeof(CookieAuthenticationEvents);
    options.AccessDeniedPath = "/Security/Error401";
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Security/Error401");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Security}/{action=Login}/{id?}");

app.Run();

