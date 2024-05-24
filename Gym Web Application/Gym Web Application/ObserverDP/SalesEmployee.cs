namespace Gym_Web_Application.Models;

using System.Threading.Tasks;
using System;
using global::Gym_Web_Application.ObserverDP;

public class SalesEmployee : ISalesEmployeeObserver
{
    private readonly EmployeeModel Sales_Employee;
    private readonly EmailService _emailService;

    public int EmployeeID { get; set; }



    public SalesEmployee(EmployeeModel employee, EmailService emailService)
    {
        Sales_Employee = employee; 
        _emailService = emailService;

    }

    public async void UpdateAsync(SalesReportModel latestReport)

    {
            Console.WriteLine("UpdateAsync invoked.");

        string adminmalak = "malakhelmy2004@gmail.com";
        // //im assuming en el sales report job title id is 4 lehad ma tables are mapped
        // if (Sales_Employee.JobTitleID == 4)
        //     {
                string subject = "New Sales Report Available";
                string body = $"Dear,<br/>A new sales report has been generated. Please check your dashboard for more details.";
                await _emailService.SendEmailAsync(adminmalak, subject, body);
                
                Console.WriteLine("Sales employee malak has been notified about the new sales report.");
            // }
    }
}
