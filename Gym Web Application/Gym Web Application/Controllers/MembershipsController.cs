using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;



public class MembershipsController : Controller
{
    private readonly ILogger<MembershipsController> _logger;
    private readonly PackageService _packageService;
    private readonly MembershipsService _membershipService;
    private readonly ClientService _clientService;


    public MembershipsController(ILogger<MembershipsController> logger, PackageService packageService, MembershipsService membershipService, ClientService clientService)
    {
        this._logger = logger;
        this._packageService = packageService;
        this._membershipService = membershipService;
        this._clientService = clientService;

    } 
 

    [HttpGet]
    public async Task<IActionResult> ViewMemberships()
    {
   if (HttpContext.Session.GetString("EmployeeEmail") == null)
    {
                return RedirectToAction("Login");
    }
        var memberships = await _membershipService.GetAllMemberships();
        _logger.LogInformation("Membership {memberships}", memberships);

        List<string> clientNames = new List<string>();
        if (memberships != null && memberships.Count > 0)
        {
            foreach (var membership in memberships)
            {
                _logger.LogInformation("Membership Client ID->>>>>>> {membership.ClientID}", membership.ClientID);

                var client = await _clientService.GetClientById(membership.ClientID);
                if (client != null)
                {
                    _logger.LogInformation("Name {client}", client);
                    clientNames.Add(client.FirstName + " " + client.LastName);
                }
                else
                {
                    clientNames.Add(" ");
                }
            }
            ViewBag.memberships = memberships;
            ViewBag.clientNames = clientNames;
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ActivateMembership(int id, int packageID)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        _logger.LogInformation("Starting ActivateMembership method for client ID {id}", id);
        PackageModel package = await _packageService.GetPackageById(packageID);
        await _membershipService.activateMembership(id, package);

        return RedirectToAction("ViewMemberships");
    }

    public async Task<ActionResult> DeleteMembership(int id)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        await _membershipService.DeleteMembership(id);
        return RedirectToAction("ViewMemberships");
    }
    public async Task<ActionResult> FreezeMembership(int id, DateTime freezeEndDate)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        await _membershipService.FreezeMembership(id, freezeEndDate);
        return RedirectToAction("ViewMemberships");
    }
    public async Task<ActionResult> UnfreezeMembership(int id)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        await _membershipService.UnfreezeMembership(id);
        return RedirectToAction("ViewMemberships");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}