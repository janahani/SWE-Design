namespace Gym_Web_Application.Data;

using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
     private static AppDbContext _instance;
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
     {

     }
     public static AppDbContext GetInstance(DbContextOptions<AppDbContext> options)
     {
          if (_instance == null)
          {
               _instance = new AppDbContext(options);
          }
          return _instance;
     }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<SalesReportModel>()
        .Property(s => s.TotalRevenue)
        .HasColumnType("decimal(18, 2)");


    base.OnModelCreating(modelBuilder);
}
     public DbSet<ClientModel> Clients { get; set; }
     public DbSet<EmployeeModel> Employees { get; set; }
     public DbSet<PackageModel> Packages { get; set; }
     public DbSet<SalesReportModel> SalesReport { get; set; }
     public DbSet<ClassModel> Classes { get; set; }
     public DbSet<MembershipModel> Memberships { get; set; }
     public DbSet<AssignedClassModel> AssignedClasses { get; set; }
     public DbSet<ReservedClassModel> ReservedClasses { get; set; }
     public DbSet<ClassDaysModel> ClassDays { get; set; }
     public DbSet<AuthorityModel> Authorities { get; set; }
     public DbSet<AttendanceModel> Attendance { get; set; }
     public DbSet<EmployeeAuthorityModel> EmployeeAuthorities { get; set; }
     public DbSet<JobTitlesModel> JobTitles { get; set; }
     public DbSet<ScheduledUnfreezeModel> ScheduledUnfreeze { get; set; }


}