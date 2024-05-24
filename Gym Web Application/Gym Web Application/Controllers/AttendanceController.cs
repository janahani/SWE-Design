using Gym_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Gym_Web_Application.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly AttendanceService _attendanceService;

        public AttendanceController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }



[HttpPost]
public IActionResult MarkAttendance(Dictionary<int, bool> attendedEmployees)
{
    if (HttpContext.Session.GetString("EmployeeEmail") == null)
    {
         return RedirectToAction("Login");
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
            
            List<EmployeeModel> employees = _attendanceService.GetAllEmployees(); 
            return View(employees);
        }
    }
}