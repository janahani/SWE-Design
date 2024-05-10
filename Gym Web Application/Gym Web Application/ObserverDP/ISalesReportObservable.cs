using Gym_Web_Application.Models;
namespace Gym_Web_Application.ObserverDP;

public interface ISalesReportObservable
{
    void AttachObserver(ISalesEmployeeObserver observer);
    void DetachObserver(ISalesEmployeeObserver observer);
    void NotifyObservers(SalesReportModel latestReport); 
}
