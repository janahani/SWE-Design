using Gym_Web_Application.Models;
namespace Gym_Web_Application.FactoryDP;

public class AdminAuthFactory : AuthoritiesFactory
{
    public List<AuthorityModel> createAuthorities()
    {
        List<AuthorityModel> adminAuth = new List<AuthorityModel>();

        ClientAuthoritiesFactory clientAuthoritiesFactory = new ClientAuthoritiesFactory();
        EmployeeAuthoritiesFactory empAuthoritiesFactory = new EmployeeAuthoritiesFactory();
        MembershipAuthoritiesFactory membershipAuthoritiesFactory = new MembershipAuthoritiesFactory();
        PackagesAuthoritiesFactory packagesAuthoritiesFactory = new PackagesAuthoritiesFactory();

        adminAuth.Add(clientAuthoritiesFactory);
        adminAuth.Add(empAuthoritiesFactory);
        adminAuth.Add(membershipAuthoritiesFactory);
        adminAuth.Add(packagesAuthoritiesFactory);

        return adminAuth;

    }
}