using Gym_Web_Application.Models;


namespace Gym_Web_Application.ObserverDP;

public interface ISalesEmployeeObserver
{
    void Update(SalesReportModel latestReport);
}
