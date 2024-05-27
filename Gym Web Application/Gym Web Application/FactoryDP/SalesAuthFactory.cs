using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
namespace Gym_Web_Application.FactoryDP;
using Microsoft.EntityFrameworkCore;

public class SalesAuthFactory : AuthoritiesFactory
{
   private readonly DbContextOptions<AppDbContext> _options;
   private readonly IWebHostEnvironment _hostingEnvironment;
   private readonly ISalesReportObservable _salesReportObservable;

    public SalesAuthFactory(DbContextOptions<AppDbContext> options,IWebHostEnvironment hostingEnvironment,ISalesReportObservable salesReportObservable)
    {
        _options = options;
        _hostingEnvironment = hostingEnvironment;
        _salesReportObservable= salesReportObservable;
    }

    public List<AuthorityModel> createAuthorities()
    {
        using var dbContext= new AppDbContext(_options);
        List<AuthorityModel> salesAuth = new List<AuthorityModel>();

        ClientAuthoritiesFactory clientAuthoritiesFactory = new ClientAuthoritiesFactory(_options);
        EmployeeAuthoritiesFactory empAuthoritiesFactory = new EmployeeAuthoritiesFactory(_options);
        MembershipAuthoritiesFactory membershipAuthoritiesFactory = new MembershipAuthoritiesFactory(_options);
        PackagesAuthoritiesFactory packagesAuthoritiesFactory = new PackagesAuthoritiesFactory(_options);
        ClassAuthoritiesFactory classAuthoritiesFactory = new ClassAuthoritiesFactory(_options,_hostingEnvironment);
        DashboardAuthoritiesFactory dashboardAuthoritiesFactory = new DashboardAuthoritiesFactory(_options);
        ReportAuthoritiesFactory reportAuthoritiesFactory = new ReportAuthoritiesFactory(_options,_salesReportObservable);

        salesAuth.Add(clientAuthoritiesFactory);
        salesAuth.Add(classAuthoritiesFactory);
        salesAuth.Add(empAuthoritiesFactory);
        salesAuth.Add(membershipAuthoritiesFactory);
        salesAuth.Add(packagesAuthoritiesFactory);
        salesAuth.Add(dashboardAuthoritiesFactory);
        salesAuth.Add(reportAuthoritiesFactory);

        return salesAuth;

    }
}