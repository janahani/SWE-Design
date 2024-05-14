using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;

public class EmployeesController : Controller
{
    private readonly ILogger<EmployeesController> _logger;
    private readonly EmployeeService _employeeService;
    private readonly JobTitleService _jobTitleService;



    public EmployeesController(ILogger<EmployeesController> logger, EmployeeService employeeService, JobTitleService jobTitleService)
    {
        _logger = logger;

        this._employeeService=employeeService;
        this._jobTitleService=jobTitleService;
    }

    [HttpGet]
    public async Task<IActionResult> ViewEmployees()
    {
        var employees = await _employeeService.GetAllEmployees();
        return View(employees);
    }

    [HttpGet]
    public async Task<IActionResult> AddEmployees()
    {
        EmployeeModel employee = new EmployeeModel();
        var jobTitles = await _jobTitleService.GetAllJobTitles();
        ViewBag.JobTitles=jobTitles;
        return View(employee);
    }
    

    [HttpPost]
    public async Task<IActionResult> AddEmployees(EmployeeModel addEmployeeRequest)
    {
        await _employeeService.AddEmployee(addEmployeeRequest);
        return RedirectToAction("ViewEmployees");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}