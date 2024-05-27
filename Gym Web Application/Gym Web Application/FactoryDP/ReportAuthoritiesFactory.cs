using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

public class ReportAuthoritiesFactory : AuthorityModel,IReportAuthority
{
    private readonly DbContextOptions<AppDbContext> _options;
    private readonly ISalesReportObservable _salesReportObservable;

    public ReportAuthoritiesFactory(DbContextOptions<AppDbContext> options, ISalesReportObservable salesReportObservable)
    {
        _options = options;
        _salesReportObservable = salesReportObservable;
    }

    public async Task<SalesReportModel> generateReport()
    {
        SalesReportModel newReport = generateReportData();
        return newReport;
    }

    public async Task notifyObservers(SalesReportModel newReport)
    {
        ((SalesReportObservable)_salesReportObservable).LatestReport = newReport;
        await Task.Run(() => _salesReportObservable.NotifyObservers(newReport));
    }

    public async Task<SalesReportModel> getLatestSalesReport()
    {
        return await Task.FromResult(((SalesReportObservable)_salesReportObservable).LatestReport);
    }

    public SalesReportModel generateReportData()
    {
        using var _dbContext = new AppDbContext(_options);
        DateTime today = DateTime.Today;
        DateTime _25thDayOfMonth = new DateTime(today.Year, today.Month, 1);

        var memberships = _dbContext.Memberships
            .Where(m => m.StartDate >= _25thDayOfMonth && m.StartDate <= today)
            .ToList();

        var packages = _dbContext.Packages
            .Where(p => p.IsActivated == "Activated")
            .ToList();

        var classes = _dbContext.AssignedClasses
            .Where(c => c.ID > 0)
            .ToList();

        decimal totalRevenue = calculateTotalRevenue(memberships, packages);
        int totalMembershipsSold = calculateTotalMembershipsSold(memberships);
        int totalClassesAttended = calculateTotalClassesAttended(classes);

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

    public int calculateTotalMembershipsSold(IEnumerable<MembershipModel> memberships)
    {
        return memberships.Count();
    }

    public int calculateTotalClassesAttended(IEnumerable<AssignedClassModel> classes)
    {
        return classes.Count();
    }

    public decimal calculateTotalRevenue(IEnumerable<MembershipModel> memberships, IEnumerable<PackageModel> packages)
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

}