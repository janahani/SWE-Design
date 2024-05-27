using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Gym_Web_Application.FactoryDP;
using Gym_Web_Application.ObserverDP;
using Gym_Web_Application.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(sp => new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).Options);

builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

builder.Services.AddSingleton<DashboardAuthoritiesFactory>();
builder.Services.AddSingleton<ClientAuthoritiesFactory>();
builder.Services.AddSingleton<ClassAuthoritiesFactory>();
builder.Services.AddSingleton<EmployeeAuthoritiesFactory>();
builder.Services.AddSingleton<MembershipAuthoritiesFactory>();

builder.Services.AddSingleton<PackagesAuthoritiesFactory>();
builder.Services.AddSingleton<AdminAuthFactory>();

builder.Services.AddTransient<MembershipService>();
builder.Services.AddTransient<DashboardService>();
builder.Services.AddTransient<ClientService>();
builder.Services.AddTransient<ClassService>();
builder.Services.AddTransient<PackageService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<JobTitleService>();
builder.Services.AddTransient<ClassService>();
builder.Services.AddTransient<AttendanceService>();
builder.Services.AddTransient<SalesReportService>();
builder.Services.AddTransient<EmailService>();
// builder.Services.AddTransient<ISalesReportObservable, SalesReportObservable>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ISalesReportObservable, SalesReportObservable>(provider =>
{
    var observable = new SalesReportObservable();

    using var scope = provider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var emailService = provider.GetRequiredService<EmailService>();

    var employees = dbContext.Employees.Where(e => e.JobTitleID == 3).ToList();

    foreach (var employee in employees)
    {
        var salesEmployee = new SalesEmployee(employee, emailService);
        observable.AttachObserver(salesEmployee);
    }

    return observable;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
