using Gym_Web_Application.Models;

public class DashboardService
{
    private DashboardAuthoritiesFactory _dashboardAuthoritiesFactory;

    public DashboardService(DashboardAuthoritiesFactory dashboardAuthoritiesFactory)
    {
        _dashboardAuthoritiesFactory= dashboardAuthoritiesFactory;
    }
    public async Task<List<ClientModel>> GetTop3RecentClients()
    {
        return await _dashboardAuthoritiesFactory.getTop3RecentClients();
    }

    public async Task<int> GetPackageCount()
    {
        return await _dashboardAuthoritiesFactory.getPackageCount();
    }

    public async Task<int> GetClientCount()
    {
        return await _dashboardAuthoritiesFactory.getClientCount();
    }

    public async Task<int> GetEmployeeCount()
    {
        return await _dashboardAuthoritiesFactory.getEmployeeCount();
    }

    public async Task<int> GetCoachCount()
    {
        return await _dashboardAuthoritiesFactory.getCoachCount();
    }

    public async Task<int> GetClassCount()
    {
        return await _dashboardAuthoritiesFactory.getClassCount();
    }
    
    public async Task<int> GetActivatedMembershipCount()
    {
        return await _dashboardAuthoritiesFactory.getActivatedMembershipCount();
    }
}