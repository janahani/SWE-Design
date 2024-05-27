using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

public class PackagesAuthoritiesFactory : AuthorityModel,IGetAuthority<PackageModel>
{

    private readonly DbContextOptions<AppDbContext> _options;

    public PackagesAuthoritiesFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

     public async Task<List<PackageModel>> getAll()
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Packages.ToListAsync();
    }

    public async Task<List<PackageModel>> GetActivatedPackages()
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Packages
             .Where(p => p.IsActivated == "Activated")
             .ToListAsync();

    }

    public async Task AddPackages(PackageModel addPackageRequest)
    {
        using var _dbContext = new AppDbContext(_options);
        var package = new PackageModel()
        {
            Title = addPackageRequest.Title,
            NumOfInbodySessions = addPackageRequest.NumOfInbodySessions,
            NumOfMonths = addPackageRequest.NumOfMonths,
            NumOfPrivateTrainingSessions = addPackageRequest.NumOfPrivateTrainingSessions,
            NumOfInvitations = addPackageRequest.NumOfInvitations,
            FreezeLimit = addPackageRequest.FreezeLimit,
            VisitsLimit = addPackageRequest.VisitsLimit,
            Price = addPackageRequest.Price,
            IsActivated = addPackageRequest.IsActivated
        };

        await _dbContext.Packages.AddAsync(package);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<PackageModel> GetPackageById(int id)
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Packages.FirstOrDefaultAsync(p => p.ID == id);
        
    }


    public async Task ActivatePackage(int id)
    {
        using var _dbContext = new AppDbContext(_options);
        var package = _dbContext.Packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Activated";
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeactivatePackage(int id)
    {
        using var _dbContext = new AppDbContext(_options);
        var package = _dbContext.Packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Deactivated";
            await _dbContext.SaveChangesAsync();
        }
    }
}