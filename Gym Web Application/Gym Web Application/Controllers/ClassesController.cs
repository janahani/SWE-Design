using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class ClassesController : Controller
{
    private readonly ILogger<ClassesController> _logger;

    public ClassesController(ILogger<ClassesController> logger)
    {
        _logger = logger;
    }

    public IActionResult AddClasses()
    {
        return View();
    }

    public IActionResult ViewClasses()
    {
        return View();
    }

        public IActionResult AssignClasses()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}