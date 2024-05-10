using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Gym_Web_Application.ObserverDP;
using Microsoft.EntityFrameworkCore;


 public class SalesReportService
    {
        private readonly AppDbContext _dbContext;

        private readonly ISalesReportObservable _salesReportObservable;

        public SalesReportService(DbContextOptions<AppDbContext> options, ISalesReportObservable salesReportObservable)
        {
            _dbContext = AppDbContext.GetInstance(options);
            _salesReportObservable = salesReportObservable;
        }

        public async Task GenerateMonthlySalesReport()
        {
            //will generate new sales report every 25th day of month
            if (DateTime.Now.Day == 25)
            {
                SalesReportModel newReport = GenerateReport(); 

                //setting the latest report in the observable
                _salesReportObservable.LatestReport = newReport;
            }
        }

        private SalesReportModel GenerateReport()
        {

            DateTime today = DateTime.Today;
            DateTime _25thDayOfMonth = new DateTime(today.Year, today.Month, 25);

            var SalesReportData = _dbContext.SalesReport.Where(s => s.Date >= _25thDayOfMonth && s.Date <= today).ToList();
            
            decimal totalRevenue = SalesReportData.Sum(s => s.Amount);
            int totalMembershipsSold = SalesReportData.Count(s => s.MembershipType == MembershipType.Sold);
            int totalClassesAttended = SalesReportData.Sum(s => s.ClassesAttended);


            SalesReportModel report = new SalesReportModel
            {
                CreatedAt = today,
                TotalRevenue = totalRevenue,
                TotalMembershipsSold = totalMembershipsSold,
                TotalClassesAttended = totalClassesAttended
            };

            return report;


        }
    }