 using System;
using System.Collections.Generic;

namespace Gym_Web_Application.ObserverDP{

 public interface ISalesReportObservable
    {
        void AttachObserver(ISalesEmployeeObserver observer);
        void DetachObserver(ISalesEmployeeObserver observer);
        void NotifyObservers();
    }
}