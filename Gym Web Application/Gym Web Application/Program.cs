using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Gym_Web_Application.FactoryDP;
using Gym_Web_Application.ObserverDP;
using Gym_Web_Application.Models;
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

builder.Services.AddSingleton(sp => new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).Options);

IServiceCollection serviceCollection = builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

builder.Services.AddSingleton<DashboardAuthoritiesFactory>();
builder.Services.AddSingleton<ClientAuthoritiesFactory>();
builder.Services.AddSingleton<ClassAuthoritiesFactory>();
builder.Services.AddSingleton<EmployeeAuthoritiesFactory>();
builder.Services.AddSingleton<PackagesAuthoritiesFactory>();
builder.Services.AddSingleton<AdminAuthFactory>();


builder.Services.AddTransient<MembershipService>();
builder.Services.AddTransient<DashboardService>();
builder.Services.AddTransient<ClientService>();
builder.Services.AddTransient<ClassService>();
builder.Services.AddTransient<EmailService, MockEmailService>();
builder.Services.AddTransient<PackageService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<JobTitleService>();
builder.Services.AddTransient<ClassService>();
builder.Services.AddTransient<AttendanceService>();
builder.Services.AddTransient<SalesReportService>();
builder.Services.AddTransient<ISalesReportObservable, SalesReportObservable>();


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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

// Test code
SalesReportObservable salesReportObservable = new SalesReportObservable();

var configuration = builder.Configuration;

            // Create an instance of EmailService
var emailService = new EmailService(configuration); // Provide the necessary configuration
    
// Create and attach mock observers for testing
salesReportObservable.AttachObserver(new MockSalesEmployeeObserver(10, emailService));
salesReportObservable.AttachObserver(new MockSalesEmployeeObserver(20, emailService));
salesReportObservable.AttachObserver(new MockSalesEmployeeObserver(30, emailService));

// Generate a test report and notify observers
SalesReportModel testReport = new SalesReportModel
{
    CreatedAt = DateTime.Now,
    TotalRevenue = 1000,
    TotalMembershipsSold = 50,
    TotalClassesAttended = 200
};
salesReportObservable.LatestReport = testReport;

app.Run();
