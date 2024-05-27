using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class ClassesController : Controller
{
    private readonly ILogger<ClassesController> _logger;
    private readonly ClassService _classService;
    private readonly EmployeeService _employeeService;
    private int? _jobTitleId;


    public ClassesController(ILogger<ClassesController> logger, ClassService classService, EmployeeService employeeService)
    {
        _logger = logger;
        _classService = classService;
        _employeeService = employeeService;
    }

     private void SetJobTitleId()
        {
            if (HttpContext.Session.GetString("EmployeeJobTitleID") != null)
            {
                _jobTitleId = Convert.ToInt32(HttpContext.Session.GetString("EmployeeJobTitleID"));
            }
            else
            {
                _jobTitleId = null;
            }
        }

    [HttpGet]
    public IActionResult AddClasses()
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }

            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddClass(ClassModel classModel, IFormFile ImageFile, List<string> SelectedDays)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            
            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        await _classService.AddClass(classModel, ImageFile, SelectedDays);
        return RedirectToAction("AddClasses");
    }

    [HttpGet]
    public async Task<IActionResult> ViewClasses()
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
        var classes = await _classService.GetAllClasses();
        var classDays = await _classService.GetAllClassDays();
        ViewData["ClassDays"] = classDays;
        return View(classes);
    }

    [HttpGet]
    public async Task<IActionResult> EditClasses(int id)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            
            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        var classObject = await _classService.GetClassById(id);
        var classDays = await _classService.GetClassDaysByClassId(id);
        ViewData["ClassDays"] = classDays.Select(cd => cd.Days).ToList();
        return View(classObject);
    }

    [HttpPost]
    public async Task<IActionResult> EditClasses(ClassModel updatedClass, List<string> SelectedDays)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            
            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        await _classService.UpdateClass(updatedClass);

        var existingClassDays = await _classService.GetClassDaysByClassId(updatedClass.ID);

        if (SelectedDays != null)
        {
            foreach (var day in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
            {
                var dayString = day.ToString();
                if (SelectedDays.Contains(dayString))
                {
                    if (!existingClassDays.Any(cd => cd.Days == dayString))
                    {
                        await _classService.AddClassDay(new ClassDaysModel { ClassID = updatedClass.ID, Days = dayString });
                    }
                }
                else
                {
                    var classDayToRemove = existingClassDays.FirstOrDefault(cd => cd.Days == dayString);
                    if (classDayToRemove != null)
                    {
                        await _classService.RemoveClassDay(classDayToRemove.ID);
                    }
                }
            }
        }

        return RedirectToAction("ViewClasses");
    }


    [HttpGet]
    public async Task<IActionResult> AssignClasses(int classId)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            
            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        var assignedClass = new AssignedClassModel();
        var coaches = await _employeeService.GetAllCoaches();
        var classDays = await _classService.GetClassDays(classId);
        var className = await _classService.GetClassName(classId);

        ViewBag.ClassID = classId;
        ViewBag.ClassName = className;
        ViewBag.Coaches = coaches;
        ViewBag.ClassDays = classDays;

        return View(assignedClass);
    }


    [HttpGet]
    public async Task<IActionResult> GetClassDays(int classId)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            
            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        var classDays = await _classService.GetClassDays(classId);
        return PartialView("GetClassDays", classDays);
    }

    [HttpPost]
    public async Task<IActionResult> AssignClasses(AssignedClassModel assignedClassRequest)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            
            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        await _classService.AddAssignedClass(assignedClassRequest);
        return RedirectToAction("AssignClasses");
    }

    [HttpGet]
    public async Task<IActionResult> ReserveClasses(int clientId)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            
            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        var assignedClasses = await _classService.GetAllAssignedClasses();
        ViewBag.ClientId = clientId;

        return View(assignedClasses);
    }
    [HttpPost]
    public async Task<IActionResult> ReserveClass(int AssignedClassID, int ClientID, int CoachID)
    {
         if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            
            SetJobTitleId(); // Set jobTitleId value
            if (_jobTitleId == 3)
            {
                return RedirectToAction("ViewClasses");
            }
        await _classService.AddReservedClass(AssignedClassID, ClientID, CoachID);
        return RedirectToAction("ReserveClasses");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}