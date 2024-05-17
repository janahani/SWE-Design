namespace Gym_Web_Application.Models;

using System.Threading.Tasks;
using global::Gym_Web_Application.ObserverDP;

public class SalesEmployee : ISalesEmployeeObserver
{
    private readonly EmployeeModel Sales_Employee;
    private readonly EmailService _emailService;


    public SalesEmployee(EmployeeModel employee, EmailService emailService)
    {
        Sales_Employee = employee; 
        _emailService = emailService;

    }

    public async void UpdateAsync(SalesReportModel latestReport)
    {
        //im assuming en el sales report job title id is 4 lehad ma tables are mapped
        if (Sales_Employee.JobTitleID == 4)
            {
                string subject = "New Sales Report Available";
                string body = $"Dear {Sales_Employee.Name},<br/>A new sales report has been generated. Please check your dashboard for more details.";
                await _emailService.SendEmailAsync(Sales_Employee.Email, subject, body);
                
                Console.WriteLine($"Sales employee {Sales_Employee.Name} has been notified about the new sales report.");
            }
    }
}
