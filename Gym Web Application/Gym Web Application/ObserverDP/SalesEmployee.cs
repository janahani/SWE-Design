namespace Gym_Web_Application.Models;
using global::Gym_Web_Application.ObserverDP;

public class SalesEmployee : ISalesEmployeeObserver
{
    private readonly EmployeeModel Sales_Employee;

    public SalesEmployee(EmployeeModel employee)
    {
        Sales_Employee = employee;
    }

    public void Update(SalesReportModel latestReport)
    {
        //im assuming en el sales report job title id is 3 lehad ma tables are mapped
        if (Sales_Employee.JobTitleID == 3)
        {
            //will either send an email notification or update the employee's dashboard here
            Console.WriteLine($"Sales employee {Sales_Employee.Name} has been notified about the new sales report.");
        }
    }
}