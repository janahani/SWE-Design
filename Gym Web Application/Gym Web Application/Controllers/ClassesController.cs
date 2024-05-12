using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class ClassesController : Controller
{
    private readonly ILogger<ClassesController> _logger;
    private readonly ClassService _classService;

    public ClassesController(ILogger<ClassesController> logger,ClassService classService)
    {
        _logger = logger;
        _classService = classService;
    }

    [HttpGet]
    public IActionResult AddClasses()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddClass(ClassModel classModel, IFormFile ImageFile, List<string> SelectedDays)
    {
        await _classService.AddClass(classModel, ImageFile, SelectedDays);
        return RedirectToAction("AddClasses");
    }

    [HttpGet]
    public IActionResult ViewClasses()
    {
        return View();
    }

    [HttpGet]
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