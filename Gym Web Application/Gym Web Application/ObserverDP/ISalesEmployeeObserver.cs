 using System;
using System.Collections.Generic;

using Gym_Web_Application.Models;
using Gym_Web_Application.Models.SalesReportModel;


namespace Gym_Web_Application.ObserverDP{

 public interface ISalesEmployeeObserver
    {
        void Update(SalesReport latestReport);
    }
}