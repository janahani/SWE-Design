using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
namespace Gym_Web_Application.FactoryDP;
using Microsoft.EntityFrameworkCore;

public class ReceptionistAuthFactory : AuthoritiesFactory
{
   private readonly DbContextOptions<AppDbContext> _options;
   private readonly IWebHostEnvironment _hostingEnvironment;
   private readonly ISalesReportObservable _salesReportObservable;

    public ReceptionistAuthFactory(DbContextOptions<AppDbContext> options,IWebHostEnvironment hostingEnvironment,ISalesReportObservable salesReportObservable)
    {
        _options = options;
        _hostingEnvironment = hostingEnvironment;
        _salesReportObservable= salesReportObservable;
    }

    public List<AuthorityModel> createAuthorities()
    {
        using var dbContext= new AppDbContext(_options);
        List<AuthorityModel> recepAuth = new List<AuthorityModel>();

        ClientAuthoritiesFactory clientAuthoritiesFactory = new ClientAuthoritiesFactory(_options);
        EmployeeAuthoritiesFactory empAuthoritiesFactory = new EmployeeAuthoritiesFactory(_options);
        MembershipAuthoritiesFactory membershipAuthoritiesFactory = new MembershipAuthoritiesFactory(_options);
        PackagesAuthoritiesFactory packagesAuthoritiesFactory = new PackagesAuthoritiesFactory(_options);
        ClassAuthoritiesFactory classAuthoritiesFactory = new ClassAuthoritiesFactory(_options,_hostingEnvironment);
        DashboardAuthoritiesFactory dashboardAuthoritiesFactory = new DashboardAuthoritiesFactory(_options);
        ReportAuthoritiesFactory reportAuthoritiesFactory = new ReportAuthoritiesFactory(_options,_salesReportObservable);

        recepAuth.Add(clientAuthoritiesFactory);
        recepAuth.Add(classAuthoritiesFactory);
        recepAuth.Add(empAuthoritiesFactory);
        recepAuth.Add(membershipAuthoritiesFactory);
        recepAuth.Add(packagesAuthoritiesFactory);
        recepAuth.Add(dashboardAuthoritiesFactory);
        recepAuth.Add(reportAuthoritiesFactory);

        return recepAuth;

    }
}