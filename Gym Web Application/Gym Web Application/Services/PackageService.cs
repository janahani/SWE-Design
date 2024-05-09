using Gym_Web_Application.Data;
using Gym_Web_Application.Models;

public class PackageService
{
    private readonly AppDbContext _context;

    public PackageService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PackageModelDto> GetAllPackages()
    {
        return _context.Packages.Select(p => new PackageModelDto
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
        _context.Packages.Add(package);
        _context.SaveChanges();
    }

    public PackageModel FindById(int id)
    {
        return _context.Packages.FirstOrDefault(p => p.ID == id);
    }

    public void ActivatePackage(int id)
    {
        var package = _context.Packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Activated";
            _context.SaveChanges();
        }
    }

    public void DeactivatePackage(int id)
    {
        var package = _context.Packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Deactivated";
            _context.SaveChanges();
        }
    }
}
