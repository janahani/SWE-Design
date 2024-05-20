using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

public class PackageService
{
    private PackagesAuthoritiesFactory _packageAuthoritiesFactory;

    public PackageService(PackagesAuthoritiesFactory packageAuthoritiesFactory)
    {
        _packageAuthoritiesFactory = packageAuthoritiesFactory;
    }

    public async Task<List<PackageModel>> GetAllPackages()
    {
        return await _packageAuthoritiesFactory.GetAllPackages();
    }

    public async Task<List<PackageModel>> GetActivatedPackages()
    {
      return await _packageAuthoritiesFactory.GetActivatedPackages();
    }

    public async Task AddPackages(PackageModel addPackageRequest)
    {
       await _packageAuthoritiesFactory.AddPackages(addPackageRequest);
    }


    public async Task<PackageModel> GetPackageById(int id)
    {
        return await _packageAuthoritiesFactory.GetPackageById(id);
    }


    public async Task ActivatePackage(int id)
    {
        await _packageAuthoritiesFactory.ActivatePackage(id);
    }

    public async Task DeactivatePackage(int id)
    {
        await _packageAuthoritiesFactory.DeactivatePackage(id);
    }
}
