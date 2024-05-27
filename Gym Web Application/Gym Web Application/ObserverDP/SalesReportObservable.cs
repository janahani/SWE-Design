using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;
using System.Collections.Generic;
using Gym_Web_Application.Data;

using System;

public class SalesReportObservable : ISalesReportObservable
{
    private List<ISalesEmployeeObserver> observers = new List<ISalesEmployeeObserver>();
    private SalesReportModel latestReport;
    private readonly AppDbContext _dbContext;
    private bool emailSent = false; 


    
 public SalesReportObservable(AppDbContext dbContext, EmailService emailService)
    {
        var employees = dbContext.Employees.Where(e => e.JobTitleID == 3).ToList();

        foreach (var employee in employees)
        {
            var salesEmployee = new SalesEmployee(employee, emailService);
            AttachObserver(salesEmployee);
        }
    }
    

    public SalesReportModel LatestReport
    {
        get { return latestReport; }
        set
        {
            if (value != latestReport)
        {
            latestReport = value;
            NotifyObservers(latestReport);
        }
        }
    }

    public void AttachObserver(ISalesEmployeeObserver observer)
    {
        
        observers.Add(observer);
        Console.WriteLine("New observer added");
   
}


    public void DetachObserver(ISalesEmployeeObserver observer)
    {
        SalesEmployee salesEmployeeObserver = observer as SalesEmployee;
        if (salesEmployeeObserver != null && salesEmployeeObserver.AssociatedEmployee.JobTitleID != 3)
    {
        observers.Remove(observer);
        Console.WriteLine("An observer is removed");
    }
    }

    public void NotifyObservers(SalesReportModel latestReport)
    {
        Console.WriteLine("Notifying sales reporters of new release of monthly sales report");

        if (!emailSent) 
        {
            foreach (var observer in observers)
            {
                observer.UpdateAsync(latestReport);
            }

            emailSent = true; 
        }
     emailSent = false;

    }
}
