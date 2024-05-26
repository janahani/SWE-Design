using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // Add this namespace

public class SalesReportService
{
     private readonly AppDbContext _dbContext;
    private readonly ISalesReportObservable _salesReportObservable;
    private readonly EmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    

    public SalesReportService(AppDbContext dbContext, ISalesReportObservable salesReportObservable, EmailService emailService, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _salesReportObservable = salesReportObservable;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SalesReportModel> GenerateReport()
    {
        SalesReportModel newReport = GenerateReportData();
        return newReport;
    }

    public async Task NotifyObservers(SalesReportModel newReport)
    {
        ((SalesReportObservable)_salesReportObservable).LatestReport = newReport;
        await Task.Run(() => _salesReportObservable.NotifyObservers(newReport));
    }

    public async Task<SalesReportModel> GetLatestSalesReport()
    {
        return await Task.FromResult(((SalesReportObservable)_salesReportObservable).LatestReport);
    }

    private SalesReportModel GenerateReportData()
    {
        DateTime today = DateTime.Today;
        DateTime _25thDayOfMonth = new DateTime(today.Year, today.Month, 1);

        var memberships = _dbContext.Memberships
            .Where(m => m.StartDate >= _25thDayOfMonth && m.StartDate <= today)
            .ToList();

        var packages = _dbContext.Packages
            .Where(p => p.IsActivated == "Activated")
            .ToList();

        var classes = _dbContext.Classes
            .Where(c => c.ID > 0)
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

    //    public SalesReportModel GetLatestSalesReport()
    // {
    //     var latestReport = _dbContext.SalesReport
    //         .OrderByDescending(s => s.CreatedAt)
    //         .FirstOrDefault();
        
    //     // Notify observers with the latest report
    //     if (latestReport != null)
    //     {
    //         _salesReportObservable.NotifyObservers(latestReport);
    //     }

    //     return latestReport;
    // }

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
        DateTime _25thDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

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

// public int GetNewClientsJoinedOfMonth()
// {
//     DateTime startDateOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

//     DateTime endDateOfMonth = startDateOfMonth.AddMonths(1).AddDays(-1); 

//     var newClients = _dbContext.Clients
//         .Where(c => c.CreatedAt >= startDateOfMonth && c.CreatedAt <= endDateOfMonth)
//         .ToList();

//     return newClients.Count;
// }


}