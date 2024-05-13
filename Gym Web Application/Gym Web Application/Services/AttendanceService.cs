using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

 public class AttendanceService
    {
    private readonly AppDbContext _dbContext;
    private readonly IWebHostEnvironment _hostingEnvironment;


        public AttendanceService(AppDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;

        }

public void MarkAttendance(int employeeId, bool attended, DateTime date)
{
    var existingAttendance = _dbContext.Attendance.FirstOrDefault(a => a.EmployeeID == employeeId && a.Date == date);

    if (existingAttendance != null)
    {
        existingAttendance.Attended = attended;
    }
    else
    {
        var attendance = new AttendanceModel
        {
            EmployeeID = employeeId,
            Attended = attended,
            Date = date
        };

        _dbContext.Add(attendance);
    }

    _dbContext.SaveChanges();
}


        public List<AttendanceModel> GetAttendanceByEmployee(int employeeId)
        {
            return _dbContext.Attendance.Where(a => a.EmployeeID == employeeId).ToList();
        }

        public List<EmployeeModel> GetAllEmployees()
    {
        return _dbContext.Employees.ToList();
    }

    
}