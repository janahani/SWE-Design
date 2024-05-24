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
            latestReport = value;
            NotifyObservers(latestReport);
        }
    }

    public void AttachObserver(ISalesEmployeeObserver observer)
    {
        observers.Add(observer);
        Console.WriteLine("New observer added");
    }

    public void DetachObserver(ISalesEmployeeObserver observer)
    {
        observers.Remove(observer);
        Console.WriteLine("An observer is removed");
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
