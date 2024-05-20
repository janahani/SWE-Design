using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
namespace Gym_Web_Application.FactoryDP;
using Microsoft.EntityFrameworkCore;

public class AdminAuthFactory : AuthoritiesFactory
{
   private readonly DbContextOptions<AppDbContext> _options;
   private readonly IWebHostEnvironment _hostingEnvironment;

    public AdminAuthFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public List<AuthorityModel> createAuthorities()
    {
        using var dbContext= new AppDbContext(_options);
        List<AuthorityModel> adminAuth = new List<AuthorityModel>();

        ClientAuthoritiesFactory clientAuthoritiesFactory = new ClientAuthoritiesFactory(_options);
        EmployeeAuthoritiesFactory empAuthoritiesFactory = new EmployeeAuthoritiesFactory(_options);
        // MembershipAuthoritiesFactory membershipAuthoritiesFactory = new MembershipAuthoritiesFactory();
        // PackagesAuthoritiesFactory packagesAuthoritiesFactory = new PackagesAuthoritiesFactory();
        ClassAuthoritiesFactory classAuthoritiesFactory = new ClassAuthoritiesFactory(_options,_hostingEnvironment);

        adminAuth.Add(clientAuthoritiesFactory);
        adminAuth.Add(classAuthoritiesFactory);
        adminAuth.Add(empAuthoritiesFactory);
        // adminAuth.Add(membershipAuthoritiesFactory);
        // adminAuth.Add(packagesAuthoritiesFactory);

        return adminAuth;

    }
}