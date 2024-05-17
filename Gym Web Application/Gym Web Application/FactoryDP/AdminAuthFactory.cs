using Gym_Web_Application.Models;
using System.Collections.Generic;

namespace Gym_Web_Application.FactoryDP
{
    public class AdminAuthFactory : AuthoritiesFactory
    {
        private readonly ClientAuthoritiesFactory _clientAuthoritiesFactory;
        private readonly EmployeeAuthoritiesFactory _empAuthoritiesFactory;
        private readonly MembershipAuthoritiesFactory _membershipAuthoritiesFactory;
        private readonly PackagesAuthoritiesFactory _packagesAuthoritiesFactory;

        public AdminAuthFactory(ClientAuthoritiesFactory clientAuthoritiesFactory,
                                EmployeeAuthoritiesFactory empAuthoritiesFactory,
                                MembershipAuthoritiesFactory membershipAuthoritiesFactory,
                                PackagesAuthoritiesFactory packagesAuthoritiesFactory)
        {
            _clientAuthoritiesFactory = clientAuthoritiesFactory;
            _empAuthoritiesFactory = empAuthoritiesFactory;
            _membershipAuthoritiesFactory = membershipAuthoritiesFactory;
            _packagesAuthoritiesFactory = packagesAuthoritiesFactory;
        }

        public List<AuthorityModel> createAuthorities()
        {
            List<AuthorityModel> adminAuth = new List<AuthorityModel>();

            adminAuth.Add(_clientAuthoritiesFactory);
            adminAuth.Add(_empAuthoritiesFactory);
            adminAuth.Add(_membershipAuthoritiesFactory);
            adminAuth.Add(_packagesAuthoritiesFactory);

            return adminAuth;
        }
    }
}
