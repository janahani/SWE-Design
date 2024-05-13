using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;

namespace Gym_Web_Application.Controllers
{
    public class SalesReportController : Controller
    {
        private readonly SalesReportService _salesReportService;

        public SalesReportController(SalesReportService salesReportService)
        {
            _salesReportService = salesReportService;
        }
[HttpGet]
    public IActionResult ViewSalesReport()
    {
        var latestReport = _salesReportService.GetLatestSalesReport();
        return View(latestReport);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
}