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
            List<EmployeeModel> employees = _attendanceService.GetAllEmployees(); 
            return View(employees);
        }
    }
}