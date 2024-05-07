using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class PackagesController : Controller
{
    private readonly ILogger<PackagesController> _logger;

    public PackagesController(ILogger<PackagesController> logger)
    {
        _logger = logger;
    }

    public IActionResult ViewPackages()
    {
        return View();
    }

    public IActionResult AddPackages()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}