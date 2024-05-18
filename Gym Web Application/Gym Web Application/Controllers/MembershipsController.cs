using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class MembershipsController : Controller
{
    private readonly ILogger<MembershipsController> _logger;
    private readonly PackageService _packageService;
    private readonly MembershipService _membershipService;


    public MembershipsController(ILogger<MembershipsController> logger, PackageService packageService, MembershipService membershipService)
    {
        _logger = logger;
        this._packageService = packageService;
        this._membershipService = membershipService;

    }

    [HttpGet]
    public IActionResult ViewMemberships()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ActivateMembership(int id, int packageID)
    {        
        _logger.LogInformation("Starting ActivateMembership method for package ID {packageID}", packageID);
        _logger.LogInformation("Starting ActivateMembership method for client ID {id}", id);
        PackageModel package = await _packageService.GetPackageById(packageID);
        await _membershipService.activateMembership(id, package);

        return RedirectToAction("ViewClients");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}