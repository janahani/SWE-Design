using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Gym_Web_Application.FactoryDP;
using Gym_Web_Application.ObserverDP;
using Gym_Web_Application.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

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

builder.Services.AddSingleton<ClientAuthoritiesFactory>();
builder.Services.AddSingleton<ClassAuthoritiesFactory>();
builder.Services.AddSingleton<EmployeeAuthoritiesFactory>();
builder.Services.AddSingleton<PackagesAuthoritiesFactory>();
builder.Services.AddSingleton<AdminAuthFactory>();

builder.Services.AddTransient<MembershipService>();
builder.Services.AddTransient<ClientService>();
builder.Services.AddTransient<ClassService>();
builder.Services.AddTransient<PackageService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<JobTitleService>();
builder.Services.AddTransient<ClassService>();
builder.Services.AddTransient<AttendanceService>();
builder.Services.AddTransient<SalesReportService>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<ISalesReportObservable, SalesReportObservable>();
builder.Services.AddHttpContextAccessor();


// Registering MockSalesEmployeeObserver with EmailService
builder.Services.AddTransient<ISalesEmployeeObserver>(provider =>
{
    var emailService = provider.GetRequiredService<EmailService>();
    return new MockSalesEmployeeObserver(10, emailService);
});

builder.Services.AddSingleton<ISalesReportObservable>(provider =>
{
    var observable = new SalesReportObservable();

    var observers = provider.GetServices<ISalesEmployeeObserver>();
    foreach (var observer in observers)
    {
        observable.AttachObserver(observer);
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

// Test code to generate report and notify
var salesReportService = app.Services.GetRequiredService<SalesReportService>();
var testReport = salesReportService.GenerateMonthlySalesReport();

app.Run();
