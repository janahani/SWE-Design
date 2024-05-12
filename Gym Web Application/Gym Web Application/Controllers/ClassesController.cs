using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class ClassesController : Controller
{
    private readonly ILogger<ClassesController> _logger;
    private readonly ClassService _classService;

    public ClassesController(ILogger<ClassesController> logger, ClassService classService)
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
    public async Task<IActionResult> AddClasses(ClassModel classModel, IFormFile ImageFile, List<string> SelectedDays)
    {
        await _classService.AddClass(classModel, ImageFile, SelectedDays);
        return RedirectToAction("AddClasses");
    }

    [HttpGet]
    public async Task<IActionResult> ViewClasses()
    {
        var classes = await _classService.GetAllClasses();
        return View(classes);
    }

    [HttpGet]
    public async Task<IActionResult> AssignClasses()
    {
        var assignedClass = new AssignedClassModel();
        var classes = await _classService.GetAllClasses();

        ViewBag.Classes = classes;

        return View(assignedClass);
    }

    [HttpGet]
    public async Task<IActionResult> GetClassDays(int classId)
    {
        var classDays = await _classService.GetClassDays(classId);
        return Json(classDays);
    }

    [HttpPost]
    public async Task<IActionResult> AssignClasses(AssignedClassModel assignedClassRequest)
    {
        await _classService.AddAssignedClasses(assignedClassRequest);
        return RedirectToAction("AssignClasses");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}