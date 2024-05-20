using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym_Web_Application.Controllers;

public class PackagesController : Controller
{
    private readonly ILogger<PackagesController> _logger;
    private readonly PackageService _packageService;
    private readonly ClientService _clientService;

    public PackagesController(ILogger<PackagesController> logger, PackageService packageService, ClientService clientService )
    {
        _logger = logger;
        this._clientService = clientService;
        this._packageService=packageService;
    }

    [HttpGet]
    public async Task<IActionResult> ViewPackages()
    {

         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        
        var packages = await _packageService.GetAllPackages();
        return View(packages);
    }

    [HttpGet]
    public async Task<IActionResult> ViewActivatedPackages(int clientID)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        var client = await _clientService.GetClientById(clientID);
         _logger.LogInformation("output this rn {client}",client);
        var packages = await _packageService.GetActivatedPackages();
        ViewBag.Client = client;
        ViewBag.Packages = packages; 
        return View();
    }
    
    [HttpGet]
     public IActionResult AddPackages()
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPackages(PackageModel addPackageRequest)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        await _packageService.AddPackages(addPackageRequest);
        return RedirectToAction("ViewPackages");
    }

    [HttpPost]
    public async Task<IActionResult> Activation(string activationStatus, int packageId)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        if (activationStatus == "Activated")
        {
            await _packageService.DeactivatePackage(packageId);      
        }
        else if (activationStatus == "Not Activated")
        {
             await _packageService.ActivatePackage(packageId);

        }

         
         return RedirectToAction("ViewPackages");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}