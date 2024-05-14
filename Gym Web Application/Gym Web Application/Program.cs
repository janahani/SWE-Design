using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

IServiceCollection serviceCollection = builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

builder.Services.AddScoped<ClientService>();
//builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<JobTitleService>();
builder.Services.AddScoped<ClassService>();
//builder.Services.AddScoped<AttendanceService>();
//builder.Services.AddScoped<SalesReportService>();
//builder.Services.AddScoped<ISalesReportObservable, SalesReportObservable>();




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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.Run();
