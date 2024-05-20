using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers;


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger, EmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            EmployeeModel employee = new EmployeeModel();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> LoginEmployee(EmployeeModel model)
        {
            var employee = await _employeeService.GetEmployeeByEmail(model.Email);
            if(employee != null)
            {
                if (await _employeeService.ValidateEmployeeLogin(model.Email, model.Password))
                {
                    string JobTitleID = employee.JobTitleID.ToString();
                    HttpContext.Session.SetString("EmployeeEmail", employee.Email);
                    HttpContext.Session.SetString("EmployeeJobTitleID", JobTitleID);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid password.";
                    return View("Login");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Email not found.";
                return View("Login");
            }
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("EmployeeEmail") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("EmployeeEmail");
            HttpContext.Session.Remove("EmployeeJobTitleID");
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }