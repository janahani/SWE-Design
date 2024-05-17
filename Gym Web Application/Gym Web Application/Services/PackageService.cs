using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class PackageService
{
    private readonly AppDbContext _dbContext;

    public PackageService(DbContextOptions<AppDbContext> options)
    {
        _dbContext = AppDbContext.GetInstance(options);
    }

    public async Task<List<PackageModel>> GetAllPackages()
    {
        return await _dbContext.Packages.ToListAsync();
    }

    public async Task<List<PackageModel>> GetActivatedPackages()
    {
        return await _dbContext.Packages
             .Where(p => p.IsActivated == "Activated")
             .ToListAsync();

    }

    public async Task AddPackages(PackageModel addPackageRequest)
    {
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


    public PackageModel FindById(int id)
    {
        return _dbContext.Packages.FirstOrDefault(p => p.ID == id);
    }

    public async Task ActivatePackage(int id)
    {
        var package = _dbContext.Packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Activated";
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeactivatePackage(int id)
    {
        var package = _dbContext.Packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Deactivated";
            await _dbContext.SaveChangesAsync();
        }
    }
}
