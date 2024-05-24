using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

public class DashboardAuthoritiesFactory : AuthorityModel
{
    private readonly DbContextOptions<AppDbContext> _options;

    public DashboardAuthoritiesFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }
    public async Task<List<ClientModel>> getTop3RecentClients()
    {
        using var _dbContext = new AppDbContext(_options);
        
        return await _dbContext.Clients
            .OrderByDescending(c => c.CreatedAt)
            .Take(3)
            .ToListAsync();
    }

    public async Task<int> getClientCount()
    {
        using var _dbContext = new AppDbContext(_options);
        
        return await _dbContext.Clients.CountAsync();
    }

    public async Task<int> getPackageCount()
    {
        using var _dbContext = new AppDbContext(_options);
        
        return await _dbContext.Packages.CountAsync();
    }

    public async Task<int> getClassCount()
    {
        using var _dbContext = new AppDbContext(_options);
        
        return await _dbContext.Classes.CountAsync();
    }

    public async Task<int> getEmployeeCount()
    {
        using var _dbContext = new AppDbContext(_options);
        
        return await _dbContext.Employees.CountAsync();
    }

    public async Task<int> getCoachCount()
    {
        using var _dbContext = new AppDbContext(_options);
        
        return await _dbContext.Employees
            .Where(e => e.JobTitleID == 4)
            .CountAsync();
    }

    public async Task<int> getActivatedMembershipCount()
    {
        using var _dbContext = new AppDbContext(_options);
        
        return await _dbContext.Memberships
            .Where(m => m.IsActivated == "Activated")
            .CountAsync();
    }
}