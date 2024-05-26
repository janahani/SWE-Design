// using System;
// using System.Threading.Tasks;
// using Gym_Web_Application.Models;
// using Gym_Web_Application.ObserverDP;

// public class MockSalesEmployeeObserver : ISalesEmployeeObserver
// {
//     private readonly int _employeeID;
//     private readonly EmailService _emailService;

//     public MockSalesEmployeeObserver(int employeeID, EmailService emailService)
//     {
//         _employeeID = employeeID;
//         _emailService = emailService;
//     }

//     public async void UpdateAsync(SalesReportModel latestReport)
//     {
//         Console.WriteLine($"Mock observer for Employee ID {_employeeID} notified of new sales report: {latestReport.CreatedAt}");

//         // Send an email to the predefined email address
//         string adminmalak = "malakhelmy2004@gmail.com";
//         string subject = "New Sales Report Available";
//         string body = $"Dear,<br/>A new sales report has been generated. Please check your dashboard for more details.";
        
//         Console.WriteLine("Sending email...");
//         await _emailService.SendEmailAsync(adminmalak, subject, body);
//         Console.WriteLine("Email sent.");
//     }
// }
