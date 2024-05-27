namespace Gym_Web_Application.Models;

using System.Threading.Tasks;
using System;
using global::Gym_Web_Application.ObserverDP;
using Gym_Web_Application.Data;
using System.Linq;

    public class SalesEmployee : ISalesEmployeeObserver
    {
        public EmployeeModel AssociatedEmployee { get; private set; }
        private readonly EmailService _emailService;

        public SalesEmployee(EmployeeModel employee, EmailService emailService)
        {
            AssociatedEmployee = employee;
            _emailService = emailService;
        }

        public async void UpdateAsync(SalesReportModel latestReport)
        {
            Console.WriteLine($"UpdateAsync invoked, Employee ID {AssociatedEmployee.ID} notified of new sales report: {latestReport.CreatedAt}");

            string employeeEmail = AssociatedEmployee.Email;

            string subject = "New Sales Report Available";
            string body = $"Dear {AssociatedEmployee.Name},<br/>A new sales report has been generated. Please check your dashboard for more details.";

            Console.WriteLine("Sending email...");
            await _emailService.SendEmailAsync(employeeEmail, subject, body);
            Console.WriteLine("Email sent.");
        }
    }

