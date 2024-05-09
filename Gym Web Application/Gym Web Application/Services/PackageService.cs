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

    public IEnumerable<PackageModelDto> GetAllPackages()
    {
        return _dbContext.Packages.Select(p => new PackageModelDto
        {
            ID = p.ID,
            Title = p.Title,
            VisitsLimit = p.VisitsLimit,
            NumOfInvitations = p.NumOfInvitations,
            NumOfInbodySessions = p.NumOfInbodySessions,
            NumOfPrivateTrainingSessions = p.NumOfPrivateTrainingSessions,
            Price = p.Price
        }).ToList();
    }

    public void AddPackage(PackageModel package)
    {
        _dbContext.Packages.Add(package);
        _dbContext.SaveChanges();
    }

    public PackageModel FindById(int id)
    {
        return _dbContext.Packages.FirstOrDefault(p => p.ID == id);
    }

    public void ActivatePackage(int id)
    {
        var package = _dbContext.Packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Activated";
            _dbContext.SaveChanges();
        }
    }

    public void DeactivatePackage(int id)
    {
        var package = _dbContext.Packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Deactivated";
            _dbContext.SaveChanges();
        }
    }
}
