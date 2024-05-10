using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class MembershipsController : Controller
{
    private readonly ILogger<MembershipsController> _logger;

    public MembershipsController(ILogger<MembershipsController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult ViewMemberships()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}