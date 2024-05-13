using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;
using Microsoft.EntityFrameworkCore;

public class SalesReportService
{
    private readonly AppDbContext _dbContext;

    private readonly ISalesReportObservable _salesReportObservable;

    public SalesReportService(DbContextOptions<AppDbContext> options, ISalesReportObservable salesReportObservable)
    {
        _dbContext = AppDbContext.GetInstance(options);
        _salesReportObservable = salesReportObservable;
    }

    public void GenerateMonthlySalesReport()
    {
        //will generate new sales report every 25th day of month

        if (DateTime.Now.Day == 25)
        {
            SalesReportModel newReport = GenerateReport();
            //setting the latest report in the observable
            _salesReportObservable.NotifyObservers(newReport);
        }
    }


    private SalesReportModel GenerateReport()
    {

        DateTime today = DateTime.Today;
        DateTime _25thDayOfMonth = new DateTime(today.Year, today.Month, 25);

        var SalesReportData = _dbContext.SalesReport.Where(s => s.CreatedAt >= _25thDayOfMonth && s.CreatedAt <= today).ToList();
        var packages = _dbContext.Packages
                             .Where(p => p.IsActivated == "Activated")
                             .ToList();

        var classes = _dbContext.Classes
                           .Where(c => c.ID > 0) 
                           .ToList();

        var memberships = _dbContext.Memberships
                                .Where(m => m.StartDate >= _25thDayOfMonth && m.StartDate <= today)
                                .ToList();

            decimal totalRevenue = CalculateTotalRevenue(memberships, packages);
            int totalMembershipsSold = CalculateTotalMembershipsSold(memberships);
            int totalClassesAttended = CalculateTotalClassesAttended(classes);


        SalesReportModel report = new SalesReportModel
        {
            CreatedAt = today,
            TotalRevenue = totalRevenue,
            TotalMembershipsSold = totalMembershipsSold,
            TotalClassesAttended = totalClassesAttended
        };

        _dbContext.SalesReport.Add(report);
        _dbContext.SaveChanges();
        return report;


    }

    public SalesReportModel GetLatestSalesReport()
    {
        // Retrieve the latest sales report from the database
        return _dbContext.SalesReport
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefault();
    }

     private int CalculateTotalMembershipsSold(IEnumerable<MembershipModel> memberships)
        {
            return memberships.Count();
        }

        private int CalculateTotalClassesAttended(IEnumerable<ClassModel> classes)
        {
            return classes.Count();
        }

        private decimal CalculateTotalRevenue(IEnumerable<MembershipModel> memberships, IEnumerable<PackageModel> packages)
        {
        DateTime _25thDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 25);

            var latestMonthMemberships = memberships.Where(m => m.StartDate >= _25thDayOfMonth);

            decimal totalRevenue = 0;
            foreach (var membership in latestMonthMemberships)
    {
        var package = packages.FirstOrDefault(p => p.ID == membership.PackageID);
        if (package != null)
        {
            totalRevenue += (decimal)package.Price;
        }
    }
             return totalRevenue;
        }


}