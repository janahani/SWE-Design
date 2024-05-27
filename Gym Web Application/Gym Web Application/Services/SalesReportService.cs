using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // Add this namespace

public class SalesReportService
{
    private ReportAuthoritiesFactory _reportAuthoritiesFactory;

    public SalesReportService(ReportAuthoritiesFactory reportAuthoritiesFactory)
    {
        _reportAuthoritiesFactory = reportAuthoritiesFactory;
    }

    public async Task<SalesReportModel> GenerateReport()
    {
        return await _reportAuthoritiesFactory.generateReport();
    }

    public async Task NotifyObservers(SalesReportModel newReport)
    {
        await _reportAuthoritiesFactory.notifyObservers(newReport);
    }

    public async Task<SalesReportModel> GetLatestSalesReport()
    {
        return await _reportAuthoritiesFactory.getLatestSalesReport();
    }

    private SalesReportModel GenerateReportData()
    {
        return _reportAuthoritiesFactory.generateReportData();
    }

    private int CalculateTotalMembershipsSold(IEnumerable<MembershipModel> memberships)
    {
        return _reportAuthoritiesFactory.calculateTotalMembershipsSold(memberships);
    }

    private int CalculateTotalClassesAttended(IEnumerable<AssignedClassModel> classes)
    {
        return _reportAuthoritiesFactory.calculateTotalClassesAttended(classes);
    }

    private decimal CalculateTotalRevenue(IEnumerable<MembershipModel> memberships, IEnumerable<PackageModel> packages)
    {
        return _reportAuthoritiesFactory.calculateTotalRevenue(memberships, packages);
    }


}