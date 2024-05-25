// SalesReportController.cs
using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;
using Microsoft.Extensions.DependencyInjection;

namespace Gym_Web_Application.Controllers
{
    public class SalesReportController : Controller
    {
        private readonly IServiceProvider _serviceProvider;

        public SalesReportController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateAndNotify()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var salesReportService = scope.ServiceProvider.GetRequiredService<SalesReportService>();
                var salesReportObservable = scope.ServiceProvider.GetRequiredService<ISalesReportObservable>();

                SalesReportModel newReport = await salesReportService.GenerateReport();
                ((SalesReportObservable)salesReportObservable).LatestReport = newReport;
                await salesReportService.NotifyObservers(newReport);

                return RedirectToAction("ViewSalesReport");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewSalesReport()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var salesReportService = scope.ServiceProvider.GetRequiredService<SalesReportService>();
                var latestReport = await salesReportService.GetLatestSalesReport();
                return View(latestReport);
            }
        }
    }
}