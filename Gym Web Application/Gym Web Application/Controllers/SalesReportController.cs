using Microsoft.AspNetCore.Mvc;
using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;

namespace Gym_Web_Application.Controllers
{
    public class SalesReportController : Controller
    {
        private readonly SalesReportService _salesReportService;
        private readonly ISalesReportObservable _salesReportObservable;

        public SalesReportController(SalesReportService salesReportService, ISalesReportObservable salesReportObservable)
        {
            _salesReportService = salesReportService;
            _salesReportObservable = salesReportObservable;
        }

        [HttpPost]
        public IActionResult GenerateAndNotify()
        {
            SalesReportModel newReport = _salesReportService.GenerateMonthlySalesReport();
            
            ((SalesReportObservable)_salesReportObservable).LatestReport = newReport;

            return RedirectToAction("ViewSalesReport");
        }

        [HttpGet]
        public IActionResult ViewSalesReport()
        {
            var latestReport = _salesReportService.GetLatestSalesReport();
            return View(latestReport);
        }
    }
}
