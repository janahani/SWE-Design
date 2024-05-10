using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class AttendanceController : Controller
{
    private readonly ILogger<AttendanceController> _logger;

    public AttendanceController(ILogger<AttendanceController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult ViewAttendance()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}