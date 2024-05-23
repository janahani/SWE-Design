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


public void UpdateAttendanceForNewDay()
{
    DateTime currentDate = DateTime.Today;

    bool attendanceUpdated = _dbContext.Attendance.Any(a => a.Date.Date == currentDate);

    if (!attendanceUpdated)
    {
        var employees = _dbContext.Employees.ToList();

        foreach (var employee in employees)
        {
            var attendanceRecord = _dbContext.Attendance.FirstOrDefault(a => a.EmployeeID == employee.ID && a.Date.Date == currentDate);

            if (attendanceRecord == null)
            {
                attendanceRecord = new AttendanceModel
                {
                    EmployeeID = employee.ID,
                    Attended = false,
                    Date = currentDate
                };

                _dbContext.Add(attendanceRecord);
            }
            else
            {
                attendanceRecord.Attended = false;
            }
        }

        _dbContext.SaveChanges();
    }
}

public bool HasAttended(int employeeId, DateTime date)
{
    var attendanceRecord = _dbContext.Attendance.FirstOrDefault(a => a.EmployeeID == employeeId && a.Date == date);
    return attendanceRecord != null && attendanceRecord.Attended;
}


    }