using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class ClientsController : Controller
{
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(ILogger<ClientsController> logger)
    {
        _logger = logger;
    }

    public IActionResult ViewClients()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}