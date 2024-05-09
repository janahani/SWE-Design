using Gym_Web_Application.Models;

public class PackageService
{
    private readonly List<PackageModel> _packages;

    public PackageService()
    {
        _packages = new List<PackageModel>();
    }

    public IEnumerable<PackageModelDto> GetAllPackages()
    {
        return _packages.Select(p => new PackageModelDto
        {
            ID = p.ID,
            Title = p.Title,
            VisitsLimit = p.VisitsLimit,
            NumOfInvitations = p.NumOfInvitations,
            NumOfInbodySessions = p.NumOfInbodySessions,
            NumOfPrivateTrainingSessions = p.NumOfPrivateTrainingSessions,
            Price = p.Price
        });
    }

    public void AddPackage(PackageModel package)
    {
        _packages.Add(package);
    }

    public PackageModel FindById(int id)
    {
        return _packages.FirstOrDefault(p => p.ID == id);
    }

    public void ActivatePackage(int id)
    {
        var package = _packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Activated";
        }
    }

    public void DeactivatePackage(int id)
    {
        var package = _packages.FirstOrDefault(p => p.ID == id);
        if (package != null)
        {
            package.IsActivated = "Deactivated";
        }
    }
}

