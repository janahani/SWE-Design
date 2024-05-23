using System;
using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;
using System.Threading.Tasks;

// Mock observer implementation for testing
public class MockSalesEmployeeObserver : ISalesEmployeeObserver
{
    private readonly int _employeeID;
    private readonly EmailService _emailService; // Add this line

    public MockSalesEmployeeObserver(int employeeID, EmailService emailService) // Modify the constructor
    {
        _employeeID = employeeID;
        _emailService = emailService; // Assign the email service instance
    }

    public async void UpdateAsync(SalesReportModel latestReport)
    {
        Console.WriteLine($"Mock observer for Employee ID {_employeeID} notified of new sales report: {latestReport.CreatedAt}");
        
        // Send an email to the predefined email address
        string adminmalak = "malakhelmy2004@gmail.com";
        string subject = "New Sales Report Available";
        string body = $"Dear,<br/>A new sales report has been generated. Please check your dashboard for more details.";
        await _emailService.SendEmailAsync(adminmalak, subject, body);
    }
}
