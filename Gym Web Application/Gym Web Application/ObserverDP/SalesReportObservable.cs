 using System;
using Gym_Web_Application.Models;
using System.Collections.Generic;

using Gym_Web_Application.Models.SalesReportModel;

namespace Gym_Web_Application.ObserverDP{
public class SalesReportObservable : ISalesReportObservable
    {
        private List<ISalesEmployeeObserver> observers = new List<ISalesEmployeeObserver>();
        private SalesReport latestReport;

        public SalesReport LatestReport
        {
            get { return latestReport; }
            set
            {
                
                latestReport = value;
                NotifyObservers();
            }
        }

        public void AttachObserver(ISalesEmployeeObserver observer)
        {
            observers.Add(observer);
            Console.WriteLine("new observer added");
        }

        public void DetachObserver(ISalesEmployeeObserver observer)
        {
            observers.Remove(observer);
            Console.WriteLine("an observer is removed");
        }

        public void NotifyObservers()
        {
            Console.WriteLine("notifying sales reporters of new release of monthly sales report");
            foreach (var observer in observers)
            {
                observer.Update(LatestReport);
            }
        }
    }
}