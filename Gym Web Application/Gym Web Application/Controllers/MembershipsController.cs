using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class MembershipsController : Controller
{
    private readonly ILogger<MembershipsController> _logger;
    private readonly PackageService _packageService;
    private readonly MembershipService _membershipService;


    public MembershipsController(ILogger<MembershipsController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult ViewMemberships()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ActivateMembership(int id, int packageID)
    {
        PackageModel package =  _packageService.FindById(packageID);
        bool membershipCreated = await _membershipService.activateMembership(id, package);

        return RedirectToAction("ViewClients");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}