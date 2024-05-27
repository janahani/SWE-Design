using Gym_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Gym_Web_Application.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly AttendanceService _attendanceService;
        private int? _jobTitleId;


        public AttendanceController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
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



[HttpPost]
public IActionResult MarkAttendance(Dictionary<int, bool> attendedEmployees)
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

    if (attendedEmployees != null)
    {
        foreach (var (employeeId, attended) in attendedEmployees)
        {
            _attendanceService.MarkAttendance(employeeId, attended, DateTime.Today);
        }
    }

    return RedirectToAction("ViewAttendance");

}


        [HttpGet]
        public IActionResult ViewAttendance()
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
            List<EmployeeModel> employees = _attendanceService.GetAllEmployees(); 
            return View(employees);
        }
    }
}