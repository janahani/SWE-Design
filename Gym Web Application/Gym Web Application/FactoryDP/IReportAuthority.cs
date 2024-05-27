namespace Gym_Web_Application.Models;

public interface IReportAuthority{
    public abstract Task<SalesReportModel> getLatestSalesReport();
}