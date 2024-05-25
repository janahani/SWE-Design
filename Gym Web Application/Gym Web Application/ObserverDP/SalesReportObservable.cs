using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;
using System.Collections.Generic;
using System;

public class SalesReportObservable : ISalesReportObservable
{
    private List<ISalesEmployeeObserver> observers = new List<ISalesEmployeeObserver>();
    private SalesReportModel latestReport;

    

    

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

        foreach (var observer in observers)
        {
            observer.UpdateAsync(latestReport);
        }
    }
}
